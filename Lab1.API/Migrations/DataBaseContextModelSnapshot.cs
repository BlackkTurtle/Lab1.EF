﻿// <auto-generated />
using Lab1.API.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Lab1.API.Migrations
{
    [DbContext(typeof(DataBaseContext))]
    partial class DataBaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.5");

            modelBuilder.Entity("Lab1.API.Entities.Table1", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("table1");
                });

            modelBuilder.Entity("Lab1.API.Entities.Table2", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Table1Id")
                        .HasColumnType("INTEGER");

                    b.HasKey("id");

                    b.HasIndex("Table1Id");

                    b.ToTable("table2");
                });

            modelBuilder.Entity("Lab1.API.Entities.Table2", b =>
                {
                    b.HasOne("Lab1.API.Entities.Table1", "Table1")
                        .WithMany()
                        .HasForeignKey("Table1Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Table1");
                });
#pragma warning restore 612, 618
        }
    }
}
