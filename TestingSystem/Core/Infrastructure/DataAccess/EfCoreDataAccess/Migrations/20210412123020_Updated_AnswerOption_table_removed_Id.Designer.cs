﻿// <auto-generated />
using System;
using Core.Infrastructure.DataAccess.EfCoreDataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Core.Infrastructure.DataAccess.EfCoreDataAccess.Migrations
{
    [DbContext(typeof(CoreEfCoreDbContext))]
    [Migration("20210412123020_Updated_AnswerOption_table_removed_Id")]
    partial class Updated_AnswerOption_table_removed_Id
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Core.Domain.Entites.AnswerOption", b =>
                {
                    b.Property<bool>("IsCorrect")
                        .HasColumnType("bit");

                    b.Property<string>("OptionText")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.HasIndex("QuestionId");

                    b.ToTable("AnswerOptions");
                });

            modelBuilder.Entity("Core.Domain.Entites.Examiner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ExternalId")
                        .HasColumnType("nvarchar(max)");

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
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ExaminerId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("ExaminerId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("Core.Domain.Entites.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte>("QuestionScore")
                        .HasColumnType("tinyint");

                    b.Property<string>("QuestionText")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<short>("TestId")
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.HasIndex("TestId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("Core.Domain.Entites.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ExternalId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<short?>("GroupId1")
                        .HasColumnType("smallint");

                    b.Property<string>("LastName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("GroupId1");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("Core.Domain.Entites.StudentTestQuestion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<short>("TestId")
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.HasIndex("StudentId");

                    b.HasIndex("TestId");

                    b.ToTable("StudentTestQuestions");
                });

            modelBuilder.Entity("Core.Domain.Entites.StudentTestQuestionResponse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Response")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<int>("ResponseScore")
                        .HasColumnType("int");

                    b.Property<int>("StudentTestQuestionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StudentTestQuestionId");

                    b.ToTable("StudentTestQuestionResponses");
                });

            modelBuilder.Entity("Core.Domain.Entites.Test", b =>
                {
                    b.Property<short>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ExaminerId")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("TestScore")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("ExaminerId");

                    b.ToTable("Tests");
                });

            modelBuilder.Entity("Core.Domain.Entites.AnswerOption", b =>
                {
                    b.HasOne("Core.Domain.Entites.Question", "Question")
                        .WithMany()
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");
                });

            modelBuilder.Entity("Core.Domain.Entites.Group", b =>
                {
                    b.HasOne("Core.Domain.Entites.Examiner", "Examiner")
                        .WithMany("Groups")
                        .HasForeignKey("ExaminerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Examiner");
                });

            modelBuilder.Entity("Core.Domain.Entites.Question", b =>
                {
                    b.HasOne("Core.Domain.Entites.Test", "Test")
                        .WithMany("Questions")
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Test");
                });

            modelBuilder.Entity("Core.Domain.Entites.Student", b =>
                {
                    b.HasOne("Core.Domain.Entites.Group", "Group")
                        .WithMany("Students")
                        .HasForeignKey("GroupId1");

                    b.Navigation("Group");
                });

            modelBuilder.Entity("Core.Domain.Entites.StudentTestQuestion", b =>
                {
                    b.HasOne("Core.Domain.Entites.Question", "Question")
                        .WithMany()
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Domain.Entites.Student", "Student")
                        .WithMany("StudentTestQuestions")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Domain.Entites.Test", "Test")
                        .WithMany()
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");

                    b.Navigation("Student");

                    b.Navigation("Test");
                });

            modelBuilder.Entity("Core.Domain.Entites.StudentTestQuestionResponse", b =>
                {
                    b.HasOne("Core.Domain.Entites.StudentTestQuestion", "StudentTestQuestion")
                        .WithMany("Responses")
                        .HasForeignKey("StudentTestQuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("StudentTestQuestion");
                });

            modelBuilder.Entity("Core.Domain.Entites.Test", b =>
                {
                    b.HasOne("Core.Domain.Entites.Examiner", "Examiner")
                        .WithMany("Tests")
                        .HasForeignKey("ExaminerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Examiner");
                });

            modelBuilder.Entity("Core.Domain.Entites.Examiner", b =>
                {
                    b.Navigation("Groups");

                    b.Navigation("Tests");
                });

            modelBuilder.Entity("Core.Domain.Entites.Group", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("Core.Domain.Entites.Student", b =>
                {
                    b.Navigation("StudentTestQuestions");
                });

            modelBuilder.Entity("Core.Domain.Entites.StudentTestQuestion", b =>
                {
                    b.Navigation("Responses");
                });

            modelBuilder.Entity("Core.Domain.Entites.Test", b =>
                {
                    b.Navigation("Questions");
                });
#pragma warning restore 612, 618
        }
    }
}
