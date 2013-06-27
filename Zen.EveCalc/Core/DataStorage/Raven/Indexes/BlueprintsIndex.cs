using System.Linq;
using Raven.Client.Indexes;
using Zen.EveCalc.DataModel;

namespace Zen.EveCalc.Core.DataStorage.Raven.Indexes
{
    public class BlueprintsIndex : AbstractIndexCreationTask<Blueprint>
    {
        public BlueprintsIndex()
        {
            Map = items => from eveItem in items
                           select new
                               {
                                   eveItem.ItemType,
                                   eveItem.Name,
                                   eveItem.Price,
                                   eveItem.EveId
                               };
        }
    }
}