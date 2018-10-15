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
    public class DbContextRepositoryBase<T, TDbContext> : ObjectRepository<T>, IDbContextRepositoryBase<T, TDbContext>
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="onDeleted"></param>
        public void Clear(Action<int> onDeleted)
        {
            List<T> toDel;
            while ((toDel = Table.Take(100).ToList()).Count > 0)
            {
                DbContext.RemoveRange(toDel);
                int deleted = DbContext.SaveChanges();
                onDeleted(deleted);
            }
        }
    }
}
