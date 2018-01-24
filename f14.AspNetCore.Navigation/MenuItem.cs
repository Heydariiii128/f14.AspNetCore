using System;
using System.Collections.Generic;

namespace f14.AspNetCore.Navigation
{
    /// <summary>
    /// Represents as base navigation menu item.
    /// </summary>
    public class MenuItem
    {
        /// <summary>
        /// The header of the object.
        /// </summary>
        public string Header { get; set; }
        /// <summary>
        /// The target url.
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// The element icon.
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// The nested items.
        /// </summary>
        public ICollection<MenuItem> Items { get; set; } = new List<MenuItem>();
        /// <summary>
        /// Adds nested item.
        /// </summary>
        /// <param name="item">An item to add.</param>
        /// <returns>The current object.</returns>
        public MenuItem Add(MenuItem item)
        {
            ExHelper.NotNull(() => item);

            Items.Add(item);
            return this;
        }
    }
}
