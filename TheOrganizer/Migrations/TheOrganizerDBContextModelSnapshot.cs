﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TheOrganizer.Model;

namespace TheOrganizer.Migrations
{
    [DbContext(typeof(TheOrganizerDBContext))]
    partial class TheOrganizerDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TheOrganizer.Model.Calendar", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsDisplayed");

                    b.Property<int>("OwnerId");

                    b.Property<string>("Title")
                        .HasMaxLength(25);

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Calendars");
                });

            modelBuilder.Entity("TheOrganizer.Model.Contact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<string>("Name");

                    b.Property<int>("OwnerId");

                    b.Property<string>("PhoneNumber");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("TheOrganizer.Model.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CalendarId");

                    b.Property<string>("Description");

                    b.Property<DateTime>("End")
                        .HasColumnName("EndTime");

                    b.Property<DateTime>("Start")
                        .HasColumnName("StartTime");

                    b.Property<string>("Tag");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("CalendarId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("TheOrganizer.Model.Note", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreated");

                    b.Property<int>("NotebookId");

                    b.Property<string>("Text");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("NotebookId");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("TheOrganizer.Model.Notebook", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("OwnerId");

                    b.Property<string>("Title")
                        .HasMaxLength(25);

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Notebooks");
                });

            modelBuilder.Entity("TheOrganizer.Model.Todo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsDone");

                    b.Property<string>("Text");

                    b.Property<string>("Title")
                        .HasMaxLength(25);

                    b.Property<int>("TodoListId");

                    b.HasKey("Id");

                    b.HasIndex("TodoListId");

                    b.ToTable("Todos");
                });

            modelBuilder.Entity("TheOrganizer.Model.TodoList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("OwnerId");

                    b.Property<string>("Title")
                        .HasMaxLength(25);

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("TodoLists");
                });

            modelBuilder.Entity("TheOrganizer.Model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TheOrganizer.Model.Calendar", b =>
                {
                    b.HasOne("TheOrganizer.Model.User", "User")
                        .WithMany("Calendars")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TheOrganizer.Model.Contact", b =>
                {
                    b.HasOne("TheOrganizer.Model.User", "User")
                        .WithMany("Contacts")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TheOrganizer.Model.Event", b =>
                {
                    b.HasOne("TheOrganizer.Model.Calendar", "Calendar")
                        .WithMany("Events")
                        .HasForeignKey("CalendarId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TheOrganizer.Model.Note", b =>
                {
                    b.HasOne("TheOrganizer.Model.Notebook", "Notebook")
                        .WithMany("Notes")
                        .HasForeignKey("NotebookId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TheOrganizer.Model.Notebook", b =>
                {
                    b.HasOne("TheOrganizer.Model.User", "User")
                        .WithMany("Notebooks")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TheOrganizer.Model.Todo", b =>
                {
                    b.HasOne("TheOrganizer.Model.TodoList", "TodoList")
                        .WithMany("Todos")
                        .HasForeignKey("TodoListId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TheOrganizer.Model.TodoList", b =>
                {
                    b.HasOne("TheOrganizer.Model.User", "User")
                        .WithMany("TodoLists")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
