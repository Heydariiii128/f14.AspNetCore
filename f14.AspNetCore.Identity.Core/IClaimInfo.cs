using System;
using System.Collections.Generic;
using System.Text;

namespace f14.AspNetCore.Identity
{
    /// <summary>
    /// Represents the claim proxy object.
    /// </summary>
    public interface IClaimInfo
    {
        /// <summary>
        /// Claim type.
        /// </summary>
        string Type { get; set; }
        /// <summary>
        /// Claim value.
        /// </summary>
        string Value { get; set; }
    }
}
