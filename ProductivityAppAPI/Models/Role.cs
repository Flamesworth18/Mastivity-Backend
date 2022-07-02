namespace ProductivityAppAPI.Models;

public class Role
{
   [Key]
   public Guid Id { get; set; }
   public string Name { get; set; }
}
