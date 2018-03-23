using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace f14.AspNetCore.Data
{
    /// <summary>
    /// Base implementation of the <see cref="IObjectRepository{T}"/>.
    /// </summary>
    /// <typeparam name="T">Type of repo objects.</typeparam>
    public abstract class ObjectRepository<T> : IObjectRepository<T> where T : class
    {
        /// <inheritdoc />
        public IQueryable<T> Table { get; }

        /// <summary>
        /// Create new instance of repository impl.
        /// </summary>
        /// <param name="table">The queryable collection.</param>
        protected ObjectRepository(IQueryable<T> table)
        {
            Table = table;
        }

        /// <inheritdoc />
        public virtual int Count() => Table.Count();
        /// <inheritdoc />
        public virtual int Count(Expression<Func<T, bool>> filter) => Table.Count(filter);
        /// <inheritdoc />
        public virtual T Get(Expression<Func<T, bool>> selector) => Table.FirstOrDefault(selector);
        /// <inheritdoc />
        public virtual List<T> GetAll() => Table.ToList();
        /// <inheritdoc />
        public virtual List<T> GetAll(Expression<Func<T, bool>> filter) => Table.Where(filter).ToList();
    }
}
