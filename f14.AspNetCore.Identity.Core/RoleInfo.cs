using System;
using System.Collections.Generic;
using System.Text;

namespace f14.AspNetCore.Identity
{
    /// <summary>
    /// Represents the role proxy object.
    /// </summary>
    public class RoleInfo : IRoleInfo
    {
        /// <summary>
        /// Role name.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Role claims.
        /// </summary>
        public IEnumerable<IClaimInfo> Claims { get; set; }
    }
}
