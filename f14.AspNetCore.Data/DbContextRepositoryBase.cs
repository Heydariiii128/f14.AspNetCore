using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace f14.AspNetCore.Data
{
    public class DbContextRepositoryBase<T, TDbContext> : ObjectRepository<T>, IDbContextRepositoryBase<T, TDbContext>
        where T : class
        where TDbContext : DbContext
    {
        public DbContextRepositoryBase(TDbContext dbContext) : base(dbContext.Set<T>())
        {
            DbContext = dbContext;
        }

        public TDbContext DbContext { get; }

        public virtual int Add(T o)
        {
            DbContext.Add(o);
            return DbContext.SaveChanges();
        }

        public virtual int AddRange(IEnumerable<T> list)
        {
            DbContext.AddRange(list);
            return DbContext.SaveChanges();
        }

        public virtual int Delete(T o)
        {
            DbContext.Remove(o);
            return DbContext.SaveChanges();
        }

        public virtual int DeleteRange(IEnumerable<T> list)
        {
            DbContext.RemoveRange(list);
            return DbContext.SaveChanges();
        }

        public virtual int Update(T o)
        {
            return 0;
        }

        public virtual int Update(T to, T from)
        {
            return 0;
        }

        public virtual void Clear(Action<int> onDeleted)
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
