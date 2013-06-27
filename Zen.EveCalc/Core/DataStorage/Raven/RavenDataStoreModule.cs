using System.Collections.Generic;
using Autofac;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Indexes;
using Raven.Imports.Newtonsoft.Json;

namespace Zen.EveCalc.Core.DataStorage.Raven
{
    /// <summary>
    ///     ����������� DataStorage � ������� ������
    /// </summary>
    public class RavenDataStoreModule : Module
    {
        private readonly IDocumentStore _ds;
        private bool _useCreationConverter = true;

        /// <summary>
        ///     ��������� �� �� ����� 8080
        /// </summary>
        public RavenDataStoreModule() : this(@"http://localhost:8080", "TempDb")
        {
        }

        /// <summary>
        ///     ����������� � ��������� �� ������ �����������
        /// </summary>
        /// <param name="connectionStringName">��� ������ ����������� � �������</param>
        public RavenDataStoreModule(string connectionStringName)
        {
            ConnectionStringName = connectionStringName;
        }

        /// <summary>
        ///     ����������� � �� �� URL � ����� ����
        /// </summary>
        /// <param name="url">����� �������</param>
        /// <param name="defaultDb">��� ����</param>
        public RavenDataStoreModule(string url, string defaultDb)
        {
            Url = url;
            DefaultDatabase = defaultDb;
            ConnectionStringName = null;
        }

        /// <summary>
        ///     ������������� ������������������� ���������
        ///     ������ ���� ��� �������������������
        /// </summary>
        /// <param name="ds">��������� ����������</param>
        public RavenDataStoreModule(IDocumentStore ds)
        {
            _ds = ds;
        }

        public string ConnectionStringName { get; set; }
        public string Url { get; set; }
        public string DefaultDatabase { get; set; }

        public bool UseCreationConverter
        {
            get { return _useCreationConverter; }
            set { _useCreationConverter = value; }
        }

        public bool CreateIndexes { get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AutofacCreationConverter>().AsSelf().SingleInstance();

            builder.Register(
                context =>
                InitDocumentStore(context.Resolve<AutofacCreationConverter>()))
                   .AsSelf()
                   .As<IDocumentStore>()
                   .SingleInstance()
                   .OnRelease(x => { if (!x.WasDisposed) x.Dispose(); });

            builder
                .Register(context => context.Resolve<IDocumentStore>().OpenSession())
                .As<IDocumentSession>()
                .InstancePerDependency();
        }

        private IDocumentStore InitDocumentStore(AutofacCreationConverter converter)
        {
            IDocumentStore ds;
            if (_ds != null)
                ds = _ds;
            else if (ConnectionStringName != null)
            {
                ds = new DocumentStore
                    {
                        ConnectionStringName = ConnectionStringName
                    };
                ds.Initialize();
            }
            else if (!string.IsNullOrEmpty(Url))
            {
                var store = new DocumentStore
                    {
                        Url = Url,
                    };
                if (!string.IsNullOrEmpty(DefaultDatabase))
                    store.DefaultDatabase = DefaultDatabase;
                store.Initialize();

                ds = store;
            }
            else
            {
                ds = new DocumentStore
                    {
                        Url = "http://localhost:9901"
                    };
                ds.Initialize();
            }

            if (converter != null && UseCreationConverter)
            {
                ds.Conventions.CustomizeJsonSerializer += s => s.Converters.Add(converter);
            }

            ds.Conventions.DisableProfiling = true;
            ds.Conventions.JsonContractResolver = new RecordClrTypeInJsonContractResolver();
            ds.Conventions.CustomizeJsonSerializer += s => s.TypeNameHandling = TypeNameHandling.Arrays;

            if (CreateIndexes)
                IndexCreation.CreateIndexes(typeof (RavenDataStoreModule).Assembly, ds);
            //ds.DatabaseCommands.EnsureDatabaseExists()

            //global::Raven.Client.Indexes.IndexCreation.CreateIndexes(typeof(RegionTrajectoryIndex).Assembly, ds);

            return ds;
        }
    }
}