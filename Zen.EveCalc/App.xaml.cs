using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using Raven.Imports.Newtonsoft.Json;
using Zen.EveCalc.Controls;
using Zen.EveCalc.Core;
using Zen.EveCalc.Core.DataStorage;
using Zen.EveCalc.Core.DataStorage.Raven;
using Zen.EveCalc.Core.DataStorage.Raven.Embeeded;
using Zen.EveCalc.Core.DataStorage.Raven.Repositories;
using Zen.EveCalc.DataModel;
using Zen.EveCalc.DataModel.EveDB;

namespace Zen.EveCalc
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static App()
        {
            var core = AppCoreBuilder.Create()
                .AddModule(new RavenEbeededDataStoreModule("Data", true)
                    {
                        CreateIndexes = true
                    })
                .AddModule<RavenRepositoriesModule>()
                .AddModule<ControlsModule>()
                //.Configure(b=>b.RegisterAssemblyTypes(typeof(BlueprintsList).Assembly).AsSelf().AsImplementedInterfaces())
                .Build();

            Core = core;

            Core.Resolve<TaskFactory>().StartNew(() =>
                {
                    var apiTest = Core.Resolve<EveCentalApi>();
                    apiTest.UpdatePricesInDb();
                });
        }

        public static AppCore Core { get; set; }

        public static MaterialDetails LoadMaterials(Blueprint blueprint)
        {
            var url =
                string.Format(@"http://odylab-evedb.appspot.com/blueprintDetailsForTypeName/" +
                              HttpUtility.UrlEncode(blueprint.Name));

            var res = GetWholeResponce(url);

            /*ExpandoObject dynamic=new ExpandoObject();
            var res1=JsonConvert.DeserializeAnonymousType(res,dynamic);*/

            var matInfo = JsonConvert.DeserializeObject<MaterialDetails>(res);

            UpdateMaterials(matInfo);
            return matInfo;
            //res = JsonConvert.SerializeObject(res1, Formatting.Indented);

        }

        private static void UpdateMaterials(MaterialDetails matInfo)
        {
            var matIds = new List<int>();
            using (var repos=Core.Resolve<IEveItemRepository>())
            {
                if (matInfo != null)
                    foreach (var materialDto in matInfo.materialDtos)
                    {
                        var material = new EveItem();
                        if (repos.Query.Any(s => s.EveId == materialDto.materialTypeID))
                        {
                            var found = repos.Query.First(s => s.EveId == materialDto.materialTypeID);
                            if (found != null)
                            {
                                material = found;
                            }

                        }
                        else
                        {
                            repos.Store(material);
                        }
                        material.Name = materialDto.materialTypeName;
                        material.Volume = materialDto.materialVolume;
                        material.MaterialDto = materialDto;
                        material.EveId = materialDto.materialTypeID;  
                        matIds.Add(material.EveId);
                    }
                repos.SaveChanges();

            }

            Core.Resolve<EveCentalApi>().UpdatePrices(matIds);
        }
        

        public static string GetWholeResponce(string url)
        {
            string wholePage = "";
            try
            {
                using (var webClient = new WebClient())
                {
                    //log.InfoFormat("Подключение к адресу {0}", url);
                    //webClient.Credentials = new NetworkCredential("holinov", "1qaz@WSX3edc", "NVX");
                    webClient.Headers.Add("Accept-Charset", "windows-1251");
                    webClient.Headers.Add("Accept:application/json");
                    wholePage = webClient.DownloadString(url);
                }

            }
            catch (Exception e)
            {
                //log.Error("Ошибка подключения с серверу событий", e);
                wholePage = "";
            }
            return wholePage;
        }
    }
}