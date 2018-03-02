using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace f14.AspNetCore.Navigation
{
    /// <summary>
    /// Represents the navigation menu.
    /// </summary>
    public sealed class NavigationMenu : NavigationMenu<MenuItem>
    {

    }
    /// <summary>
    /// Represents the navigation menu.
    /// </summary>
    /// <typeparam name="T">The specific menu item type.</typeparam>
    public class NavigationMenu<T> where T : IMenuItem<T>
    {
        /// <summary>
        /// Navigation items.
        /// </summary>
        public List<T> Items { get; set; } = new List<T>();
        /// <summary>
        /// Adds navigation item to the menu.
        /// </summary>
        /// <param name="item">An menu item.</param>
        /// <returns>The current object.</returns>
        public NavigationMenu<T> Add(T item)
        {
            Items.Add(item);
            return this;
        }
    }
}
