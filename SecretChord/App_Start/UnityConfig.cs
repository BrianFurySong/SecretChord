using System.Web.Mvc;
using Microsoft.Practices.Unity;
using SecretChord.Services;
using SecretChord.Services.Tools;
using Unity;
using Unity.Mvc5;

namespace SecretChord
{
    public sealed class UnityConfig
    {
        private static readonly UnityConfig _instance = new UnityConfig();
        private UnityConfig() { }
        static UnityConfig() { }
        public static UnityConfig Instance { get { return _instance; } }

        public void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IFaqItemService, FaqItemService>();
            container.RegisterType<IAppConfigService, AppConfigService>();
            container.RegisterType<IFileRepositoryService, FileRepositoryService>();
            container.RegisterType<IFileTransferService, FileTransferService>();
            container.RegisterType<IAboutPageService, AboutPageService>();

            System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver =
                new Unity.WebApi.UnityDependencyResolver(container);
            DependencyResolver.SetResolver(new Unity.Mvc5.UnityDependencyResolver(container));

        }
    }
}