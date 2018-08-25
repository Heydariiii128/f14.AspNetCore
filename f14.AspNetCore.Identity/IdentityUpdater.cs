using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace f14.AspNetCore.Identity
{
    /// <summary>
    /// Provides a abstract class for identity data updater. Also, provides specific implementations.
    /// </summary>
    public abstract class IdentityUpdater : IUpdaterExecutor
    {
        private IdentityErrorHandler _IEHandler;

        /// <summary>
        /// Sets the action that will be fired when some errors an occurred.
        /// </summary>
        /// <param name="handler">The error handler.</param>
        /// <returns>The current instance.</returns>
        public IdentityUpdater OnError(IdentityErrorHandler handler)
        {
            _IEHandler = handler;
            return this;
        }
        /// <summary>
        /// Executes updater actions.
        /// </summary>
        /// <returns>A updater task.</returns>
        public abstract Task ExecuteAsync();

        protected void HandleIdentityErrors(IEnumerable<IdentityError> errors)
        {
            if (errors != null)
            {
                foreach (var error in errors)
                {
                    _IEHandler?.Invoke(error);
                }
            }
        }        
    }
}
