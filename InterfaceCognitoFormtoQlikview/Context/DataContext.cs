using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using InterfaceSingTaxitoSQL.Model;

namespace InterfaceSingTaxitoSQL.Context
{
    public class DataContext : DbContext
    {
        public DataContext() : base("Conn")
        {

        }
        public DbSet<TaxiHistory> TaxiHistories { get; set; }

        
    }
}
