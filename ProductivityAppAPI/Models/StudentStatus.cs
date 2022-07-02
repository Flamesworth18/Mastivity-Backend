namespace ProductivityAppAPI.Models;

public class StudentStatus
{
   [Key]
   public Guid Id { get; set; }
   public string Status { get; set; }
}
