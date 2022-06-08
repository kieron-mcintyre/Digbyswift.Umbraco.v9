using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Website.Controllers;

namespace Digbyswift.Umbraco.v9.Controllers
{
    public class SurfaceControllerDependencies
    {
        public readonly ILogger<SurfaceController> Logger;
        public readonly IUmbracoContextAccessor UmbracoContextAccessor;
        public readonly IUmbracoDatabaseFactory DatabaseFactory;
        public readonly ServiceContext Services;
        public readonly AppCaches AppCaches;
        public readonly IProfilingLogger ProfilingLogger;
        public readonly IPublishedUrlProvider PublishedUrlProvider;

        public SurfaceControllerDependencies(
            ILogger<SurfaceController> logger,
            IUmbracoContextAccessor umbracoContextAccessor,
            IUmbracoDatabaseFactory databaseFactory,
            ServiceContext services,
            AppCaches appCaches,
            IProfilingLogger profilingLogger,
            IPublishedUrlProvider publishedUrlProvider)
        {
            Logger = logger;
            UmbracoContextAccessor = umbracoContextAccessor;
            DatabaseFactory = databaseFactory;
            Services = services;
            AppCaches = appCaches;
            ProfilingLogger = profilingLogger;
            PublishedUrlProvider = publishedUrlProvider;
        }
    }
}