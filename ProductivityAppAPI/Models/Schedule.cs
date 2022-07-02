namespace ProductivityAppAPI.Models;

public class Schedule
{
   [Key]
   public Guid Id { get; set; }

   public string Title { get; set; }

   public string Date { get; set; }

   public string Color { get; set; }

   public string Author { get; set; }

   public string DateCreated { get; set; } = DateTime.UtcNow.ToString();

   public string DateUpdated { get; set; }
}
