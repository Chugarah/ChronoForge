﻿// <auto-generated />
using System;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Infrastructure.Entities.CustomersEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ContactPersonId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("ContactPersonId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("Name", "ContactPersonId")
                        .IsUnique()
                        .HasDatabaseName("IX_Customers_Name_ContactPersonId");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ContactPersonId = 1,
                            Name = "No Contact Person"
                        });
                });

            modelBuilder.Entity("Infrastructure.Entities.PaymentTypeEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("PaymentType");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Currency = "None",
                            Name = "No Roles"
                        },
                        new
                        {
                            Id = 2,
                            Currency = "SEK",
                            Name = "Hourly Rate"
                        },
                        new
                        {
                            Id = 3,
                            Currency = "SEK",
                            Name = "Per Project"
                        },
                        new
                        {
                            Id = 4,
                            Currency = "SEK",
                            Name = "Other Type"
                        });
                });

            modelBuilder.Entity("Infrastructure.Entities.ProjectsEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 100L);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly?>("EndDate")
                        .HasColumnType("date");

                    b.Property<int>("ProjectManager")
                        .HasColumnType("int");

                    b.Property<DateOnly>("StartDate")
                        .HasColumnType("date");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(75)");

                    b.HasKey("Id");

                    b.HasIndex("StatusId");

                    b.HasIndex("ProjectManager", "Title")
                        .IsUnique();

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("Infrastructure.Entities.RolesEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "No Roles"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Project Manager"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Customer"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Client"
                        });
                });

            modelBuilder.Entity("Infrastructure.Entities.ServiceContractsEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(150)");

                    b.Property<int>("PaymentTypeId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("money");

                    b.HasKey("Id");

                    b.HasIndex("PaymentTypeId");

                    b.HasIndex("CustomerId", "PaymentTypeId", "Name")
                        .IsUnique();

                    b.ToTable("ServiceContracts");
                });

            modelBuilder.Entity("Infrastructure.Entities.StatusEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Status");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "No Status"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Not Started"
                        },
                        new
                        {
                            Id = 3,
                            Name = "In Progress"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Completed"
                        });
                });

            modelBuilder.Entity("Infrastructure.Entities.UsersEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("FirstName", "LastName")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FirstName = "No User is set",
                            LastName = "Starcraft"
                        });
                });

            modelBuilder.Entity("ProjectServiceContracts", b =>
                {
                    b.Property<int>("ProjectsEntityId")
                        .HasColumnType("int");

                    b.Property<int>("ServiceContractsEntityId")
                        .HasColumnType("int");

                    b.HasKey("ProjectsEntityId", "ServiceContractsEntityId");

                    b.HasIndex("ServiceContractsEntityId");

                    b.ToTable("ProjectServiceContracts", (string)null);
                });

            modelBuilder.Entity("RolesEntityUsersEntity", b =>
                {
                    b.Property<int>("RolesEntityId")
                        .HasColumnType("int");

                    b.Property<int>("UsersEntityId")
                        .HasColumnType("int");

                    b.HasKey("RolesEntityId", "UsersEntityId");

                    b.HasIndex("UsersEntityId");

                    b.ToTable("RolesEntityUsersEntity");
                });

            modelBuilder.Entity("Infrastructure.Entities.CustomersEntity", b =>
                {
                    b.HasOne("Infrastructure.Entities.UsersEntity", "UsersEntity")
                        .WithMany()
                        .HasForeignKey("ContactPersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UsersEntity");
                });

            modelBuilder.Entity("Infrastructure.Entities.ProjectsEntity", b =>
                {
                    b.HasOne("Infrastructure.Entities.UsersEntity", "UsersEntity")
                        .WithMany()
                        .HasForeignKey("ProjectManager")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Infrastructure.Entities.StatusEntity", "StatusEntity")
                        .WithMany("ProjectsEntity")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("StatusEntity");

                    b.Navigation("UsersEntity");
                });

            modelBuilder.Entity("Infrastructure.Entities.ServiceContractsEntity", b =>
                {
                    b.HasOne("Infrastructure.Entities.CustomersEntity", "CustomersEntity")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Infrastructure.Entities.PaymentTypeEntity", "PaymentTypeEntity")
                        .WithMany()
                        .HasForeignKey("PaymentTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CustomersEntity");

                    b.Navigation("PaymentTypeEntity");
                });

            modelBuilder.Entity("ProjectServiceContracts", b =>
                {
                    b.HasOne("Infrastructure.Entities.ProjectsEntity", null)
                        .WithMany()
                        .HasForeignKey("ProjectsEntityId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Infrastructure.Entities.ServiceContractsEntity", null)
                        .WithMany()
                        .HasForeignKey("ServiceContractsEntityId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("RolesEntityUsersEntity", b =>
                {
                    b.HasOne("Infrastructure.Entities.RolesEntity", null)
                        .WithMany()
                        .HasForeignKey("RolesEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Infrastructure.Entities.UsersEntity", null)
                        .WithMany()
                        .HasForeignKey("UsersEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Infrastructure.Entities.StatusEntity", b =>
                {
                    b.Navigation("ProjectsEntity");
                });
#pragma warning restore 612, 618
        }
    }
}
