using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace f14.AspNetCore.Identity.Internal
{
    /// <summary>
    /// The user updater imlp.
    /// </summary>
    /// <typeparam name="TInfo">Type of user info.</typeparam>
    /// <typeparam name="TUser">Type of identity user.</typeparam>
    internal class IdentityUserUpdater<TInfo, TUser> : IdentityUpdater, IDataStore<TInfo, TUser>, IEntityFactory<TInfo, TUser>
        where TInfo : UserInfo
        where TUser : class
    {
        private UserManager<TUser> UserManager { get; set; }
        private IEnumerable<TInfo> Users { get; set; }
        private Func<TInfo, TUser> UserFactory { get; set; }
        /// <summary>
        /// Creates new instance of updater.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        public IdentityUserUpdater(UserManager<TUser> userManager, ILoggerFactory loggerFactory)
            : base(loggerFactory)
        {
            UserManager = userManager;
        }

        #region Private
        /// <summary>
        /// Updates users via UserManager.
        /// </summary>
        /// <returns>A update action task.</returns>
        private async Task UpdateUsersAsync()
        {
            foreach (var userInfo in Users)
            {
                var user = await UserManager.FindByNameAsync(userInfo.Login);
                if (user == null)
                {
                    user = UserFactory(userInfo);
                    var result = await UserManager.CreateAsync(user, userInfo.Password);

                    if (result.Succeeded)
                    {
                        Log.LogInformation($"New user created with login: {userInfo.Login}");
                    }
                    else
                    {
                        Log.LogError("Cannot create user for: " + userInfo.Login);
                        foreach (var e in result.Errors)
                        {
                            Log.LogError($"Code: {e.Code} Description: {e.Description}");
                        }
                    }
                    await UpdateUserRolesAsync(user, userInfo.Roles);
                }
                else
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
            List<string> rList = new List<string>();
            foreach (string role in roles)
            {
                if (!(await UserManager.IsInRoleAsync(user, role)))
                {
                    rList.Add(role);
                }
            }

            var result = await UserManager.AddToRolesAsync(user, rList);
            if (!result.Succeeded)
            {
                Log.LogError("Error has occurred while adding a role to the user.");
                foreach (var e in result.Errors)
                {
                    Log.LogError($"Code: {e.Code} Description: {e.Description}");
                }
            }
        }

        #endregion

        #region Fluent

        public IEntityFactory<TInfo, TUser> ForData(IEnumerable<TInfo> items)
        {
            Users = items;
            return this;
        }

        public IUpdaterExecutor Generate(Func<TInfo, TUser> factory)
        {
            UserFactory = factory;
            return this;
        }

        public override Task ExecuteAsync()
        {
            return UpdateUsersAsync();
        }

        #endregion
    }
}
