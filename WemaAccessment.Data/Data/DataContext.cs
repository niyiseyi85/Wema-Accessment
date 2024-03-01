using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WemaAccessment.Data.Models;

namespace WemaAccessment.Data.Data
{
  public class DataContext : DbContext
  {
    public DataContext(DbContextOptions<DataContext> options): base(options) { }
    public virtual DbSet<Customer> Customers { get; set; }
  }
}
