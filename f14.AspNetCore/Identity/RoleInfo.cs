using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace f14.AspNetCore.Identity
{
    /// <summary>
    /// Represents the role proxy object.
    /// </summary>
    public class RoleInfo
    {
        /// <summary>
        /// Role name.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Role claims.
        /// </summary>
        public IEnumerable<(string Type, string Value)> Claims { get; set; }
    }
}
