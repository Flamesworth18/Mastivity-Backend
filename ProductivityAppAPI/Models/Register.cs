namespace ProductivityAppAPI.Models;

public class Register
{
   [Required]
   public string Username { get; set; }

   [Required, MinLength(6, ErrorMessage = "Please enter more than 6 characters!")]
   public string Password { get; set; }

   [Required, Compare("Password")]
   public string ConfirmPassword { get; set; }

   [Required, EmailAddress]
   public string Email { get; set; }

   [Required]
   public string FirstName { get; set; }

   [Required]
   public string LastName { get; set; }


}
