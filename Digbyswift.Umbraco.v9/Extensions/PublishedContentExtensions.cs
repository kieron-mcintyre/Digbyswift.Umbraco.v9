using System;
using System.Linq;
using System.Text.RegularExpressions;
using Digbyswift.Extensions;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;

namespace Digbyswift.Umbraco.v9.Extensions
{
    public static class PublishedContentExtensions
    {
        private static readonly Regex DuplicateNameRegex = new Regex(@"^(.*)(\s\([1-9]\d*\))$");

        #region Properties
        
        /// <summary>
        /// Uses the title property, falling back to the displayTitle and then the name without a duplicate suffix.
        /// </summary>
        public static string GetDisplayTitle(this IPublishedContent content)
        {
            if(content == null)
                throw new ArgumentNullException(nameof(content));

            return content
                .Value<string>("title")
                .Coalesce(content.Value<string>("displayTitle"))
                .Coalesce(content.GetNameWithoutDuplicateSuffix());
        }

        /// <summary>
        /// Uses the listingDate property, and failing that the createDate 
        /// </summary>
        public static DateTime GetDisplayDate(this IPublishedContent content)
        {
            if(content == null)
                throw new ArgumentNullException(nameof(content));

            return content.Value<DateTime>("listingDate") != DateTime.MinValue
                ? content.Value<DateTime>("listingDate")
                : content.CreateDate;
        }

        /// <summary>
        /// Returns the content name without the duplicate prefix, e.g. (1)
        /// </summary>
        public static string GetNameWithoutDuplicateSuffix(this IPublishedContent content)
        {
            var matches = DuplicateNameRegex.Matches(content.Name);
            if (matches.Count == 0 || matches[0].Groups.Count < 2)
                return content.Name;

            return matches[0].Groups[1].Value;
        }

        public static string GetNameWithoutDuplicateSuffix(this IContent content)
        {
            var matches = DuplicateNameRegex.Matches(content.Name);
            if (matches.Count == 0 || matches[0].Groups.Count < 2)
                return content.Name;

            return matches[0].Groups[1].Value;
        }

        public static string GetMetaTitle(this IPublishedContent content)
        {
            return content
                .Value<string>("metaTitle")
                .Coalesce(content.GetDisplayTitle());
        }

        public static string GetMetaDescription(this IPublishedContent content, int characterLimit = 300)
        {
            if (!string.IsNullOrEmpty(content.Value<string>("metaDescription")))
                return content.Value<string>("metaDescription").TruncateAtWord(characterLimit, "...");
            
            if (!string.IsNullOrEmpty(content.Value<string>("listingSummary")))
                return content.Value<string>("listingSummary").StripMarkup().TruncateAtWord(characterLimit, "...");
            
            if(!string.IsNullOrEmpty(content.Value<string>("summary")))
                return content.Value<string>("summary").StripMarkup().TruncateAtWord(characterLimit, "...");
            
            if (!string.IsNullOrEmpty(content.Value<string>("title")))
                return content.Value<string>("title").TruncateAtWord(characterLimit, "...");

            return GetMetaTitle(content);
        }

        #endregion
        
        #region Navigation
        
        public static bool HasAncestor(this IPublishedContent content, string docTypeAlias)
        {
            if(content == null)
                throw new ArgumentNullException(nameof(content));

            return content.Ancestor(docTypeAlias) != null;
        }

        public static bool HasTemplate(this IPublishedContent content)
        {
            if(content == null)
                throw new ArgumentNullException(nameof(content));

            return content.TemplateId > 0;
        }

        public static IPublishedContent FirstSibling(this IPublishedContent content)
        {
            if(content == null)
                throw new ArgumentNullException(nameof(content));

            return content.Siblings().FirstOrDefault();
        }

        public static IPublishedContent FirstSibling(this IPublishedContent content, string alias)
        {
            if(content == null)
                throw new ArgumentNullException(nameof(content));

            return content.Siblings().FirstOrDefault(x => x.Is(alias));
        }

        public static T FirstSibling<T>(this IPublishedContent content) where T : class, IPublishedContent
        {
            if(content == null)
                throw new ArgumentNullException(nameof(content));

            return content.Siblings<T>().FirstOrDefault();
        }

        public static IPublishedContent PreviousSibling(this IPublishedContent content)
        {
            if(content == null)
                throw new ArgumentNullException(nameof(content));

            return content.PreviousSibling<IPublishedContent>();
        }

        public static IPublishedContent PreviousSibling(this IPublishedContent content, string alias)
        {
            if(content == null)
                throw new ArgumentNullException(nameof(content));

            return content.PreviousSibling<IPublishedContent>(x => x.Is(alias));
        }

        public static T PreviousSibling<T>(this IPublishedContent content, Func<T, bool> filter = null) where T : class, IPublishedContent
        {
            if(content == null)
                throw new ArgumentNullException(nameof(content));

            if (filter != null)
                return content.Siblings<T>().Where(filter).LastOrDefault(x => x.SortOrder < content.SortOrder);
            
            return content.Siblings<T>().LastOrDefault(x => x.SortOrder < content.SortOrder);
        }

        public static IPublishedContent LastSibling(this IPublishedContent content)
        {
            if(content == null)
                throw new ArgumentNullException(nameof(content));

            return content.Siblings().LastOrDefault();
        }

        public static IPublishedContent LastSibling(this IPublishedContent content, string alias)
        {
            if(content == null)
                throw new ArgumentNullException(nameof(content));

            return content.Siblings().LastOrDefault(x => x.Is(alias));
        }

        public static T LastSibling<T>(this IPublishedContent content) where T : class, IPublishedContent
        {
            if(content == null)
                throw new ArgumentNullException(nameof(content));

            return content.Siblings<T>().LastOrDefault();
        }

        public static IPublishedContent NextSibling(this IPublishedContent content, Func<IPublishedContent, bool> filter = null)
        {
            if(content == null)
                throw new ArgumentNullException(nameof(content));

            return content.NextSibling<IPublishedContent>(filter);
        }

        public static IPublishedContent NextSibling(this IPublishedContent content, string alias)
        {
            if(content == null)
                throw new ArgumentNullException(nameof(content));

            return content.NextSibling(x => x.Is(alias));
        }

        public static T NextSibling<T>(this IPublishedContent content, Func<T, bool> filter = null) where T : class, IPublishedContent
        {
            if(content == null)
                throw new ArgumentNullException(nameof(content));

            if (filter != null)
                return content.Siblings<T>().Where(filter).FirstOrDefault(x => x.SortOrder > content.SortOrder);
            
            return content.Siblings<T>().FirstOrDefault(x => x.SortOrder > content.SortOrder);
        }
        
        #endregion

    }
}
