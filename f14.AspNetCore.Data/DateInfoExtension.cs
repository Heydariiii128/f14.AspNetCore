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
        /// Sets the <see cref="DateTime.Now"/> to the <see cref="IDateInfo.Created"/> property with seconds and milliseconds equal to zero.
        /// </summary>
        /// <param name="source">The source object.</param>
        public static void SetCreatedDateNoSecMills(this IDateInfo source) => source.SetCreatedDate(now => new TimeSpan(0, 0, 0, -now.Second, -now.Millisecond));
        /// <summary>
        /// Sets the <see cref="DateTime.Now"/> to the <see cref="IDateInfo.Created"/> property.
        /// </summary>
        /// <param name="source">The source object.</param>
        /// <param name="mod">The date time modificator.</param>
        public static void SetCreatedDate(this IDateInfo source, Func<DateTime, TimeSpan> mod)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            var now = DateTime.Now;
            var tsMod = mod(now);
            source.Created = now.Add(tsMod);
        }
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
