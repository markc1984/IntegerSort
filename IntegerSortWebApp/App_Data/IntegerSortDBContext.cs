using IntegerSortWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace IntegerSortWebApp.App_Data
{
    public class IntegerSortDBContext :DbContext
    {
        public IntegerSortDBContext(DbContextOptions<IntegerSortDBContext> options) : base(options)
        {

        }

        public DbSet<Sort> Sorts { get; set; }
        public DbSet<Number> Numbers { get; set; }
    }
}
