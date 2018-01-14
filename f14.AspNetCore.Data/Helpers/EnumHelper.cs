using f14.AspNetCore.Extensions;
using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace f14.AspNetCore.Helpers
{
    /// <summary>
    /// Provides an extensions methods for work with enum.
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// Gets a <see cref="DisplayAttribute"/> for enum field..
        /// </summary>
        /// <param name="enumObj">An enum field.</param>
        /// <returns>The attribute or null.</returns>
        public static DisplayAttribute GetDisplayAttribute(object enumObj)
        {
            return AttributeUtil.GetAttributeFromEnum<DisplayAttribute>(enumObj);
        }
        /// <summary>
        /// Extracts the <see cref="DisplayAttribute.Name"/> and create new <see cref="HtmlString"/>.
        /// </summary>
        /// <param name="enumValue">The enum field.</param>
        /// <returns>The html content.</returns>
        public static IHtmlContent DisplayFor(object enumValue)
        {
            ExHelper.NotNull(() => enumValue);
            return GetDisplayAttribute(enumValue)?.Name.AsHtml() ?? new HtmlString(enumValue.ToString());
        }
    }
}
