using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace f14.AspNetCore.Identity
{
    /// <summary>
    /// The role updater impl.
    /// </summary>
    /// <typeparam name="TRole">Type of identity role.</typeparam>
    public class RoleUpdater<TRole> : IdentityUpdater where TRole : class
    {
        private RoleManager<TRole> RoleManager { get; set; }
        private IEnumerable<IRoleInfo> Roles { get; set; }
        private Func<IRoleInfo, TRole> RoleFactory { get; set; }

        public RoleUpdater(RoleManager<TRole> roleManager, IEnumerable<IRoleInfo> roles, Func<IRoleInfo, TRole> roleFactory)
        {
            RoleManager = roleManager;
            Roles = roles;
            RoleFactory = roleFactory;
        }

        #region Main

        /// <summary>
        /// Update roles via RoleManager.
        /// </summary>
        /// <returns>A update action task.</returns>
        private async Task UpdateRolesAsync()
        {
            IdentityResult result;
            foreach (var roleInfo in Roles)
            {
                bool exist = await RoleManager.RoleExistsAsync(roleInfo.Name);
                if (!exist)
                {
                    var role = RoleFactory(roleInfo);
                    result = await RoleManager.CreateAsync(role);

                    if (result.Succeeded)
                    {
                        foreach (var claim in roleInfo.Claims)
                        {
                            result = await RoleManager.AddClaimAsync(role, new Claim(claim.Type, claim.Value));
                            if (!result.Succeeded)
                            {
                                HandleIdentityErrors(result.Errors);
                            }
                        }
                    }
                    else
                    {
                        HandleIdentityErrors(result.Errors);
                    }
                }
                else
                {
                    var role = await RoleManager.FindByNameAsync(roleInfo.Name);
                    var claims = await RoleManager.GetClaimsAsync(role);

                    foreach (var claim in roleInfo.Claims)
                    {
                        if (claims.Any(x => x.Type == claim.Type && x.Value == claim.Value))
                        {
                            continue;
                        }
                        else
                        {
                            result = await RoleManager.AddClaimAsync(role, new Claim(claim.Type, claim.Value));
                            if (!result.Succeeded)
                            {
                                HandleIdentityErrors(result.Errors);
                            }
                        }
                    }
                }
            }
        }

        #endregion

        #region Interfaces

        public override Task ExecuteAsync() => UpdateRolesAsync();

        #endregion
    }
}
