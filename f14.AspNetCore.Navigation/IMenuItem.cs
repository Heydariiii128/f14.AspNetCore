using System;
using System.Collections.Generic;
using System.Text;

namespace f14.AspNetCore.Navigation
{
    /// <summary>
    /// The base interface for menu items.
    /// </summary>
    /// <typeparam name="T">The specific menu item type.</typeparam>
    public interface IMenuItem<T> where T : IMenuItem<T>
    {
        /// <summary>
        /// The header of the object.
        /// </summary>
        string Header { get; }
        /// <summary>
        /// Returns a nested menu items.
        /// </summary>
        ICollection<T> Items { get; }
        /// <summary>
        /// Adds menu item to the nested collection.
        /// </summary>
        /// <param name="item">The menu item to add.</param>
        /// <returns>The current instance.</returns>
        T Add(T item);
    }
}
