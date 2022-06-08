using System;
using Digbyswift.Core.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Web.Website.Controllers;

namespace Digbyswift.Umbraco.v9.Controllers
{
    public abstract class BaseSurfaceController : SurfaceController
    {
        protected readonly ILogger<SurfaceController> Logger;

        protected BaseSurfaceController(SurfaceControllerDependencies defaultDependencies) 
            : base(
                defaultDependencies.UmbracoContextAccessor,
                defaultDependencies.DatabaseFactory,
                defaultDependencies.Services,
                defaultDependencies.AppCaches,
                defaultDependencies.ProfilingLogger,
                defaultDependencies.PublishedUrlProvider)
        {
            Logger = defaultDependencies.Logger;
        }
        
        [NonAction]
        public virtual RedirectResult Redirect(string url, string hash)
        {
            if (String.IsNullOrWhiteSpace(url))
                throw new ArgumentException("Cannot be null or empty", nameof(url));
            
            if (String.IsNullOrWhiteSpace(hash) || hash.Equals(StringConstants.Hash))
                return new RedirectResult(url);

            return new RedirectResult($"{url}#{hash.Trim(CharConstants.Hash)}");
        }
    }

    public abstract class BaseSurfaceController<T> : BaseSurfaceController where T : class, IPublishedContent
    {
        private T _typedCurrentPage;
        public T TypedCurrentPage
        {
            get
            {
                if (_typedCurrentPage != null)
                    return _typedCurrentPage;

                if (!Request.RouteValues.ContainsKey("pageId"))
                    return null;
                
                var contentCache = HttpContext.RequestServices.GetService<IPublishedContentQuery>();
                return (_typedCurrentPage = contentCache.Content(Request.RouteValues["pageId"]) as T);
            }
        }

        public BaseSurfaceController(SurfaceControllerDependencies defaultDependencies) : base(defaultDependencies)
        {
        }
    }

    
}