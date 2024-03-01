using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WemaAccessment.Data.IRepository
{
  public interface IRepositoryGeneric<T> where T : class
  {
    Task<T> Get(int id);
    Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>,
        IOrderedQueryable<T>> orderBy = null, string includeProperties = null);
    Task<T> Add(T entity);
    Task<T> FirstOrDefault(Expression<Func<T, bool>> filter = null, string includeProperties = null);
    void Update(T entity);
  }
}

