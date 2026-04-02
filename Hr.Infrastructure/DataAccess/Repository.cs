using System;
using Hr.Application.interfaces;
using Hr.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Hr.Infrastructure.DataAccess;

public class Repository<T>(DatabaseContext ctx) : IRepository<T> where T : class
{
      readonly DatabaseContext context = ctx;
      public async Task AddAsync(T entity)
      {
            await context.Set<T>().AddAsync(entity);
      }

      public async Task<T?> FindAsync(int id)
      {
            return await context.Set<T>().FindAsync(id);
      }
}
