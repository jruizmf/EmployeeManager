using DataAccessLayer.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Reflection.Emit;

namespace DataAccessLayer
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options) { }

        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeeType> EmployeeTypes { get; set; }
        public virtual DbSet<Payroll> Payrolls { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Employee>().Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Entity<EmployeeType>().Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Entity<Payroll>().Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Entity<Employee>().HasOne(x => x.EmployeeType).WithOne().HasForeignKey<EmployeeType>("EmployeeTypeId");
           
            builder.Entity<Payroll>().HasOne(x => x.Employee).WithMany(x => x.Payrolls).HasForeignKey("EmployeeId");

            builder.Entity<Employee>(entity => {
                entity.HasIndex(e => e.RFC).IsUnique();
            });
            builder.Entity<EmployeeType>().HasData(
            new EmployeeType
            {
                Id= Guid.NewGuid(),
                Name = "Local",
                FirstBonus = 30,
                HasSecondBonus = true,
                SecondBonus= 60,
                HoursWorked = 48
            }, new EmployeeType
            {
                Id = Guid.NewGuid(),
                Name = "Externo",
                FirstBonus = 50,
                HasSecondBonus = false,
                HoursWorked = 40
            }
        );

        }
        public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DBContext>
        {
            public DBContext CreateDbContext(string[] args)
            {
              
                var builder = new DbContextOptionsBuilder<DBContext>();
                var connectionString = ConfigurationManager.ConnectionStrings["DuventusApp"].ConnectionString;

                builder.UseSqlServer(connectionString);

                return new DBContext(builder.Options);
            }
        }
    }

}
