namespace ProductivityAppAPI.Models;

public class Counter
{
   [Key]
   public Guid Id { get; set; }
   public int Count { get; set; }
   public int OverallCount { get; set; }
   public int OnlineCount { get; set; }
}
