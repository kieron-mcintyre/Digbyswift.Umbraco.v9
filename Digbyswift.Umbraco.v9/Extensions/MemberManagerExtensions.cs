using System.Threading.Tasks;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Security;

namespace Digbyswift.Umbraco.v9.Extensions
{
    public static class MemberManagerExtensions
    {
        public static async Task<IPublishedContent> GetCurrentMemberContentAsync(this IMemberManager memberManager)
        {
            var member = await memberManager.GetCurrentMemberAsync();
            if (member == null)
                return null;

            return memberManager.AsPublishedMember(member);
        }
    }
}
