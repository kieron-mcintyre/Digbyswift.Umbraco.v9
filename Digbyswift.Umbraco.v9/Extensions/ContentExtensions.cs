using System;
using System.Collections.Generic;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models;
using uConstants = Umbraco.Cms.Core.Constants;

namespace Digbyswift.Umbraco.v9.Extensions
{
    public static class ContentExtensions
    {

        public static Dictionary<string, object> GetDirtyProperties(this IContent content)
        {
            var dirtyEntries = new Dictionary<string, object>();

            foreach (var property in content.Properties)
            {
                if (content.IsPropertyDirty(property.Alias))
                {
                    dirtyEntries.Add(property.Alias, property.GetValue());
                }
            }

            return dirtyEntries;
        }

        public static void SetValueAsUdi(this IContent content, string alias, Guid contentKey, string entityType = uConstants.UdiEntityType.Document)
        {
            content.SetValue(alias, Udi.Create(entityType, contentKey).ToString());
        }

    }
}
