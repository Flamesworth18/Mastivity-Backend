namespace ProductivityAppAPI.Models;

public class SubUser
{
   [Key]
   public Guid Id { get; set; }
   public string FirstName { get; set; }
   public string LastName { get; set; }
   public string Username { get; set; }
   public string Email { get; set; }
   public string Password { get; set; }
   public string Role { get; set; }
   public string UserCreated { get; set; } = DateTime.UtcNow.ToShortDateString();
   public bool IsVerify { get; set; } = false;
}
