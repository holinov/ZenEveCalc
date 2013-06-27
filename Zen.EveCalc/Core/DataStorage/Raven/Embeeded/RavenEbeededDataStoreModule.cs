using Autofac;
using Raven.Client;
using Raven.Client.Embedded;
using Raven.Client.Extensions;
using Raven.Client.Indexes;
using Raven.Database.Server;
using Raven.Imports.Newtonsoft.Json;

namespace Zen.EveCalc.Core.DataStorage.Raven.Embeeded
{
    /// <summary>
    ///     Регистрация DataStorage и фабрики сессий
    /// </summary>
    public class RavenEbeededDataStoreModule : Module
    {
        private readonly string _dataDirectory;        
        private readonly bool _httpAccesss;
        private IDocumentStore _ds;
        private bool _useCreationConverter = true;
        private int _ravenPort;
        private EmbeddableDocumentStore ds;

        public RavenEbeededDataStoreModule(string dataDirectory, bool httpAccesss = false, int ravenPort = 9909)
        {
            _ravenPort = ravenPort;
            _dataDirectory = dataDirectory;
            _httpAccesss = httpAccesss;            
        }

        public bool UseCreationConverter
        {
            get { return _useCreationConverter; }
            set { _useCreationConverter = value; }
        }

        public bool CreateIndexes { get; set; }


        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AutofacCreationConverter>().AsSelf().SingleInstance();

            builder.Register(context => InitDocumentStore(context.Resolve<AutofacCreationConverter>()))
                   .AsSelf()
                   .As<IDocumentStore>()
                   .SingleInstance()
                   .OnRelease(x =>
                       {
                           if (!x.WasDisposed) x.Dispose();
                       });

            builder
                .Register(context => context.Resolve<IDocumentStore>().OpenSession())
                .As<IDocumentSession>()
                .InstancePerDependency();
        }

        private IDocumentStore InitDocumentStore(AutofacCreationConverter converter)
        {

            if (_httpAccesss)
            {
                ds = new EmbeddableDocumentStore
                    {
                        DataDirectory = _dataDirectory,                        
                        UseEmbeddedHttpServer = true,
                    };
                NonAdminHttp.EnsureCanListenToWhenInNonAdminContext(_ravenPort);
            }
            else
            {
                ds = new EmbeddableDocumentStore
                    {
                        DataDirectory = _dataDirectory,                        
                    };
            }
            if (converter != null && UseCreationConverter)
            {
                ds.Conventions.CustomizeJsonSerializer += s => s.Converters.Add(converter);
            }
            ds.Conventions.DisableProfiling = true;
            ds.Conventions.JsonContractResolver = new RecordClrTypeInJsonContractResolver();
            ds.Conventions.CustomizeJsonSerializer += s => s.TypeNameHandling = TypeNameHandling.Arrays;

            ds.Initialize();
            
            if (CreateIndexes)
                IndexCreation.CreateIndexes(typeof (RavenDataStoreModule).Assembly, ds);

            //global::Raven.Client.Indexes.IndexCreation.CreateIndexes(typeof(RegionTrajectoryIndex).Assembly, ds);
            _ds = ds;
            return ds;
        }
    }
}