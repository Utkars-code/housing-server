using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using webApi_build_Real.Models;

namespace webApi_build_Real.ApplictionContext
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) 
              : base(options) { }

        public  DbSet<City>Cities { get; set; }
        public DbSet<Usercs> Usercs { get; set; }
    }
}
