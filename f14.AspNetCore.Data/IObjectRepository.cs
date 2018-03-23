using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace f14.AspNetCore.Data
{
    /// <summary>
    /// Provides the interface for a data repository with an access methods.
    /// </summary>
    /// <typeparam name="T">Type of repository objects.</typeparam>
    public interface IObjectRepository<T> where T : class
    {
        /// <summary>
        /// Returns the queryable table collection.
        /// </summary>
        IQueryable<T> Table { get; }
        /// <summary>
        /// Returns all table objects.
        /// </summary>
        /// <returns>The all table objects.</returns>
        List<T> GetAll();
        /// <summary>
        /// Returns the table objects which satisfy the filter.
        /// </summary>
        /// <param name="filter">The objects filter.</param>
        /// <returns>The filtered collection.</returns>
        List<T> GetAll(Expression<Func<T, bool>> filter);
        /// <summary>
        /// Returns the single object by selector.
        /// </summary>
        /// <param name="selector">The object selector.</param>
        /// <returns>The single object.</returns>
        T Get(Expression<Func<T, bool>> selector);
        /// <summary>
        /// Returns the count of objects in the table.
        /// </summary>
        /// <returns>The count of objects.</returns>
        int Count();
        /// <summary>
        /// Returns the count of objects in the table using the given filter.
        /// </summary>
        /// <param name="filter">The objects filter.</param>
        /// <returns>The count of filtered objects.</returns>
        int Count(Expression<Func<T, bool>> filter);
    }
}
