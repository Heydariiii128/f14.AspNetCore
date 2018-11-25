using System;
using System.Collections.Generic;
using System.Text;

namespace f14.AspNetCore.Mvc
{
    /// <summary>
    /// Provides helpful methods for MVC framework.
    /// </summary>
    public static class MvcHelper
    {
        /// <summary>
        /// Gets name of the type. Also, extracts controller name without 'Controller' word.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <returns>The name of the object.</returns>
        public static string GetName<T>() => GetName(typeof(T));
        /// <summary>
        /// Gets name of the type. Also, extracts controller name without 'Controller' word.
        /// </summary>
        /// <param name="type">The sepcified type.</param>
        /// <returns>The name of the object.</returns>
        public static string GetName(Type type)
        {
            ExHelper.NotNull(() => type);
            string name = type.Name;
            if (name.EndsWith("Controller"))
                return name.Replace("Controller", "");
            else
                return name;
        }
    }
}
