using System;
using System.Collections.Generic;
using System.Text;

namespace f14.AspNetCore.Data
{
    /// <summary>
    /// The base interface for object that must support create and modify date.
    /// </summary>
    /// <typeparam name="T">Type of date type.</typeparam>
    public interface IDateInfo<T>
    {
        T Created { get; set; }
        T Modified { get; set; }
    }
}
