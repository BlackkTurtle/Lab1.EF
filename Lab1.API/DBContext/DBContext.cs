using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab1.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace Lab1.API.DBContext
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> option) : base(option)
        {

        }
        public DbSet<Table1> table1 { get; set; }
        public DbSet<Table2> table2 { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=database.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Table2>()
                .HasOne(t => t.Table1)
                .WithMany()
                .HasForeignKey(t => t.Table1Id);
        }
    }

}
