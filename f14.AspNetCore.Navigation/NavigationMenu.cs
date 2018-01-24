using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace f14.AspNetCore.Navigation
{
    /// <summary>
    /// Represents the navigation menu.
    /// </summary>
    public class NavigationMenu
    {
        /// <summary>
        /// Navigation items.
        /// </summary>
        public List<MenuItem> Items { get; set; } = new List<MenuItem>();
        /// <summary>
        /// Adds navigation item to the menu.
        /// </summary>
        /// <param name="item">An menu item.</param>
        /// <returns>The current object.</returns>
        public NavigationMenu Add(MenuItem item)
        {
            Items.Add(item);
            return this;
        }
    }
}
