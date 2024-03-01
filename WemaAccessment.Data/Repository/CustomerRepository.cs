using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WemaAccessment.Data.Data;
using WemaAccessment.Data.IRepository;
using WemaAccessment.Data.Models;

namespace WemaAccessment.Data.Repository
{
  public class CustomerRepository : RepositoryGeneric<Customer>, ICustomerRepository
  {
    private readonly DataContext _context;
    public CustomerRepository(DataContext context) : base(context)
    {
      _context = context;
    }
  }
}
