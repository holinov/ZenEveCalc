using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Raven.Client.Linq;
using Zen.EveCalc.Core.DataStorage;

namespace Zen.EveCalc.DataModel.EveDB
{
    public class EveCentalApi
    {
        private readonly Func<IRepositoryWithGuid<EveItem>> _itemRepos;

        public EveCentalApi(Func<IRepositoryWithGuid<EveItem>> itemRepos)
        {
            _itemRepos = itemRepos;
        }

        public void UpdatePricesInDb()
        {
            using (var repos=_itemRepos())
            {
                UpdatePrices(repos.GetAll());
            }
        }

        public void UpdatePrices(IEnumerable<EveItem> eveItems)
        {
            UpdatePrices(eveItems.Select(it=>it.EveId));
        }

        public void UpdatePrices(IEnumerable<int> eveIds)
        {
            using (var repos = _itemRepos())
            {
                var items = repos.Query
                                 .Where(it => it.EveId.In(eveIds))
                                 .OrderBy(it => it.Name)
                                 .ToArray();

                var prices = GetItemPriceById(items.Select(it => it.EveId));
                foreach (var eveItem in items)
                {
                    eveItem.Price = prices[eveItem.EveId];
                }

                repos.SaveChanges();
            }
        }

        public Dictionary<int,float> GetItemPriceById(IEnumerable<int> eveId,bool sell=false)
        {
            var res = new Dictionary<int,float>();
            //http://api.eve-central.com/api/marketstat?typeid=34&typeid=35&regionlimit=10000002
            var url = new StringBuilder("http://api.eve-central.com/api/marketstat?");
            foreach (var id in eveId)
            {
                url.Append("typeid=").Append(id).Append("&");
            }
            url.Append("regionlimit=10000002");

            using (var cli = new WebClient())
            {
                var rdr = cli.OpenRead(url.ToString());
                var ser = new XmlSerializer(typeof (evec_api));
                if (rdr != null)
                {
                    var objectRes = (evec_api)ser.Deserialize(rdr);
                    foreach (var type in objectRes.marketstat)
                    {
                        res[type.id] = sell? type.sell.avg :type.buy.avg;
                    }
                }
            }

            return res;
        }
    }
}
