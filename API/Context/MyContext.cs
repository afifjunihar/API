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
        public DbSet<AccountRole> AccountRoles { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasOne(a => a.Account)
                .WithOne(b => b.Employee)
                .HasForeignKey<Account>(c => c.NIK);

            modelBuilder.Entity<Account>()
                .HasOne(a => a.Profiling)
                .WithOne(b => b.Account)
                .HasForeignKey<Profiling>(b => b.NIK); 

            modelBuilder.Entity<Education>()
                .HasMany(c => c.Profiling)
                .WithOne(e => e.Education);

            modelBuilder.Entity<University>()
                .HasMany(c => c.Education)
                .WithOne(e => e.University);

            modelBuilder.Entity<AccountRole>()
                .HasOne(a => a.Account)
                .WithMany(b => b.AccountRole);

            modelBuilder.Entity<AccountRole>()
                .HasOne(a => a.Role)
                .WithMany(b => b.AccountRole);


        }
    }
}
