using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Web.Common.Controllers;

namespace Digbyswift.Umbraco.v9.Controllers
{
    public class BaseController : RenderController
    {
        protected readonly ILogger<RenderController> Logger;
        protected readonly ICompositeViewEngine CompositeViewEngine;

        public BaseController(ControllerDependencies defaultDependencies) : base(
            defaultDependencies.Logger,
            defaultDependencies.CompositeViewEngine,
            defaultDependencies.UmbracoContextAccessor)
        {
            Logger = defaultDependencies.Logger;
            CompositeViewEngine = defaultDependencies.CompositeViewEngine;
        }
    }
    
    public class BaseController<T> : BaseController where T : class, IPublishedContent
    {
        protected T TypedCurrentPage => CurrentPage as T;

        public BaseController(ControllerDependencies defaultDependencies) : base(defaultDependencies)
        {
        }
    }

}
