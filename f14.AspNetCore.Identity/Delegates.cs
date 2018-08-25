using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace f14.AspNetCore.Identity
{
    /// <summary>
    /// The <see cref="IdentityError"/> handler.
    /// </summary>
    /// <param name="error">The identity error instance.</param>
    public delegate void IdentityErrorHandler(IdentityError error);
}
