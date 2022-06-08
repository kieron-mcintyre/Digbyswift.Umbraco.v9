using System;
using System.Linq;
using Digbyswift.Core.Constants;
using Digbyswift.Extensions;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Web.Common;
using Umbraco.Extensions;

namespace Digbyswift.Umbraco.v9.Extensions
{
    public static class UmbracoHelperExtensions
    {
        public static IPublishedContent TypedContentFromPath(this UmbracoHelper helper, string path)
        {
            if (String.IsNullOrWhiteSpace(path))
                throw new ArgumentException("Cannot be null or empty", nameof(path));
            
            // We only want to match items with more than one path
            // part, e.g. /articles/article-name, /my-cbi/policy-forum/articles/article-name
            var pathParts = path.Split(new[] {CharConstants.ForwardSlash}, StringSplitOptions.RemoveEmptyEntries);
            if (pathParts.Length < 2)
                return null;

            var rootNodes = helper.ContentAtRoot();
            if (rootNodes.IsEmpty())
                return null;

            var workingNode = (IPublishedContent)null;

            foreach (var rootNode in rootNodes)
            {
                workingNode = rootNode;

                // Walk the tree of each root node but break out if we can't match any part of the URL structure
                foreach (var urlName in pathParts)
                {
                    // Assume that the page's urlName property could be provided by either the
                    // urlName field or by the umbracoUrlName field.
                    workingNode = workingNode.FirstChild(x => x.Value<string>("umbracoUrlName", x.UrlSegment).Equals(urlName));
                    if (workingNode == null)
                    {
                        break;
                    }
                }

                // If a node has been found, then break out of walking the root nodes further
                if (workingNode != null)
                {
                    break;
                }
            }

            return workingNode;
        }
    }
}