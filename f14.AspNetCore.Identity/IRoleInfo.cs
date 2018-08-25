using System;
using System.Collections.Generic;
using System.Text;

namespace f14.AspNetCore.Identity
{
    /// <summary>
    /// Represents the role proxy object.
    /// </summary>
    public interface IRoleInfo
    {
        /// <summary>
        /// Role name.
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// Role claims.
        /// </summary>
        IEnumerable<(string Type, string Value)> Claims { get; set; }
    }
}
