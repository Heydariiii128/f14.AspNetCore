using System;
using System.Collections.Generic;
using System.Text;

namespace f14.AspNetCore.Identity
{
    /// <summary>
    /// Represents the claim proxy object.
    /// </summary>
    public class ClaimInfo : IClaimInfo
    {
        /// <summary>
        /// Role name.
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// Role claims.
        /// </summary>
        public string Value { get; set; }
    }
}
