using System.Collections.Generic;
using System.Collections.ObjectModel;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Digbyswift.Umbraco.v9
{
    public static class Constants
    {
        public static readonly IReadOnlyCollection<IPublishedElement> EmptyElementList = new Collection<IPublishedElement>();
        public static readonly IReadOnlyCollection<IPublishedContent> EmptyContentList = new Collection<IPublishedContent>();
    }
}