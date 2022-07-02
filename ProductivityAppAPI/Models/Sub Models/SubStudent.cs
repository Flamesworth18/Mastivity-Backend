namespace ProductivityAppAPI.Models.Sub_Models;

public class SubStudent
{
   [Key]
   public Guid Id { get; set; }
   public string FullName { get; set; }
}
