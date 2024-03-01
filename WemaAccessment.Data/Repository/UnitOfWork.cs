using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WemaAccessment.Data.Data;
using WemaAccessment.Data.IRepository;

namespace WemaAccessment.Data.Repository
{
  public class UnitOfWork : IUnitOfWork
  {
    private readonly DataContext _context;
    private ICustomerRepository _customerRepository;
    public UnitOfWork(DataContext context, ICustomerRepository customerRepository)
	{
      _context = context;
      _customerRepository = customerRepository;
    }
    public ICustomerRepository CustomerRepository => _customerRepository ??= new CustomerRepository(_context);
    public void Dispose()
    {
      _context.Dispose();
      GC.SuppressFinalize(this);
    }

    public async Task<int> SaveAsync()
    {
      return await _context.SaveChangesAsync();
    }
  }
}
