using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options) 
        {
        
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Education> Educations { get; set; }

        public DbSet<Profiling> Profilings { get; set; }
        public DbSet<University> Universities { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasOne(a => a.Account)
                .WithOne(b => b.Employee)
                .HasForeignKey<Account>(c => c.NIK)
                .IsRequired();
    
            modelBuilder.Entity<Account>()
                .HasOne(a => a.Profiling)
                .WithOne(b => b.Account)
                .HasForeignKey<Profiling>(b => b.NIK)
                .IsRequired(); 

            modelBuilder.Entity<Education>()
                .HasMany(c => c.Profiling)
                .WithOne(e => e.Education)
                .IsRequired(); 

            modelBuilder.Entity<University>()
                .HasMany(c => c.Education)
                .WithOne(e => e.University)
                .IsRequired(); 
        }
    }
}
