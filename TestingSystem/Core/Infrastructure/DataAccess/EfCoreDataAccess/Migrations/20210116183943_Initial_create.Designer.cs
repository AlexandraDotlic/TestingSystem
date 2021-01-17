﻿// <auto-generated />
using Core.Infrastructure.DataAccess.EfCoreDataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Core.Infrastructure.DataAccess.EfCoreDataAccess.Migrations
{
    [DbContext(typeof(CoreEfCoreDbContext))]
    [Migration("20210116183943_Initial_create")]
    partial class Initial_create
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("Core.Domain.Entites.Examiner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("FirstName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Examiners");
                });

            modelBuilder.Entity("Core.Domain.Entites.Group", b =>
                {
                    b.Property<short>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint")
                        .UseIdentityColumn();

                    b.Property<int>("ExaminerId")
                        .HasColumnType("int");

                    b.Property<short>("TestId")
                        .HasColumnType("smallint");

                    b.Property<string>("Title")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("ExaminerId");

                    b.HasIndex("TestId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("Core.Domain.Entites.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Answer")
                        .HasMaxLength(5000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QuestionText")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.HasKey("Id");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("Core.Domain.Entites.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("FirstName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("Core.Domain.Entites.StudentGroup", b =>
                {
                    b.Property<short>("GroupId")
                        .HasColumnType("smallint");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<string>("StudentResponse")
                        .HasMaxLength(10000)
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GroupId", "StudentId");

                    b.HasIndex("StudentId");

                    b.ToTable("StudentGroups");
                });

            modelBuilder.Entity("Core.Domain.Entites.Test", b =>
                {
                    b.Property<short>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint")
                        .UseIdentityColumn();

                    b.Property<string>("Title")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Tests");
                });

            modelBuilder.Entity("Core.Domain.Entites.TestQuestion", b =>
                {
                    b.Property<short>("TestId")
                        .HasColumnType("smallint");

                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.HasKey("TestId", "QuestionId");

                    b.HasIndex("QuestionId");

                    b.ToTable("TestQuestions");
                });

            modelBuilder.Entity("Core.Domain.Entites.Group", b =>
                {
                    b.HasOne("Core.Domain.Entites.Examiner", "Examiner")
                        .WithMany("Groups")
                        .HasForeignKey("ExaminerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Domain.Entites.Test", "Test")
                        .WithMany("Groups")
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Examiner");

                    b.Navigation("Test");
                });

            modelBuilder.Entity("Core.Domain.Entites.StudentGroup", b =>
                {
                    b.HasOne("Core.Domain.Entites.Group", "Group")
                        .WithMany("StudentGroups")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Domain.Entites.Student", "Student")
                        .WithMany("StudentGroups")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Core.Domain.Entites.TestQuestion", b =>
                {
                    b.HasOne("Core.Domain.Entites.Question", "Question")
                        .WithMany("TestQuestions")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Domain.Entites.Test", "Test")
                        .WithMany("TestQuestions")
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");

                    b.Navigation("Test");
                });

            modelBuilder.Entity("Core.Domain.Entites.Examiner", b =>
                {
                    b.Navigation("Groups");
                });

            modelBuilder.Entity("Core.Domain.Entites.Group", b =>
                {
                    b.Navigation("StudentGroups");
                });

            modelBuilder.Entity("Core.Domain.Entites.Question", b =>
                {
                    b.Navigation("TestQuestions");
                });

            modelBuilder.Entity("Core.Domain.Entites.Student", b =>
                {
                    b.Navigation("StudentGroups");
                });

            modelBuilder.Entity("Core.Domain.Entites.Test", b =>
                {
                    b.Navigation("Groups");

                    b.Navigation("TestQuestions");
                });
#pragma warning restore 612, 618
        }
    }
}
