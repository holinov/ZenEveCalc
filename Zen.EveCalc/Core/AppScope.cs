using System;
using System.Collections.Generic;
using Autofac;
using Autofac.Core;

namespace Zen.EveCalc.Core
{
    public class AppScope : IDisposable
    {
        private readonly ILifetimeScope _scope;

        public AppScope(ILifetimeScope scope)
        {
            _scope = scope;
        }

        public void Dispose()
        {
            _scope.Dispose();
        }

        public TType Resolve<TType>()
        {
            return _scope.Resolve<TType>();
        }

        public object Resolve(Type type)
        {
            return _scope.Resolve(type);
        }

        public AppScope BeginScope()
        {
            return new AppScope(_scope.BeginLifetimeScope());
        }
    }
}