﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProductivityAppAPI.Data;

#nullable disable

namespace ProductivityAppAPI.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20220701122600_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ProductivityAppAPI.Models.Counter", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<int>("OnlineCount")
                        .HasColumnType("int");

                    b.Property<int>("OverallCount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Counters");
                });

            modelBuilder.Entity("ProductivityAppAPI.Models.Day", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Abbreviation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("SubjectId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("SubjectId");

                    b.ToTable("Days");
                });

            modelBuilder.Entity("ProductivityAppAPI.Models.Mode", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsLightMode")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Modes");
                });

            modelBuilder.Entity("ProductivityAppAPI.Models.Notes", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DateCreated")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DateUpdated")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsArchived")
                        .HasColumnType("bit");

                    b.Property<Guid?>("RequestUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RequestUserId");

                    b.HasIndex("UserId");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("ProductivityAppAPI.Models.Programs", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Department")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProgramAbbreviation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProgramName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SchoolYear")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Semester")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Programs");
                });

            modelBuilder.Entity("ProductivityAppAPI.Models.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("ProductivityAppAPI.Models.Schedule", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Date")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DateCreated")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DateUpdated")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("RequestUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RequestUserId");

                    b.HasIndex("UserId");

                    b.ToTable("Schedules");
                });

            modelBuilder.Entity("ProductivityAppAPI.Models.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("StatusName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Statuses");
                });

            modelBuilder.Entity("ProductivityAppAPI.Models.Student", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Department")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Program")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("YearLevel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("ProductivityAppAPI.Models.StudentStatus", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("StudentStatuses");
                });

            modelBuilder.Entity("ProductivityAppAPI.Models.Sub_Models.RequestUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsVerify")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("PasswordResetToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.Property<DateTime?>("PasswordTokenExpires")
                        .HasColumnType("datetime2");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserCreated")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VerificationToken")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("RequestUsers");
                });

            modelBuilder.Entity("ProductivityAppAPI.Models.Sub_Models.SubStudent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("SubjectId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("SubjectId");

                    b.ToTable("SubStudents");
                });

            modelBuilder.Entity("ProductivityAppAPI.Models.Sub_Models.SubSubject", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ProgramsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("SubjectName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProgramsId");

                    b.ToTable("SubSubjects");
                });

            modelBuilder.Entity("ProductivityAppAPI.Models.Sub_Models.UserSubDay", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Abbreviation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UserSubDays");
                });

            modelBuilder.Entity("ProductivityAppAPI.Models.Sub_Models.UserSubStudent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Remarks")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("UserSubjectId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserSubjectId");

                    b.ToTable("UserSubStudents");
                });

            modelBuilder.Entity("ProductivityAppAPI.Models.Sub_Models.UserSubSubject", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("SubjectName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("UserProgramId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserProgramId");

                    b.ToTable("UserSubSubjects");
                });

            modelBuilder.Entity("ProductivityAppAPI.Models.Subject", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DaysView")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Department")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EndingDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EndingTime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EndingTimeView")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("OnFriday")
                        .HasColumnType("bit");

                    b.Property<bool>("OnMonday")
                        .HasColumnType("bit");

                    b.Property<bool>("OnThursday")
                        .HasColumnType("bit");

                    b.Property<bool>("OnTuesday")
                        .HasColumnType("bit");

                    b.Property<bool>("OnWednesday")
                        .HasColumnType("bit");

                    b.Property<string>("StartingDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StartingTime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StartingTimeView")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SubjectCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SubjectName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("ProductivityAppAPI.Models.SubUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsVerify")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserCreated")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SubUsers");
                });

            modelBuilder.Entity("ProductivityAppAPI.Models.Tasks", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DateCreated")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DateUpdated")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("bit");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ToDosId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ToDosId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("ProductivityAppAPI.Models.ToDos", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DateCreated")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DateUpdated")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Due")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsArchvied")
                        .HasColumnType("bit");

                    b.Property<Guid?>("RequestUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.Property<int>("TaskCompleted")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RequestUserId");

                    b.HasIndex("StatusId");

                    b.HasIndex("UserId");

                    b.ToTable("ToDos");
                });

            modelBuilder.Entity("ProductivityAppAPI.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsVerify")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("PasswordResetToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.Property<DateTime?>("PasswordTokenExpires")
                        .HasColumnType("datetime2");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserCreated")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VerificationToken")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ProductivityAppAPI.Models.UserProgram", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Department")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProgramAbbreviation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProgramName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("RequestUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("SchoolYear")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Semester")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RequestUserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserPrograms");
                });

            modelBuilder.Entity("ProductivityAppAPI.Models.UserStudent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Department")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Program")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("RequestUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("YearLevel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RequestUserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserStudents");
                });

            modelBuilder.Entity("ProductivityAppAPI.Models.UserSubject", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DaysView")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Department")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EndingDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EndingTime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EndingTimeView")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("OnFriday")
                        .HasColumnType("bit");

                    b.Property<bool>("OnMonday")
                        .HasColumnType("bit");

                    b.Property<bool>("OnThursday")
                        .HasColumnType("bit");

                    b.Property<bool>("OnTuesday")
                        .HasColumnType("bit");

                    b.Property<bool>("OnWednesday")
                        .HasColumnType("bit");

                    b.Property<Guid?>("RequestUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("StartingDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StartingTime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StartingTimeView")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SubjectCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SubjectName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RequestUserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserSubjects");
                });

            modelBuilder.Entity("ProductivityAppAPI.Models.Day", b =>
                {
                    b.HasOne("ProductivityAppAPI.Models.Subject", null)
                        .WithMany("Days")
                        .HasForeignKey("SubjectId");
                });

            modelBuilder.Entity("ProductivityAppAPI.Models.Notes", b =>
                {
                    b.HasOne("ProductivityAppAPI.Models.Sub_Models.RequestUser", null)
                        .WithMany("Notes")
                        .HasForeignKey("RequestUserId");

                    b.HasOne("ProductivityAppAPI.Models.User", null)
                        .WithMany("Notes")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("ProductivityAppAPI.Models.Schedule", b =>
                {
                    b.HasOne("ProductivityAppAPI.Models.Sub_Models.RequestUser", null)
                        .WithMany("Schedules")
                        .HasForeignKey("RequestUserId");

                    b.HasOne("ProductivityAppAPI.Models.User", null)
                        .WithMany("Schedules")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("ProductivityAppAPI.Models.Sub_Models.SubStudent", b =>
                {
                    b.HasOne("ProductivityAppAPI.Models.Subject", null)
                        .WithMany("Students")
                        .HasForeignKey("SubjectId");
                });

            modelBuilder.Entity("ProductivityAppAPI.Models.Sub_Models.SubSubject", b =>
                {
                    b.HasOne("ProductivityAppAPI.Models.Programs", null)
                        .WithMany("Subjects")
                        .HasForeignKey("ProgramsId");
                });

            modelBuilder.Entity("ProductivityAppAPI.Models.Sub_Models.UserSubStudent", b =>
                {
                    b.HasOne("ProductivityAppAPI.Models.UserSubject", null)
                        .WithMany("Students")
                        .HasForeignKey("UserSubjectId");
                });

            modelBuilder.Entity("ProductivityAppAPI.Models.Sub_Models.UserSubSubject", b =>
                {
                    b.HasOne("ProductivityAppAPI.Models.UserProgram", null)
                        .WithMany("Subjects")
                        .HasForeignKey("UserProgramId");
                });

            modelBuilder.Entity("ProductivityAppAPI.Models.Tasks", b =>
                {
                    b.HasOne("ProductivityAppAPI.Models.ToDos", null)
                        .WithMany("Tasks")
                        .HasForeignKey("ToDosId");
                });

            modelBuilder.Entity("ProductivityAppAPI.Models.ToDos", b =>
                {
                    b.HasOne("ProductivityAppAPI.Models.Sub_Models.RequestUser", null)
                        .WithMany("ToDos")
                        .HasForeignKey("RequestUserId");

                    b.HasOne("ProductivityAppAPI.Models.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProductivityAppAPI.Models.User", null)
                        .WithMany("ToDos")
                        .HasForeignKey("UserId");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("ProductivityAppAPI.Models.UserProgram", b =>
                {
                    b.HasOne("ProductivityAppAPI.Models.Sub_Models.RequestUser", null)
                        .WithMany("ProgramHandled")
                        .HasForeignKey("RequestUserId");

                    b.HasOne("ProductivityAppAPI.Models.User", null)
                        .WithMany("ProgramHandled")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("ProductivityAppAPI.Models.UserStudent", b =>
                {
                    b.HasOne("ProductivityAppAPI.Models.Sub_Models.RequestUser", null)
                        .WithMany("StudentHandled")
                        .HasForeignKey("RequestUserId");

                    b.HasOne("ProductivityAppAPI.Models.User", null)
                        .WithMany("StudentHandled")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("ProductivityAppAPI.Models.UserSubject", b =>
                {
                    b.HasOne("ProductivityAppAPI.Models.Sub_Models.RequestUser", null)
                        .WithMany("SubjectHandled")
                        .HasForeignKey("RequestUserId");

                    b.HasOne("ProductivityAppAPI.Models.User", null)
                        .WithMany("SubjectHandled")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("ProductivityAppAPI.Models.Programs", b =>
                {
                    b.Navigation("Subjects");
                });

            modelBuilder.Entity("ProductivityAppAPI.Models.Sub_Models.RequestUser", b =>
                {
                    b.Navigation("Notes");

                    b.Navigation("ProgramHandled");

                    b.Navigation("Schedules");

                    b.Navigation("StudentHandled");

                    b.Navigation("SubjectHandled");

                    b.Navigation("ToDos");
                });

            modelBuilder.Entity("ProductivityAppAPI.Models.Subject", b =>
                {
                    b.Navigation("Days");

                    b.Navigation("Students");
                });

            modelBuilder.Entity("ProductivityAppAPI.Models.ToDos", b =>
                {
                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("ProductivityAppAPI.Models.User", b =>
                {
                    b.Navigation("Notes");

                    b.Navigation("ProgramHandled");

                    b.Navigation("Schedules");

                    b.Navigation("StudentHandled");

                    b.Navigation("SubjectHandled");

                    b.Navigation("ToDos");
                });

            modelBuilder.Entity("ProductivityAppAPI.Models.UserProgram", b =>
                {
                    b.Navigation("Subjects");
                });

            modelBuilder.Entity("ProductivityAppAPI.Models.UserSubject", b =>
                {
                    b.Navigation("Students");
                });
#pragma warning restore 612, 618
        }
    }
}
