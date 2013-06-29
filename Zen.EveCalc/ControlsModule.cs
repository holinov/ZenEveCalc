using System.Threading.Tasks;
using System.Windows.Input;
using Autofac;
using Zen.EveCalc.Controls.Models;
using Zen.EveCalc.Core.DataStorage.Raven.Repositories;

namespace Zen.EveCalc
{
    public class ControlsModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly)
                   .AsSelf()
                   .AsImplementedInterfaces();

            builder.RegisterType<EveItemRepository>().AsImplementedInterfaces();
            builder.RegisterInstance(new TaskFactory()).AsSelf().SingleInstance();
            builder.RegisterAssemblyTypes(ThisAssembly).AssignableTo<ICommand>().As<ICommand>();
            builder.RegisterGeneric(typeof (SaveListCommand<>)).AsSelf();
            //builder.RegisterType<EveItemRepository>().AsImplementedInterfaces();
        }
    }
}