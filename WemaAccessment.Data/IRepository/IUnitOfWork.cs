using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WemaAccessment.Data.IRepository
{
  public interface IUnitOfWork : IDisposable
  {
    ICustomerRepository CustomerRepository { get; }
    Task<int> SaveAsync();
  }
}
