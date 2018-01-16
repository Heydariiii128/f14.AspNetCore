using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.Text;

namespace f14.AspNetCore.Extensions
{
    /// <summary>
    /// Provides an extensions methods for work with Html.
    /// </summary>
    public static class HtmlExtension
    {
        /// <summary>
        /// Create <see cref="IHtmlContent"/> from given string.
        /// </summary>
        /// <param name="source">The source string.</param>
        /// <returns>The <see cref="IHtmlContent"/> as result.</returns>
        public static IHtmlContent AsHtml(this string source) => new HtmlString(source);
    }
}
