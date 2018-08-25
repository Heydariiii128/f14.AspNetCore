using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace f14.AspNetCore.Data
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TDbContext"></typeparam>
    public class DbContextRepositoryBase<T, TDbContext> : ObjectRepository<T>
        where T : class
        where TDbContext : DbContext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        public DbContextRepositoryBase(TDbContext dbContext) : base(dbContext.Set<T>())
        {
            DbContext = dbContext;
        }
        /// <summary>
        /// 
        /// </summary>
        public TDbContext DbContext { get; }
    }
}
