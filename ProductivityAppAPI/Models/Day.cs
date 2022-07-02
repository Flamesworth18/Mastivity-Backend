namespace ProductivityAppAPI.Models;

public class Day
{
   [Key]
   public Guid Id { get; set; }

   public string Abbreviation { get; set; }
}
