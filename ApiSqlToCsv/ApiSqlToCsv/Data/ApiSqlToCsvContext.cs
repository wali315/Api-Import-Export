using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ApiSqlToCsv.Models;

namespace ApiSqlToCsv.Data
{
    public class ApiSqlToCsvContext : DbContext
    {
        public ApiSqlToCsvContext (DbContextOptions<ApiSqlToCsvContext> options)
            : base(options)
        {
        }

        public DbSet<ApiSqlToCsv.Models.CsvModel> CsvModel { get; set; } = default!;
    }
}
