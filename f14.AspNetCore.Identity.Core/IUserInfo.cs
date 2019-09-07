using System;
using System.Collections.Generic;
using System.Text;

namespace f14.AspNetCore.Identity
{
    /// <summary>
    /// Provides an interface for user info.
    /// </summary>
    public interface IUserInfo
    {
        /// <summary>
        /// User login.
        /// </summary>
        string Login { get; set; }
        /// <summary>
        /// User password.
        /// </summary>
        string Password { get; set; }
        /// <summary>
        /// User email.
        /// </summary>
        string Email { get; set; }
        /// <summary>
        /// User roles.
        /// </summary>
        IEnumerable<string> Roles { get; set; }
    }
}
