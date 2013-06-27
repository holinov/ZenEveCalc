using Autofac;

namespace Zen.EveCalc.Core
{
    public class AppCore : AppScope
    {
        public AppCore(IContainer container) : base(container)
        {
        }
    }
}