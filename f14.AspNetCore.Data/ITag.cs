using System;
using System.Collections.Generic;
using System.Text;

namespace f14.AspNetCore.Data
{
    /// <summary>
    /// Provides the basic interface for the tag data model with the primary key type <see cref="Int32"/>.
    /// </summary>
    public interface ITag : ITag<int>
    {

    }

    /// <summary>
    /// Provides the basic interface for the tag data model.
    /// </summary>
    /// <typeparam name="TKey">Type of data model key.</typeparam>
    public interface ITag<TKey> : IDataModel<TKey>
    {
        /// <summary>
        /// The tag name.
        /// </summary>
        string Name { get; set; }
    }
}
