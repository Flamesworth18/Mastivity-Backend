namespace ProductivityAppAPI.Models;

public class Notes
{
   [Key]
   public Guid Id { get; set; }

   public string Title { get; set; }

   public string Description { get; set; }

   public string Author { get; set; }

   public bool IsArchived { get; set; }

   public string DateCreated { get; set; } = DateTime.UtcNow.ToString();

   public string DateUpdated { get; set; }
}
