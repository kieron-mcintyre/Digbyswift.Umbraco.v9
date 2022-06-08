using System;
using Umbraco.Cms.Core;

namespace Digbyswift.Umbraco.v9.Extensions
{
    public static class UdiExtensions
    {
        public static Guid ToGuid(this Udi value)
        {
            if(value == null)
                throw new ArgumentNullException(nameof(value));

            var guidUdi = value as GuidUdi;
            if (guidUdi == null)
            {
                return Guid.Empty;
            }

            return guidUdi.Guid;
        }
    }
}