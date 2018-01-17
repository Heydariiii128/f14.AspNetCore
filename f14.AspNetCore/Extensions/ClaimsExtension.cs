using f14;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace System.Security.Claims
{
    /// <summary>
    /// Provides an extensions methods for claims.
    /// </summary>
    public static class ClaimsExtension
    {
        /// <summary>
        /// Tries to get user id.
        /// </summary>
        /// <param name="principal">The user claim principal.</param>
        /// <returns>The user id or null.</returns>
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            ExHelper.NotNull(() => principal);

            return principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
        /// <summary>
        /// Tries to get user name.
        /// </summary>
        /// <param name="principal">The user claim principal.</param>
        /// <returns>The user name or null.</returns>
        public static string GetUserName(this ClaimsPrincipal principal)
        {
            ExHelper.NotNull(() => principal);

            return principal.FindFirst(ClaimTypes.Name)?.Value;
        }
    }
}
