using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvTaskConsole
{
    public class CsvDbContext : DbContext
    {
        public DbSet<CsvModel> CsvModels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            MyConfiguration config = new MyConfiguration();
            var connection = config.GetConnectionString();
            optionsBuilder.UseSqlServer(connection);
        }
    }
}
