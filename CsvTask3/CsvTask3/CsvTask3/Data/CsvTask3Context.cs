using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CsvTask3.Models;

namespace CsvTask3.Data
{
    public class CsvTask3Context : DbContext
    {
        public CsvTask3Context (DbContextOptions<CsvTask3Context> options)
            : base(options)
        {
        }

        public DbSet<CsvTask3.Models.CsvModel> CsvModel { get; set; } = default!;
    }
}
