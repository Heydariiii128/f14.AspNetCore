using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace f14.AspNetCore.Data
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TDbContext"></typeparam>
    public interface IDbContextRepositoryBase<T, TDbContext> : IObjectRepository<T>
        where T : class
        where TDbContext : DbContext
    {
        /// <summary>
        /// 
        /// </summary>
        TDbContext DbContext { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="onDeleted"></param>
        void Clear(Action<int> onDeleted);
    }
}
