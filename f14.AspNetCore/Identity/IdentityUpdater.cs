using f14.AspNetCore.Identity.Internal;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace f14.AspNetCore.Identity
{
    /// <summary>
    /// Provides a abstract class for identity data updater. Also, provides specific implementations.
    /// </summary>
    public abstract class IdentityUpdater : IUpdaterExecutor
    {
        /// <summary>
        /// Retruns the logger.
        /// </summary>
        protected ILogger Log { get; private set; }

        /// <summary>
        /// Default ctor with log.
        /// </summary>
        /// <param name="loggerFactory">The logger factory.</param>
        protected IdentityUpdater(ILoggerFactory loggerFactory)
        {
            Log = loggerFactory.CreateLogger(GetType());
        }
        /// <summary>
        /// Executes updater actions.
        /// </summary>
        /// <returns>A updater task.</returns>
        public abstract Task ExecuteAsync();

        #region Static

        /// <summary>
        /// Creates new instance of <see cref="IdentityUpdater"/> for role updates.
        /// </summary>
        /// <typeparam name="TRole">Type of role object.</typeparam>
        /// <param name="roleManager">The role manager.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        /// <returns>A first object in the fluent api.</returns>
        public static IDataStore<RoleInfo, TRole> CreateRoleUpdater<TRole>(RoleManager<TRole> roleManager, ILoggerFactory loggerFactory) where TRole : class
            => CreateRoleUpdater<RoleInfo, TRole>(roleManager, loggerFactory);
        /// <summary>
        /// Creates new instance of <see cref="IdentityUpdater"/> for user updates.
        /// </summary>
        /// <typeparam name="TUser">Type of user object.</typeparam>
        /// <param name="userManager">The role manager.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        /// <returns>A first object in the fluent api.</returns>
        public static IDataStore<UserInfo, TUser> CreateUserUpdater<TUser>(UserManager<TUser> userManager, ILoggerFactory loggerFactory) where TUser : class
            => CreateUserUpdater<UserInfo, TUser>(userManager, loggerFactory);
        /// <summary>
        /// Creates new instance of <see cref="IdentityUpdater"/> for role updates.
        /// </summary>
        /// <typeparam name="TInfo">Type of info object.</typeparam>
        /// <typeparam name="TRole">Type of role object.</typeparam>
        /// <param name="roleManager">The role manager.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        /// <returns>A first object in the fluent api.</returns>
        public static IDataStore<TInfo, TRole> CreateRoleUpdater<TInfo, TRole>(RoleManager<TRole> roleManager, ILoggerFactory loggerFactory)
            where TInfo : RoleInfo
            where TRole : class
            => new IdentityRoleUpdater<TInfo, TRole>(roleManager, loggerFactory);
        /// <summary>
        /// Creates new instance of <see cref="IdentityUpdater"/> for user updates.
        /// </summary>
        /// <typeparam name="TInfo">Type of info object.</typeparam>
        /// <typeparam name="TUser">Type of user object.</typeparam>
        /// <param name="userManager">The role manager.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        /// <returns>A first object in the fluent api.</returns>
        public static IDataStore<TInfo, TUser> CreateUserUpdater<TInfo, TUser>(UserManager<TUser> userManager, ILoggerFactory loggerFactory)
            where TInfo : UserInfo
            where TUser : class
            => new IdentityUserUpdater<TInfo, TUser>(userManager, loggerFactory);

        #endregion
    }
}
