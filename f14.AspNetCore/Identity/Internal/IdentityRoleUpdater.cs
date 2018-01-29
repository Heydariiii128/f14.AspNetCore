using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace f14.AspNetCore.Identity.Internal
{
    /// <summary>
    /// The role updater impl.
    /// </summary>
    /// <typeparam name="TInfo">Type of role info.</typeparam>
    /// <typeparam name="TRole">Type of identity role.</typeparam>
    internal class IdentityRoleUpdater<TInfo, TRole> : IdentityUpdater, IDataStore<TInfo, TRole>, IEntityFactory<TInfo, TRole>
        where TInfo : RoleInfo
        where TRole : class
    {
        private RoleManager<TRole> RoleManager { get; set; }
        private IEnumerable<TInfo> Roles { get; set; }
        private Func<TInfo, TRole> RoleFactory { get; set; }
        /// <summary>
        /// Creates new instance of updater.
        /// </summary>
        /// <param name="roleManager">The role manager.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        public IdentityRoleUpdater(RoleManager<TRole> roleManager, ILoggerFactory loggerFactory)
            : base(loggerFactory)
        {
            RoleManager = roleManager;
        }

        #region Private
        /// <summary>
        /// Update roles via RoleManager.
        /// </summary>
        /// <returns>A update action task.</returns>
        private async Task UpdateRolesAsync()
        {
            foreach (var roleInfo in Roles)
            {
                bool exist = await RoleManager.RoleExistsAsync(roleInfo.Name);
                if (!exist)
                {
                    var role = RoleFactory(roleInfo);
                    await RoleManager.CreateAsync(role);

                    foreach (var claim in roleInfo.Claims)
                    {
                        await RoleManager.AddClaimAsync(role, new Claim(claim.Type, claim.Value));
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
                            await RoleManager.AddClaimAsync(role, new Claim(claim.Type, claim.Value));
                        }
                    }
                }
            }
        }

        #endregion

        #region FluentApi

        public IEntityFactory<TInfo, TRole> ForData(IEnumerable<TInfo> items)
        {
            Roles = items;
            return this;
        }

        public IUpdaterExecutor Generate(Func<TInfo, TRole> factory)
        {
            RoleFactory = factory;
            return this;
        }

        public override Task ExecuteAsync()
        {
            return UpdateRolesAsync();
        }

        #endregion
    }
}
