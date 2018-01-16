using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using f14.AspNetCore.Identity.Internal;

namespace f14.AspNetCore.Identity
{
    /// <summary>
    /// Provides a abstract class for identity data updater. Also, provides specific implementations.
    /// </summary>
    public abstract class IdentityUpdater : IUpdaterExecutor
    {
        protected ILogger Log { get; set; }

        /// <summary>
        /// Default ctor with log.
        /// </summary>
        /// <param name="log">Logger instance.</param>
        protected IdentityUpdater(ILogger log)
        {
            Log = log;
        }
        /// <summary>
        /// Executes updater actions.
        /// </summary>
        /// <returns>A updater task.</returns>
        public abstract Task ExecuteAsync();
        /// <summary>
        /// Creates new instance of <see cref="IdentityUpdater"/> for role updates.
        /// </summary>
        /// <typeparam name="TRole">Type of role object.</typeparam>
        /// <param name="serviceProvider">A services.</param>
        /// <returns>A first object in the fluent api.</returns>
        public static IDataStore<RoleInfo, TRole> CreateRoleUpdater<TRole>(IServiceProvider serviceProvider) where TRole : class
            => CreateRoleUpdater<RoleInfo, TRole>(serviceProvider);
        /// <summary>
        /// Creates new instance of <see cref="IdentityUpdater"/> for user updates.
        /// </summary>
        /// <typeparam name="TUser">Type of user object.</typeparam>
        /// <param name="serviceProvider">A services.</param>
        /// <returns>A first object in the fluent api.</returns>
        public static IDataStore<UserInfo, TUser> CreateUserUpdate<TUser>(IServiceProvider serviceProvider) where TUser : class
            => CreateUserUpdate<UserInfo, TUser>(serviceProvider);
        /// <summary>
        /// Creates new instance of <see cref="IdentityUpdater"/> for role updates.
        /// </summary>
        /// <typeparam name="TInfo">Type of info object.</typeparam>
        /// <typeparam name="TRole">Type of role object.</typeparam>
        /// <param name="serviceProvider">A services.</param>
        /// <returns>A first object in the fluent api.</returns>
        public static IDataStore<TInfo, TRole> CreateRoleUpdater<TInfo, TRole>(IServiceProvider serviceProvider)
            where TInfo : RoleInfo
            where TRole : class
            => new IdentityRoleUpdater<TInfo, TRole>(serviceProvider);
        /// <summary>
        /// Creates new instance of <see cref="IdentityUpdater"/> for user updates.
        /// </summary>
        /// <typeparam name="TInfo">Type of info object.</typeparam>
        /// <typeparam name="TUser">Type of user object.</typeparam>
        /// <param name="serviceProvider">A services.</param>
        /// <returns>A first object in the fluent api.</returns>
        public static IDataStore<TInfo, TUser> CreateUserUpdate<TInfo, TUser>(IServiceProvider serviceProvider)
            where TInfo : UserInfo
            where TUser : class
            => new IdentityUserUpdater<TInfo, TUser>(serviceProvider);
    }
}
