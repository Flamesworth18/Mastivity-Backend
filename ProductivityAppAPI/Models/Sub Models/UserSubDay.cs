namespace ProductivityAppAPI.Models.Sub_Models;

public class UserSubDay
{
   [Key]
   public Guid Id { get; set; }

   public string Abbreviation { get; set; }
}
