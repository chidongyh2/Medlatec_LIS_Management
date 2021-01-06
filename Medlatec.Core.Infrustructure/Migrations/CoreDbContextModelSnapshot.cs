﻿// <auto-generated />
using System;
using Medlatec.Core.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Oracle.EntityFrameworkCore.Metadata;

namespace Medlatec.Core.Infrastructure.Migrations
{
    [DbContext(typeof(CoreDbContext))]
    partial class CoreDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Oracle:ValueGenerationStrategy", OracleValueGenerationStrategy.IdentityColumn)
                .HasAnnotation("ProductVersion", "3.1.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            modelBuilder.Entity("Medlatec.Core.Domain.AggregateModels.TenantAggregate.Page", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)")
                        .HasAnnotation("Oracle:ValueGenerationStrategy", OracleValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ChildCount")
                        .HasColumnType("NUMBER(10)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("CreatedById")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("TIMESTAMP(7) WITH TIME ZONE");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(500)")
                        .HasMaxLength(500);

                    b.Property<string>("Icon")
                        .HasColumnType("NVARCHAR2(250)")
                        .HasMaxLength(250);

                    b.Property<string>("IdPath")
                        .HasColumnType("NVARCHAR2(250)")
                        .HasMaxLength(250);

                    b.Property<bool>("IsActive")
                        .HasColumnType("NUMBER(1)");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("NUMBER(1)");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("NUMBER(1)");

                    b.Property<bool>("IsShowSidebar")
                        .HasColumnType("NUMBER(1)");

                    b.Property<string>("LastUpdatedBy")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("LastUpdatedById")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<DateTimeOffset>("LastUpdatedDate")
                        .HasColumnType("TIMESTAMP(7) WITH TIME ZONE");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(250)")
                        .HasMaxLength(250);

                    b.Property<int>("Order")
                        .HasColumnType("NUMBER(10)");

                    b.Property<string>("OrderPath")
                        .HasColumnType("NVARCHAR2(250)")
                        .HasMaxLength(250);

                    b.Property<int?>("ParentId")
                        .HasColumnType("NUMBER(10)");

                    b.Property<int>("Type")
                        .HasColumnType("NUMBER(10)");

                    b.Property<string>("UnsignName")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("Url")
                        .HasColumnType("NVARCHAR2(500)")
                        .HasMaxLength(500);

                    b.HasKey("Id");

                    b.ToTable("Pages");
                });

            modelBuilder.Entity("Medlatec.Core.Domain.AggregateModels.TenantAggregate.Tenant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("RAW(16)");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(500)")
                        .HasMaxLength(500);

                    b.Property<string>("AdminPortal")
                        .HasColumnType("NVARCHAR2(500)")
                        .HasMaxLength(500);

                    b.Property<string>("CreatedBy")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("CreatedById")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("TIMESTAMP(7) WITH TIME ZONE");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(250)")
                        .HasMaxLength(250);

                    b.Property<bool>("IsActive")
                        .HasColumnType("NUMBER(1)");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("NUMBER(1)");

                    b.Property<string>("LastUpdatedBy")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("LastUpdatedById")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<DateTimeOffset>("LastUpdatedDate")
                        .HasColumnType("TIMESTAMP(7) WITH TIME ZONE");

                    b.Property<string>("LearnerPortal")
                        .HasColumnType("NVARCHAR2(500)")
                        .HasMaxLength(500);

                    b.Property<string>("Logo")
                        .HasColumnType("NVARCHAR2(500)")
                        .HasMaxLength(500);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(500)")
                        .HasMaxLength(500);

                    b.Property<string>("Note")
                        .HasColumnType("NVARCHAR2(500)")
                        .HasMaxLength(500);

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(15)")
                        .HasMaxLength(15);

                    b.Property<string>("UnsignName")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(500)")
                        .HasMaxLength(500);

                    b.HasKey("Id");

                    b.ToTable("Tenants");
                });

            modelBuilder.Entity("Medlatec.Core.Domain.AggregateModels.TenantAggregate.TenantPage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("RAW(16)");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("NUMBER(1)");

                    b.Property<int>("PageId")
                        .HasColumnType("NUMBER(10)");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("RAW(16)");

                    b.HasKey("Id");

                    b.HasIndex("PageId");

                    b.HasIndex("TenantId");

                    b.ToTable("TenantPages");
                });

            modelBuilder.Entity("Medlatec.Infrastructure.Domain.AccountAggregate.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("RAW(16)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("NUMBER(10)");

                    b.Property<string>("Address")
                        .HasColumnType("NVARCHAR2(500)")
                        .HasMaxLength(500);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("Email")
                        .HasColumnType("NVARCHAR2(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("NUMBER(1)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(250)")
                        .HasMaxLength(250);

                    b.Property<bool>("IsActive")
                        .HasColumnType("NUMBER(1)");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("NUMBER(1)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(250)")
                        .HasMaxLength(250);

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("NUMBER(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TIMESTAMP(7) WITH TIME ZONE");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("NVARCHAR2(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("NVARCHAR2(256)")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("NUMBER(1)");

                    b.Property<string>("ProfileImageUrl")
                        .HasColumnType("NVARCHAR2(500)")
                        .HasMaxLength(500);

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("RAW(16)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("NUMBER(1)");

                    b.Property<string>("UserName")
                        .HasColumnType("NVARCHAR2(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("Medlatec.Infrastructure.Domain.AccountAggregate.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("RAW(16)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("Description")
                        .HasColumnType("NVARCHAR2(500)")
                        .HasMaxLength(500);

                    b.Property<string>("Name")
                        .HasColumnType("NVARCHAR2(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("NVARCHAR2(256)")
                        .HasMaxLength(256);

                    b.Property<Guid>("TenantId")
                        .HasColumnType("RAW(16)");

                    b.Property<string>("Type")
                        .HasColumnType("NVARCHAR2(250)")
                        .HasMaxLength(250);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Medlatec.Infrastructure.Domain.AccountAggregate.RolePage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("RAW(16)");

                    b.Property<int>("PageId")
                        .HasColumnType("NUMBER(10)");

                    b.Property<int>("Permissions")
                        .HasColumnType("NUMBER(10)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("RAW(16)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RolePages");
                });

            modelBuilder.Entity("Medlatec.Infrastructure.Domain.SystemAggregate.District", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("RAW(16)");

                    b.Property<string>("Culture")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(150)")
                        .HasMaxLength(150);

                    b.Property<Guid>("ProvinceId")
                        .HasColumnType("RAW(16)");

                    b.Property<string>("ProvinceName")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(150)")
                        .HasMaxLength(150);

                    b.Property<string>("UnsignName")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(150)")
                        .HasMaxLength(150);

                    b.HasKey("Id");

                    b.HasIndex("ProvinceId");

                    b.ToTable("Districts");
                });

            modelBuilder.Entity("Medlatec.Infrastructure.Domain.SystemAggregate.Ethnic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)")
                        .HasAnnotation("Oracle:ValueGenerationStrategy", OracleValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<Guid>("NationalId")
                        .HasColumnType("RAW(16)");

                    b.Property<string>("UnsignName")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.HasKey("Id");

                    b.HasIndex("NationalId");

                    b.ToTable("Ethnics");
                });

            modelBuilder.Entity("Medlatec.Infrastructure.Domain.SystemAggregate.National", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("RAW(16)");

                    b.Property<string>("Name")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("UnsignName")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.HasKey("Id");

                    b.ToTable("Nationals");
                });

            modelBuilder.Entity("Medlatec.Infrastructure.Domain.SystemAggregate.Province", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("RAW(16)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(250)")
                        .HasMaxLength(250);

                    b.Property<Guid?>("NationalId")
                        .HasColumnType("RAW(16)");

                    b.Property<string>("UnsignName")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(250)")
                        .HasMaxLength(250);

                    b.HasKey("Id");

                    b.HasIndex("NationalId");

                    b.ToTable("Provinces");
                });

            modelBuilder.Entity("Medlatec.Infrastructure.Domain.SystemAggregate.Religion", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("RAW(16)");

                    b.Property<string>("Name")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<Guid>("NationalId")
                        .HasColumnType("RAW(16)");

                    b.Property<string>("UnsignName")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.HasKey("Id");

                    b.HasIndex("NationalId");

                    b.ToTable("Religions");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)")
                        .HasAnnotation("Oracle:ValueGenerationStrategy", OracleValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("RAW(16)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)")
                        .HasAnnotation("Oracle:ValueGenerationStrategy", OracleValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("RAW(16)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("NVARCHAR2(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("NVARCHAR2(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("RAW(16)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("RAW(16)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("RAW(16)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.HasKey("UserId", "RoleId");

                    b.ToTable("AspNetUserRoles");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUserRole<Guid>");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("RAW(16)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("NVARCHAR2(450)");

                    b.Property<string>("Name")
                        .HasColumnType("NVARCHAR2(450)");

                    b.Property<string>("Value")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Medlatec.Infrastructure.Domain.AccountAggregate.AccountRole", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>");

                    b.HasIndex("RoleId");

                    b.HasDiscriminator().HasValue("AccountRole");
                });

            modelBuilder.Entity("Medlatec.Core.Domain.AggregateModels.TenantAggregate.TenantPage", b =>
                {
                    b.HasOne("Medlatec.Core.Domain.AggregateModels.TenantAggregate.Page", "Page")
                        .WithMany("TenantPages")
                        .HasForeignKey("PageId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Medlatec.Core.Domain.AggregateModels.TenantAggregate.Tenant", "Tenant")
                        .WithMany("TenantPages")
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Medlatec.Infrastructure.Domain.AccountAggregate.RolePage", b =>
                {
                    b.HasOne("Medlatec.Infrastructure.Domain.AccountAggregate.Role", "Role")
                        .WithMany("RolePages")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Medlatec.Infrastructure.Domain.SystemAggregate.District", b =>
                {
                    b.HasOne("Medlatec.Infrastructure.Domain.SystemAggregate.Province", "Province")
                        .WithMany("Districts")
                        .HasForeignKey("ProvinceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Medlatec.Infrastructure.Domain.SystemAggregate.Ethnic", b =>
                {
                    b.HasOne("Medlatec.Infrastructure.Domain.SystemAggregate.National", "National")
                        .WithMany("Ethnics")
                        .HasForeignKey("NationalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Medlatec.Infrastructure.Domain.SystemAggregate.Province", b =>
                {
                    b.HasOne("Medlatec.Infrastructure.Domain.SystemAggregate.National", "National")
                        .WithMany("Provinces")
                        .HasForeignKey("NationalId");
                });

            modelBuilder.Entity("Medlatec.Infrastructure.Domain.SystemAggregate.Religion", b =>
                {
                    b.HasOne("Medlatec.Infrastructure.Domain.SystemAggregate.National", "National")
                        .WithMany("Religions")
                        .HasForeignKey("NationalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Medlatec.Infrastructure.Domain.AccountAggregate.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("Medlatec.Infrastructure.Domain.AccountAggregate.Account", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("Medlatec.Infrastructure.Domain.AccountAggregate.Account", null)
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("Medlatec.Infrastructure.Domain.AccountAggregate.Account", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Medlatec.Infrastructure.Domain.AccountAggregate.AccountRole", b =>
                {
                    b.HasOne("Medlatec.Infrastructure.Domain.AccountAggregate.Role", "Role")
                        .WithMany("AccountRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Medlatec.Infrastructure.Domain.AccountAggregate.Account", "Account")
                        .WithMany("AccountRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
