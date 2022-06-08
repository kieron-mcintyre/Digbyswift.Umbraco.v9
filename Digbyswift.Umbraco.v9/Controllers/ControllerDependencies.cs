using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;

namespace Digbyswift.Umbraco.v9.Controllers
{
    public class ControllerDependencies
    {
        public readonly ILogger<RenderController> Logger;
        public readonly ICompositeViewEngine CompositeViewEngine;
        public readonly IUmbracoContextAccessor UmbracoContextAccessor;

        public ControllerDependencies(
            ILogger<RenderController> logger,
            ICompositeViewEngine compositeViewEngine,
            IUmbracoContextAccessor umbracoContextFactory)
        {
            Logger = logger;
            CompositeViewEngine = compositeViewEngine;
            UmbracoContextAccessor = umbracoContextFactory;
        }
    }
}