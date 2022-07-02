namespace ProductivityAppAPI.Models.Sub_Models;

public class RequestUser
{
   [Key]
   public Guid Id { get; set; }

   public string FirstName { get; set; }

   public string LastName { get; set; }

   public string Username { get; set; }

   public string Email { get; set; }

   public byte[]? PasswordHash { get; set; }

   public byte[]? PasswordSalt { get; set; }

   public string? VerificationToken { get; set; }

   public bool IsVerify { get; set; } = false;

   public string? PasswordResetToken { get; set; }

   public DateTime? PasswordTokenExpires { get; set; }

   public virtual ICollection<UserProgram>? ProgramHandled { get; set; }

   public virtual ICollection<UserSubject>? SubjectHandled { get; set; }

   public virtual ICollection<UserStudent>? StudentHandled { get; set; }

   public virtual ICollection<Schedule>? Schedules { get; set; }

   public virtual ICollection<ToDos>? ToDos { get; set; }

   public virtual ICollection<Notes>? Notes { get; set; }

   public string UserCreated { get; set; } = DateTime.UtcNow.ToShortDateString();

   public string Role { get; set; }
}
