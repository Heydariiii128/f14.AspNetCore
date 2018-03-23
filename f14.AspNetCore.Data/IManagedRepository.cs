using System;
using System.Collections.Generic;
using System.Text;

namespace f14.AspNetCore.Data
{
    /// <summary>
    /// Provides the interface for manage objects. This interface include: Insert, Update, Delete methods.
    /// </summary>
    /// <typeparam name="T">Type of repo objects.</typeparam>
    public interface IManagedRepository<T> : IObjectRepository<T> where T : class
    {
        /// <summary>
        /// Addes an object into the db.
        /// </summary>
        /// <param name="o">The object to add to the db.</param>
        /// <returns>The affected rows.</returns>
        int Insert(T o);
        /// <summary>
        /// Addesan objects into the db.
        /// </summary>
        /// <param name="list">The objects to add to the db.</param>
        /// <returns>The affected rows.</returns>
        int InsertRange(IEnumerable<T> list);
        /// <summary>
        /// Updates an object in the db.
        /// </summary>
        /// <param name="o">The data source object.</param>
        /// <returns>The affected rows.</returns>
        int Update(T o);
        /// <summary>
        /// Deletes an object from db.
        /// </summary>
        /// <param name="o">The object to delete.</param>
        /// <returns>The affected rows.</returns>
        int Delete(T o);
        /// <summary>
        /// Deletes an objects from db.
        /// </summary>
        /// <param name="list">The objects to delete.</param>
        /// <returns>The affected rows.</returns>
        int DeleteRange(IEnumerable<T> list);
    }
}
