namespace ProductivityAppAPI.Models.Sub_Models;

public class UserSubStudent
{
   [Key]
   public Guid Id { get; set; }
   public string FullName { get; set; }
   public string Status { get; set; } = "NG";
   public string? Remarks { get; set; }
}
