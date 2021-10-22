﻿using EmployeeAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAPI.Context
{
	public class MyContext : DbContext
	{
		public MyContext(DbContextOptions<MyContext> options) : base(options)
		{

		}

		public DbSet<Employee> Employees { get; set; }
		public DbSet<Account> Accounts { get; set; }
		public DbSet<Profiling> Profilings { get; set; }
		public DbSet<Education> Educations { get; set; }
		public DbSet<University> Universities { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//  Account With Employee
			modelBuilder.Entity<Employee>()
					.HasOne(emp => emp.Acc)
					.WithOne(acc => acc.Employee)
					.HasForeignKey<Account>(fk => fk.NIK);
			// Profiling With Account
			modelBuilder.Entity<Account>()
					.HasOne(acc => acc.Profiling)
					.WithOne(prf => prf.Account)
					.HasForeignKey<Profiling>(fk => fk.NIK);
			// Profiling dg Education
			modelBuilder.Entity<Education>()
					.HasMany(c => c.Profiling)
					.WithOne(e => e.Education);
			// Education dg University
			modelBuilder.Entity<University>()
				.HasMany(c => c.Education)
				.WithOne(e => e.University);
		}
	}
}
