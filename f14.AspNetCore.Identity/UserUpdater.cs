using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace f14.AspNetCore.Identity
{
    /// <summary>
    /// The user updater imlp.
    /// </summary>
    /// <typeparam name="TUser">Type of identity user.</typeparam>
    public class UserUpdater<TUser> : IdentityUpdater where TUser : class
    {
        private UserManager<TUser> UserManager { get; set; }
        private IEnumerable<IUserInfo> Users { get; set; }
        private Func<IUserInfo, TUser> UserFactory { get; set; }
        private Action<TUser> UserHandler { get; set; }

        public UserUpdater(UserManager<TUser> userManager, IEnumerable<IUserInfo> users, Func<IUserInfo, TUser> userFactory)
        {
            UserManager = userManager;
            Users = users;
            UserFactory = userFactory;
        }

        #region Main

        /// <summary>
        /// Sets the action that will be fired when new user is created or retrieved from the user store.
        /// </summary>
        /// <param name="handler">The action.</param>
        /// <returns>The current instance.</returns>
        public UserUpdater<TUser> HandleUser(Action<TUser> handler)
        {
            UserHandler = handler;
            return this;
        }
        /// <summary>
        /// Updates users via UserManager.
        /// </summary>
        /// <returns>A update action task.</returns>
        private async Task UpdateUsersAsync()
        {
            IdentityResult result;
            foreach (var userInfo in Users)
            {
                var user = await UserManager.FindByNameAsync(userInfo.Login);
                if (user == null)
                {
                    user = UserFactory(userInfo);
                    result = await UserManager.CreateAsync(user, userInfo.Password);
                    if (!result.Succeeded)
                    {
                        HandleIdentityErrors(result.Errors);
                        continue;
                    }                                                         
                }

                UserHandler?.Invoke(user);

                if (userInfo.Roles != null)
                {
                    await UpdateUserRolesAsync(user, userInfo.Roles);
                }
            }
        }
        /// <summary>
        /// Updates user roles.
        /// </summary>
        /// <param name="user">The concrete user.</param>
        /// <param name="roles">The roles collection to update.</param>
        /// <returns>A update action task.</returns>
        private async Task UpdateUserRolesAsync(TUser user, IEnumerable<string> roles)
        {
            var currentRoles = await UserManager.GetRolesAsync(user);
            var newRoles = roles.Except(currentRoles);

            if (newRoles.Count() > 0)
            {
                var result = await UserManager.AddToRolesAsync(user, newRoles);
                if (!result.Succeeded)
                {
                    HandleIdentityErrors(result.Errors);
                }
            }
        }

        #endregion

        #region Interfaces

        public override Task ExecuteAsync() => UpdateUsersAsync();

        #endregion
    }
}
