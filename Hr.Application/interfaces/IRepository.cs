using System;

namespace Hr.Application.interfaces;

public interface IRepository<T> where T : class
{
      Task<T?> FindAsync(int id);
      Task AddAsync(T entity);

}
