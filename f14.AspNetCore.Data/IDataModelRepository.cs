using System;
using System.Collections.Generic;
using System.Text;

namespace f14.AspNetCore.Data
{
    /// <summary>
    /// Represents a repository which can serve object which implements <see cref="IDataModel{TKey}"/> interface.
    /// </summary>
    /// <typeparam name="T">Type of repository object.</typeparam>
    /// <typeparam name="TKey">Type of object key.</typeparam>
    public interface IDataModelRepository<T, TKey> : IObjectRepository<T>
        where T : class, IDataModel<TKey>
    {
        /// <summary>
        /// Gets an object with given key.
        /// </summary>
        /// <param name="id">Object key.</param>
        /// <returns>Object or null.</returns>
        T Get(TKey id);
    }
}
