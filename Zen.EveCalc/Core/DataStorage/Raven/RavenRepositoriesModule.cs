using Autofac;

namespace Zen.EveCalc.Core.DataStorage.Raven
{
    /// <summary>
    ///     Модуль стандартных репозиториев Raven
    /// </summary>
    public class RavenRepositoriesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof (BasicRavenRepositoryWithGuid<>))
                   .As(typeof (IRepository<>))
                   .As(typeof (IRepositoryWithGuid<>))
                   .AsImplementedInterfaces();

            builder.RegisterGeneric(typeof (Refrence<>))
                   .PropertiesAutowired()
                   .AsSelf();
        }
    }
}