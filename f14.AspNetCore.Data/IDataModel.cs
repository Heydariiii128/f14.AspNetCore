using System;

namespace f14.AspNetCore.Data
{
    /// <summary>
    /// Provides a base interface for data model with <see cref="Id"/> property as key.
    /// </summary>
    /// <typeparam name="TKey">Type of model key.</typeparam>
    public interface IDataModel<TKey>
    {
        /// <summary>
        /// The data model key.
        /// </summary>
        TKey Id { get; set; }
    }
}
