namespace ProductivityAppAPI.Models;

public class Student
{
   [Key]
   public Guid Id { get; set; }
   public string FullName { get; set; }
   public string YearLevel { get; set; }
   public string Program { get; set; }
   public string Department { get; set; }
   public string Status { get; set; }

}
