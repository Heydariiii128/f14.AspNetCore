using System;
using System.Collections.Generic;
using System.Text;

namespace f14.AspNetCore.Data
{
    /// <summary>
    /// Provides an extension methods for <see cref="IDateInfo"/> objects.
    /// </summary>
    public static class DateInfoExtension
    {
        /// <summary>
        /// Updates the <see cref="IDateInfo.Modified"/> property using <see cref="DateTime.Now"/> in the source object.
        /// </summary>
        /// <param name="source">The source object to be update.</param>
        public static void UpModifiedDate(this IDateInfo source) => source.UpModifiedDate(DateTime.Now);
        /// <summary>
        /// Updates the <see cref="IDateInfo.Modified"/> property in the source object.
        /// </summary>
        /// <param name="source">The source object to be update.</param>
        /// <param name="date">The desired date.</param>
        public static void UpModifiedDate(this IDateInfo source, DateTime date)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            source.Modified = date;
        }
    }
}
