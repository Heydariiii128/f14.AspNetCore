using System;
using System.Collections.Generic;
using System.Text;

namespace f14.AspNetCore.Identity
{
    /// <summary>
    /// Represents the user proxy class.
    /// </summary>
    public class UserInfo
    {
        /// <summary>
        /// User login.
        /// </summary>
        public string Login { get; set; }
        /// <summary>
        /// User password.
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// User email.
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// User roles.
        /// </summary>
        public IEnumerable<string> Roles { get; set; }
    }
}
