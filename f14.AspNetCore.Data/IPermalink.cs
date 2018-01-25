using System;
using System.Collections.Generic;
using System.Text;

namespace f14.AspNetCore.Data
{
    /// <summary>
    /// Provides a base interface for data model that should support permalink value and using <see cref="int"/> as type of key.
    /// </summary>
    public interface IPermalink : IPermalink<int>
    {

    }
    /// <summary>
    /// Provides a base interface for data model that should support permalink value.
    /// </summary>
    /// <typeparam name="TKey">Type of data model key.</typeparam>
    public interface IPermalink<TKey>
    {
        /// <summary>
        /// Gets a data model key.
        /// </summary>
        /// <returns>The key of current model.</returns>
        TKey GetId();
        /// <summary>
        /// Gets a data model permalink.
        /// </summary>
        /// <returns>The permalink of current model.</returns>
        string GetPermalink();
    }
}
