using System;
using Microsoft.AspNetCore.Html;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Web.Common.UmbracoContext;

namespace Digbyswift.Umbraco.v9.Extensions
{
    public static class LinkExtensions
    {
        public static IHtmlContent GetTargetAsHtml(this Link link)
        {
            if (link == null)
                return null;

            if (!String.IsNullOrWhiteSpace(link.Target))
                return new HtmlString($"target=\"{link.Target}\"");
                    
            if(link.Type == LinkType.Media || link.Type == LinkType.External)
                return new HtmlString("target=\"_blank\"");

            return null;
        }
    }
}