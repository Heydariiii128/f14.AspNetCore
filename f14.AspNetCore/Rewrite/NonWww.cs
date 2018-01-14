using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace f14.AspNetCore.Engine.Rewrite
{
    /// <summary>
    /// Provides the redirect rule which redirect "www.sample.com" to "sample.com".
    /// </summary>
    public class NonWww : IRule
    {
        private bool _permanent = false;

        public NonWww(bool premanent)
        {
            _permanent = premanent;
        }

        public void ApplyRule(RewriteContext context)
        {
            var request = context.HttpContext.Request;
            var host = request.Host;
            if (host.Host.StartsWith("www."))
            {
                var nonWwwHost = new HostString(host.Host.Substring(4), host.Port ?? 80);
                var sb = new StringBuilder()
                    .Append("http://")
                    .Append(nonWwwHost)
                    .Append(request.PathBase)
                    .Append(request.Path)
                    .Append(request.QueryString);
                context.HttpContext.Response.Redirect(sb.ToString(), _permanent);
                context.Result = RuleResult.EndResponse;
            }
        }
    }
}
