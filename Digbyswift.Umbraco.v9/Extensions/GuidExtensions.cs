using System;
using Umbraco.Cms.Core;
using uConstants = Umbraco.Cms.Core.Constants;

namespace Digbyswift.Umbraco.v9.Extensions
{
    public static class GuidExtensions
    {
        public static Udi ToUdi(this Guid value, string entityType = uConstants.UdiEntityType.Document)
        {
            if(value == Guid.Empty)
                throw new ArgumentOutOfRangeException(nameof(value), "Cannot be empty");

            return Udi.Create(entityType, value);
        }
    }
}