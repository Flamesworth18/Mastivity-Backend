using Microsoft.EntityFrameworkCore;
using ProductivityAppAPI.Models;
using ProductivityAppAPI.Models.Sub_Models;

namespace ProductivityAppAPI.Data;

public class DataContext : DbContext
{
   public DataContext(DbContextOptions options) : base(options) { }

   public DbSet<User> Users { get; set; }

   public DbSet<RequestUser> RequestUsers { get; set; }

   public DbSet<Programs> Programs { get; set; }

   public DbSet<Subject> Subjects { get; set; }

   public DbSet<Schedule> Schedules { get; set; }

   public DbSet<Student> Students { get; set; }

   public DbSet<ToDos> ToDos { get; set; }

   public DbSet<Notes> Notes { get; set; }

   public DbSet<Tasks> Tasks { get; set; }

   public DbSet<Status> Statuses { get; set; }

   public DbSet<UserProgram> UserPrograms { get; set; }

   public DbSet<UserStudent> UserStudents { get; set; }

   public DbSet<UserSubject> UserSubjects { get; set; }

   public DbSet<UserSubSubject> UserSubSubjects { get; set; }

   public DbSet<UserSubStudent> UserSubStudents { get; set; }

   public DbSet<UserSubDay> UserSubDays { get; set; }

   public DbSet<SubUser> SubUsers { get; set; }

   public DbSet<SubSubject> SubSubjects { get; set; }

   public DbSet<SubStudent> SubStudents { get; set; }

   public DbSet<StudentStatus> StudentStatuses { get; set; }

   public DbSet<Role> Roles { get; set; }

   public DbSet<Counter> Counters { get; set; }

   public DbSet<Day> Days { get; set; }

   public DbSet<Total> Totals { get; set; }

}
