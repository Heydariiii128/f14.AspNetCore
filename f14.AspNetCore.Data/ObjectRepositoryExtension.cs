using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace f14.AspNetCore.Data
{
    public static class ObjectRepositoryExtension
    {
        public static List<T> GetAllAsNoTracking<T>(this IObjectRepository<T> repository) where T : class
            => repository.Table.AsNoTracking().ToList();

        public static List<T> GetAllAsNoTracking<T>(this IObjectRepository<T> repository, Expression<Func<T, bool>> filter) where T : class
            => repository.Table.Where(filter).AsNoTracking().ToList();

        public static T GetAsNoTracking<T>(this IObjectRepository<T> repository, Expression<Func<T, bool>> selector) where T : class
            => repository.Table.AsNoTracking().FirstOrDefault(selector);
    }
}
