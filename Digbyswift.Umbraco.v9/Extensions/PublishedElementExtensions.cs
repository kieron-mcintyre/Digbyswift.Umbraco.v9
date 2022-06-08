using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;

namespace Digbyswift.Umbraco.v9.Extensions
{
    public static class PublishedElementExtensions
    {

        public static bool Is(this IPublishedElement element, string alias)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (alias == null)
                throw new ArgumentNullException(nameof(alias));

            return element.ContentType.Alias.Equals(alias);
        }

        public static bool IsAny(this IPublishedElement element, params string[] alias)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (alias == null)
                throw new ArgumentNullException(nameof(alias));

            return alias.Any(x => element.Is(x));
        }

        public static T? ValueOrNull<T>(this IPublishedElement element, string alias) where T : struct
        {
            return (T?)element.Value(alias, defaultValue: null, fallback: Fallback.ToDefaultValue);
        }

        public static Guid? ValueAsGuid(this IPublishedElement element, string alias)
        {
            var value = element.Value<string>(alias, defaultValue: null, fallback: Fallback.ToDefaultValue);
            if (String.IsNullOrWhiteSpace(value))
                return null;

            if (Guid.TryParse(value, out Guid returnValue))
                return returnValue;

            return null;
        }

        public static IPublishedElement ValueAsElement(this IPublishedElement element, string alias)
        {
            return element.Value<IPublishedElement>(alias);
        }

        public static IReadOnlyCollection<IPublishedElement> GetPropertyAsElementList(this IPublishedElement element, string alias)
        {
            return element.Value<IEnumerable<IPublishedElement>>(alias)?.ToList() ?? Constants.EmptyElementList;
        }
    }
}
