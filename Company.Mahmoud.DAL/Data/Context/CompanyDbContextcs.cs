using Company.Mahmoud.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Company.Mahmoud.DAL.Data.Context
{
   public class CompanyDbContextcs:DbContext
    {
        public CompanyDbContextcs(DbContextOptions <CompanyDbContextcs> Options) : base(Options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
            
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //    optionsBuilder.UseSqlServer("Server=. ;Database=Company;Trusted_Connection=True;TrustServerCertificate=True");
        //}
        public DbSet<Department> Departments { get; set; }

    }
}
