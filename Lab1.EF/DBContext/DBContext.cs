using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab1.EF.Entities;
using Microsoft.EntityFrameworkCore;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace Lab1.EF.DataBaseContext
{
    public class DataBaseContext:DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext>option):base(option)
        {

        }
        public DbSet<Table1> table1 { get; set; }
        public DbSet<Table2> table2 { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Table2>()
                .HasOne(t => t.Table1)
                .WithMany()
                .HasForeignKey(t => t.Table1Id);
        }
    }
}
