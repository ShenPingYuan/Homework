﻿// <auto-generated />
using System;
using Homework.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Homework.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210320075402_init0")]
    partial class init0
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Homework.DB.Entities.Choice", b =>
                {
                    b.Property<string>("ChoiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("ChoiceOrder")
                        .IsRequired()
                        .HasColumnType("varchar(1) CHARACTER SET utf8mb4");

                    b.Property<string>("Content")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("QuestionId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("ChoiceId");

                    b.HasIndex("QuestionId");

                    b.ToTable("Choices");
                });

            modelBuilder.Entity("Homework.DB.Entities.Course", b =>
                {
                    b.Property<string>("CourseId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("CourseName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("CourseId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("Homework.DB.Entities.Homework", b =>
                {
                    b.Property<string>("HomeworkId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<DateTime>("AssignTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CourseId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<DateTime>("Deadline")
                        .HasColumnType("datetime(6)");

                    b.HasKey("HomeworkId");

                    b.HasIndex("CourseId");

                    b.ToTable("Homeworks");
                });

            modelBuilder.Entity("Homework.DB.Entities.Question", b =>
                {
                    b.Property<string>("QuestionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Answer")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("HomeworkId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("QuestionTitle")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<double>("Score")
                        .HasColumnType("double");

                    b.Property<int>("WorkType")
                        .HasColumnType("int");

                    b.HasKey("QuestionId");

                    b.HasIndex("HomeworkId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("Homework.DB.Entities.Student", b =>
                {
                    b.Property<string>("Sub")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("StudentId")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("StudentName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Sub");

                    b.HasIndex("Sub")
                        .IsUnique();

                    b.ToTable("Students");
                });

            modelBuilder.Entity("Homework.DB.Entities.StudentAnswer", b =>
                {
                    b.Property<string>("StudentAnswerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Answer")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<double>("Score")
                        .HasColumnType("double");

                    b.Property<string>("StudentWorkId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("StudentAnswerId");

                    b.HasIndex("StudentWorkId");

                    b.ToTable("StudentAnswers");
                });

            modelBuilder.Entity("Homework.DB.Entities.StudentCourse", b =>
                {
                    b.Property<string>("CourseId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("StudentId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("CourseId", "StudentId");

                    b.HasIndex("StudentId");

                    b.ToTable("StudentCourses");
                });

            modelBuilder.Entity("Homework.DB.Entities.StudentWork", b =>
                {
                    b.Property<string>("StudentWorkId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("HomeworkId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<bool>("IsReview")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsSubmited")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("StudentId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<DateTime>("SubmissionTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("TeacherName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<double>("TotalScore")
                        .HasColumnType("double");

                    b.HasKey("StudentWorkId");

                    b.HasIndex("HomeworkId");

                    b.HasIndex("StudentId");

                    b.ToTable("StudentWorks");
                });

            modelBuilder.Entity("Homework.DB.Entities.Teacher", b =>
                {
                    b.Property<string>("TeacherId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("CourseId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Sub")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("TeacherName")
                        .HasColumnType("varchar(20) CHARACTER SET utf8mb4")
                        .HasMaxLength(20)
                        .IsUnicode(true);

                    b.HasKey("TeacherId");

                    b.HasIndex("CourseId")
                        .IsUnique();

                    b.HasIndex("Sub")
                        .IsUnique();

                    b.ToTable("tb_teacher");
                });

            modelBuilder.Entity("Homework.DB.Entities.Choice", b =>
                {
                    b.HasOne("Homework.DB.Entities.Question", "Question")
                        .WithMany("Choices")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Homework.DB.Entities.Homework", b =>
                {
                    b.HasOne("Homework.DB.Entities.Course", "Course")
                        .WithMany("Homeworks")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("Homework.DB.Entities.Question", b =>
                {
                    b.HasOne("Homework.DB.Entities.Homework", "Homework")
                        .WithMany("Questions")
                        .HasForeignKey("HomeworkId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Homework.DB.Entities.StudentAnswer", b =>
                {
                    b.HasOne("Homework.DB.Entities.StudentWork", "StudentWork")
                        .WithMany("StudentAnswers")
                        .HasForeignKey("StudentWorkId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Homework.DB.Entities.StudentCourse", b =>
                {
                    b.HasOne("Homework.DB.Entities.Course", "Course")
                        .WithMany("StudentCourses")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Homework.DB.Entities.Student", "Student")
                        .WithMany("StudentCourses")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Homework.DB.Entities.StudentWork", b =>
                {
                    b.HasOne("Homework.DB.Entities.Homework", "Homework")
                        .WithMany("StudentWorks")
                        .HasForeignKey("HomeworkId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Homework.DB.Entities.Student", "Student")
                        .WithMany("StudentWorks")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("Homework.DB.Entities.Teacher", b =>
                {
                    b.HasOne("Homework.DB.Entities.Course", "Course")
                        .WithOne("Teacher")
                        .HasForeignKey("Homework.DB.Entities.Teacher", "CourseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
