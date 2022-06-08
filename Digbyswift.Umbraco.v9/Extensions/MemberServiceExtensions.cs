using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Digbyswift.Extensions;
using Digbyswift.Extensions.Validation;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;
using uConstants = Umbraco.Cms.Core.Constants;


namespace Digbyswift.Umbraco.v9.Extensions
{
    public static class MemberServiceExtensions
    {
        public static IReadOnlyCollection<IMember> Get(this IMemberService service, int[] ids)
        {
            if (ids == null)
                throw new ArgumentNullException(nameof(ids));
            
            if (ids.IsEmpty())
                return Enumerable.Empty<IMember>().ToList();
			
            return service.GetAllMembers(ids).ToList();
        }

        public static Dictionary<int, string> GetEmailsById(this IMemberService service)
        {
            const string sql = @"SELECT m.nodeId [Key], m.Email [Value] FROM [dbo].cmsMember m";
            
            using (var conn = new SqlConnection(uConstants.System.UmbracoConnectionName))
            {
                return conn.Query(sql).ToList().ToDictionary(
                    row => (int)row.Key,
                    row => (string)row.Value);
            }
        }

        public static IEnumerable<string> GetMatchingEmails(this IMemberService service, IEnumerable<string> emails)
        {
            if(emails == null)
                throw new ArgumentNullException(nameof(emails));

            var workingEmails = emails.Where(x => x.IsEmail()).ToList();
            if (workingEmails.IsEmpty())
                return new List<string>();

            const string emailMatchingSql = @"SELECT m.Email FROM dbo.cmsMember AS m WHERE m.Email IN @emails";
            const string loginMatchingSql = @"SELECT m.LoginName FROM dbo.cmsMember AS m WHERE m.LoginName IN @emails";

            using (var conn = new SqlConnection(uConstants.System.UmbracoConnectionName))
            {
                var matchingEmails = conn.Query<string>(loginMatchingSql, new {emails = workingEmails}).ToList();
                var matchingLoginNames = conn.Query<string>(emailMatchingSql, new {emails = workingEmails}).ToList();

                return matchingEmails.Union(matchingLoginNames, StringComparer.OrdinalIgnoreCase).ToList();
            }
        }
        

    }
}
