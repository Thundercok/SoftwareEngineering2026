using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Exer1DbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public Exer1DbContext() : base("name=MyConn")
        { }
    }
}
