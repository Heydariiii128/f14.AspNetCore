using System;
using System.Collections.Generic;

namespace f14.AspNetCore.Navigation
{
    /// <summary>
    /// Represents as base navigation menu item.
    /// </summary>
    public sealed class MenuItem : IMenuItem<MenuItem>
    {
        /// <summary>
        /// Creates new instance of menu item.
        /// </summary>
        public MenuItem()
        {
        }
        /// <summary>
        /// Creates new instance of menu item.
        /// </summary>
        /// <param name="header">The menu item header.</param>
        public MenuItem(string header)
        {
            Header = header;
        }
        /// <summary>
        /// Creates new instance of menu item.
        /// </summary>
        /// <param name="header">The menu item header.</param>
        /// <param name="url">The target url.</param>
        public MenuItem(string header, string url)
        {
            Header = header;
            Url = url;
        }
        /// <summary>
        /// Creates new instance of menu item.
        /// </summary>
        /// <param name="header">The menu item header.</param>
        /// <param name="url">The target url.</param>
        /// <param name="icon">The menu item icon.</param>
        public MenuItem(string header, string url, string icon)
        {
            Header = header;
            Url = url;
            Icon = icon;
        }
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
        /// <param name="header">The menu item header.</param>
        /// <returns>The current object.</returns>
        public MenuItem Add(string header) => Add(new MenuItem(header));
        /// <summary>
        /// Adds nested item.
        /// </summary>
        /// <param name="header">The menu item header.</param>
        /// <param name="url">The target url.</param>
        /// <returns>The current object.</returns>
        public MenuItem Add(string header, string url) => Add(new MenuItem(header, url));
        /// <summary>
        /// Adds nested item.
        /// </summary>
        /// <param name="header">The menu item header.</param>
        /// <param name="url">The target url.</param>
        /// <param name="icon">The menu item icon.</param>
        /// <returns>The current object.</returns>
        public MenuItem Add(string header, string url, string icon) => Add(new MenuItem(header, url, icon));
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
