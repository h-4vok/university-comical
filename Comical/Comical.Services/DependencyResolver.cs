using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Registration;
using Unity.Resolution;

namespace Comical.Services
{
    public class DependencyResolver
    {
        #region Singleton

        private DependencyResolver() { }
        static DependencyResolver() { }

        private static readonly DependencyResolver instance = new DependencyResolver();

        public static DependencyResolver obj => instance;

        #endregion

        private IUnityContainer container = new UnityContainer();

        public void SetContainer(IUnityContainer container) => this.container = container;

        public T Resolve<T>() => this.container.Resolve<T>();

        public T Resolve<T>(string key) => this.container.Resolve<T>(key);

        public void Register<Interface, Concrete>() where Concrete : Interface => this.container.RegisterType<Interface, Concrete>();

        public void RegisterSingleton<Interface>(Interface instance) => this.container.RegisterInstance<Interface>(instance);
    }
}
