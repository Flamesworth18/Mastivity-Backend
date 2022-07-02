namespace ProductivityAppAPI.Models;

public class ToDos
{
   [Key]
   public Guid Id { get; set; }
   public string Title { get; set; }
   public string Author { get; set; }
   public DateTime Due { get; set; }
   public ICollection<Tasks> Tasks { get; set; }
   public int TaskCompleted { get; set; }
   public int StatusId { get; set; }
   public Status? Status { get; set; }
   public bool IsArchvied { get; set; }
   public string DateCreated { get; set; } = DateTime.UtcNow.ToString();
   public string DateUpdated { get; set; }
}
