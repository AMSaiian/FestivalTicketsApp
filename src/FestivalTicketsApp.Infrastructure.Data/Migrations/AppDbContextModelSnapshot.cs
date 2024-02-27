﻿// <auto-generated />
using System;
using FestivalTicketsApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FestivalTicketsApp.Infrastructure.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ClientEvent", b =>
                {
                    b.Property<int>("AddedToFavouriteById")
                        .HasColumnType("int");

                    b.Property<int>("FavouriteEventsId")
                        .HasColumnType("int");

                    b.HasKey("AddedToFavouriteById", "FavouriteEventsId");

                    b.HasIndex("FavouriteEventsId");

                    b.ToTable("ClientEvent");
                });

            modelBuilder.Entity("FestivalTicketsApp.Core.Entities.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("PhoneNumber")
                        .IsUnique();

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("FestivalTicketsApp.Core.Entities.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("EventStatusId")
                        .HasColumnType("int");

                    b.Property<int>("EventTypeId")
                        .HasColumnType("int");

                    b.Property<int>("HostId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("EventStatusId");

                    b.HasIndex("EventTypeId");

                    b.HasIndex("HostId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("FestivalTicketsApp.Core.Entities.EventDetails", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("EventDetails");
                });

            modelBuilder.Entity("FestivalTicketsApp.Core.Entities.EventStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Status")
                        .IsUnique();

                    b.ToTable("EventStatuses");
                });

            modelBuilder.Entity("FestivalTicketsApp.Core.Entities.EventType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("EventTypes");
                });

            modelBuilder.Entity("FestivalTicketsApp.Core.Entities.Host", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("HostTypeId")
                        .HasColumnType("int");

                    b.Property<int>("LocationId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("HostTypeId");

                    b.HasIndex("LocationId");

                    b.ToTable("Hosts");
                });

            modelBuilder.Entity("FestivalTicketsApp.Core.Entities.HostDetails", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDividedBySeats")
                        .HasColumnType("bit");

                    b.Property<int>("RowAmount")
                        .HasColumnType("int");

                    b.Property<int>("SeatsInRow")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("HostDetails", t =>
                        {
                            t.HasCheckConstraint("CK_rowAmount", "[RowAmount] > 0");

                            t.HasCheckConstraint("CK_seatsInRow", "[SeatsInRow] > 0");
                        });
                });

            modelBuilder.Entity("FestivalTicketsApp.Core.Entities.HostType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("HostTypes");
                });

            modelBuilder.Entity("FestivalTicketsApp.Core.Entities.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BuildingNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("CityName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<double>("Latitude")
                        .HasColumnType("float");

                    b.Property<double>("Longitude")
                        .HasColumnType("float");

                    b.Property<string>("StreetName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("CityName", "StreetName", "BuildingNumber")
                        .IsUnique();

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("FestivalTicketsApp.Core.Entities.Ticket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("ClientId")
                        .HasColumnType("int");

                    b.Property<int>("RowNum")
                        .HasColumnType("int");

                    b.Property<int>("SeatNum")
                        .HasColumnType("int");

                    b.Property<int>("TicketStatusId")
                        .HasColumnType("int");

                    b.Property<int>("TicketTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("TicketStatusId");

                    b.HasIndex("TicketTypeId");

                    b.ToTable("Tickets", t =>
                        {
                            t.HasCheckConstraint("CK_rowNum", "[RowNum] > 0");

                            t.HasCheckConstraint("CK_seatNum", "[SeatNum] > 0");
                        });
                });

            modelBuilder.Entity("FestivalTicketsApp.Core.Entities.TicketStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Status")
                        .IsUnique();

                    b.ToTable("TicketStatuses");
                });

            modelBuilder.Entity("FestivalTicketsApp.Core.Entities.TicketType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("EventId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("Name", "EventId")
                        .IsUnique();

                    b.ToTable("TicketTypes", t =>
                        {
                            t.HasCheckConstraint("CK_price", "[Price] > 0");
                        });
                });

            modelBuilder.Entity("FestivalTicketsApp.Core.Entities.TypeGenre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("EventTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("EventTypeId");

                    b.HasIndex("Genre", "EventTypeId")
                        .IsUnique();

                    b.ToTable("TypeGenres");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("ClientEvent", b =>
                {
                    b.HasOne("FestivalTicketsApp.Core.Entities.Client", null)
                        .WithMany()
                        .HasForeignKey("AddedToFavouriteById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FestivalTicketsApp.Core.Entities.Event", null)
                        .WithMany()
                        .HasForeignKey("FavouriteEventsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FestivalTicketsApp.Core.Entities.Event", b =>
                {
                    b.HasOne("FestivalTicketsApp.Core.Entities.EventStatus", "EventStatus")
                        .WithMany("EventsWithStatus")
                        .HasForeignKey("EventStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FestivalTicketsApp.Core.Entities.EventType", "EventType")
                        .WithMany("EventsWithType")
                        .HasForeignKey("EventTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FestivalTicketsApp.Core.Entities.Host", "Host")
                        .WithMany("HostedEvents")
                        .HasForeignKey("HostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EventStatus");

                    b.Navigation("EventType");

                    b.Navigation("Host");
                });

            modelBuilder.Entity("FestivalTicketsApp.Core.Entities.EventDetails", b =>
                {
                    b.HasOne("FestivalTicketsApp.Core.Entities.Event", null)
                        .WithOne("EventDetails")
                        .HasForeignKey("FestivalTicketsApp.Core.Entities.EventDetails", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FestivalTicketsApp.Core.Entities.Host", b =>
                {
                    b.HasOne("FestivalTicketsApp.Core.Entities.HostType", "HostType")
                        .WithMany("Hosts")
                        .HasForeignKey("HostTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FestivalTicketsApp.Core.Entities.Location", "Location")
                        .WithMany("Hosts")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HostType");

                    b.Navigation("Location");
                });

            modelBuilder.Entity("FestivalTicketsApp.Core.Entities.HostDetails", b =>
                {
                    b.HasOne("FestivalTicketsApp.Core.Entities.Host", null)
                        .WithOne("Details")
                        .HasForeignKey("FestivalTicketsApp.Core.Entities.HostDetails", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FestivalTicketsApp.Core.Entities.Ticket", b =>
                {
                    b.HasOne("FestivalTicketsApp.Core.Entities.Client", "Client")
                        .WithMany("PurchasedTickets")
                        .HasForeignKey("ClientId");

                    b.HasOne("FestivalTicketsApp.Core.Entities.TicketStatus", "TicketStatus")
                        .WithMany("TicketsWithStatus")
                        .HasForeignKey("TicketStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FestivalTicketsApp.Core.Entities.TicketType", "TicketType")
                        .WithMany("TicketsWithType")
                        .HasForeignKey("TicketTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("TicketStatus");

                    b.Navigation("TicketType");
                });

            modelBuilder.Entity("FestivalTicketsApp.Core.Entities.TicketType", b =>
                {
                    b.HasOne("FestivalTicketsApp.Core.Entities.Event", "Event")
                        .WithMany("TicketTypes")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");
                });

            modelBuilder.Entity("FestivalTicketsApp.Core.Entities.TypeGenre", b =>
                {
                    b.HasOne("FestivalTicketsApp.Core.Entities.EventType", "EventType")
                        .WithMany("EventTypeGenres")
                        .HasForeignKey("EventTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EventType");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<int>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("FestivalTicketsApp.Core.Entities.Client", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("FestivalTicketsApp.Core.Entities.Client", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<int>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FestivalTicketsApp.Core.Entities.Client", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("FestivalTicketsApp.Core.Entities.Client", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FestivalTicketsApp.Core.Entities.Client", b =>
                {
                    b.Navigation("PurchasedTickets");
                });

            modelBuilder.Entity("FestivalTicketsApp.Core.Entities.Event", b =>
                {
                    b.Navigation("EventDetails")
                        .IsRequired();

                    b.Navigation("TicketTypes");
                });

            modelBuilder.Entity("FestivalTicketsApp.Core.Entities.EventStatus", b =>
                {
                    b.Navigation("EventsWithStatus");
                });

            modelBuilder.Entity("FestivalTicketsApp.Core.Entities.EventType", b =>
                {
                    b.Navigation("EventTypeGenres");

                    b.Navigation("EventsWithType");
                });

            modelBuilder.Entity("FestivalTicketsApp.Core.Entities.Host", b =>
                {
                    b.Navigation("Details")
                        .IsRequired();

                    b.Navigation("HostedEvents");
                });

            modelBuilder.Entity("FestivalTicketsApp.Core.Entities.HostType", b =>
                {
                    b.Navigation("Hosts");
                });

            modelBuilder.Entity("FestivalTicketsApp.Core.Entities.Location", b =>
                {
                    b.Navigation("Hosts");
                });

            modelBuilder.Entity("FestivalTicketsApp.Core.Entities.TicketStatus", b =>
                {
                    b.Navigation("TicketsWithStatus");
                });

            modelBuilder.Entity("FestivalTicketsApp.Core.Entities.TicketType", b =>
                {
                    b.Navigation("TicketsWithType");
                });
#pragma warning restore 612, 618
        }
    }
}
