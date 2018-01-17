using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace f14.AspNetCore.Identity
{
    /// <summary>
    /// Represents an end point for any Identity updater. The implementation of this interface must update some identity data.
    /// </summary>
    public interface IUpdaterExecutor
    {
        /// <summary>
        /// This method must executes certain update action.
        /// </summary>
        /// <returns>Action as task.</returns>
        Task ExecuteAsync();
    }
    /// <summary>
    /// Represents a start point for any Identity updater. The implementation of this interface must store input identity data.
    /// </summary>
    /// <typeparam name="TInfo">Type of some info class.</typeparam>
    /// <typeparam name="TEntity">Type of identity target class. Roles, Users, etc.</typeparam>
    public interface IDataStore<TInfo, TEntity> where TEntity : class
    {
        /// <summary>
        /// Adds the data to store.
        /// </summary>
        /// <param name="items">The initial data.</param>
        /// <returns>The entity factory.</returns>
        IEntityFactory<TInfo, TEntity> ForData(IEnumerable<TInfo> items);
    }
    /// <summary>
    /// Represents a part of fluent api for any Identity updater. The implementation of this interface must store identity entity factory.
    /// </summary>
    /// <typeparam name="TInfo">Type of some info class.</typeparam>
    /// <typeparam name="TEntity">Type of identity target class. Roles, Users, etc.</typeparam>
    public interface IEntityFactory<TInfo, TEntity> where TEntity : class
    {
        /// <summary>
        /// Sets entity factory.
        /// </summary>
        /// <param name="factory">The entity factory.</param>
        /// <returns>A updater executor.</returns>
        IUpdaterExecutor Generate(Func<TInfo, TEntity> factory);
    }
}
