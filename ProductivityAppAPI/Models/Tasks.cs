namespace ProductivityAppAPI.Models;

public class Tasks
{
   [Key]
   public Guid Id { get; set; }
   public string Title { get; set; }
   public string Author { get; set; }
   public bool IsCompleted { get; set; }
   public string Status { get; set; }
   public string DateCreated { get; set; }
   public string DateUpdated { get; set; }
}
