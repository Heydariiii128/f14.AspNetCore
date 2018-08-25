using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace f14.AspNetCore.Identity
{
    /// <summary>
    /// Represents an end point for any Identity updater.
    /// </summary>
    public interface IUpdaterExecutor
    {
        /// <summary>
        /// This method must executes certain update action.
        /// </summary>
        /// <returns>Action as task.</returns>
        Task ExecuteAsync();
    }    
}
