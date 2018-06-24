using System.Web;
using System.Web.Http;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;
using XMLParser.Api.Attributes;
using XMLParser.BusinessLogic.Parser;
using XMLParser.BusinessLogic.Parser.Implementation;
using XMLParser.BusinessLogic.Validation;
using XMLParser.BusinessLogic.Validation.Implementation;

namespace XMLParser.Api {
    public class WebApiApplication : HttpApplication {
        protected void Application_Start() {
            ContainerSetup();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.Filters.Add(new ValidRawXmlInRequestAttribute());
        }

        /// <summary>
        /// setup SimpleInjector container & register components
        /// </summary>
        private static void ContainerSetup() {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            container.Register<IRawXmlValidator, RawXmlValidator>(Lifestyle.Singleton);
            container.Register<IXmlParser, XmlParser>(Lifestyle.Singleton);

            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
            container.Verify();
            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);
        }
    }
}
