using Microsoft.AspNetCore.Mvc;
using ProductivityAppAPI.Models;
using ProductivityAppAPI.Models.Sub_Models;
using ProductivityAppAPI.Static;

namespace ProductivityAppAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : Controller
{
   private readonly DataContext _context;
   private readonly IConfiguration _config;

   public AccountController(DataContext context, IConfiguration config)
   {
      _context = context;
      _config = config;
   }

   //Create user
   [HttpPost("Register")]
   public async Task<IActionResult> Register([FromBody] Register request)
   {
      if (string.IsNullOrEmpty(request.Username) &&
         string.IsNullOrEmpty(request.Password) &&
         string.IsNullOrEmpty(request.Email) &&
         string.IsNullOrEmpty(request.FirstName) &&
         string.IsNullOrEmpty(request.LastName))
      {
         return BadRequest("Invalid inputs");
      }

      if (_context.Users.Any(u => u.Email == request.Email))
      {
         return BadRequest("Email already exists!");
      }

      TokenGenerator.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

      var user = new RequestUser();
      user.Id = Guid.NewGuid();
      user.FirstName = request.FirstName;
      user.LastName = request.LastName;
      user.Username = request.Username;
      user.Email = request.Email;
      user.PasswordHash = passwordHash;
      user.PasswordSalt = passwordSalt;
      user.VerificationToken = TokenGenerator.CreateRandomVerificationToken();
      user.Role = "User";

      await _context.RequestUsers.AddAsync(user);
      await _context.SaveChangesAsync();

      var subUser = new SubUser();
      subUser.Id = user.Id;
      subUser.FirstName = request.FirstName;

      return Ok(subUser);
   }

   //Log in user
   [HttpPost("Login")]
   public async Task<IActionResult> Login([FromBody] Login request)
   {
      if (string.IsNullOrEmpty(request.Username) && string.IsNullOrEmpty(request.Password))
      {
         return BadRequest("Invalid inputs");
      }

      var user = await _context.Users.Where(x => x.Username == request.Username).FirstOrDefaultAsync();
      if (user != null)
      {

         if (!user.IsVerify)
         {
            return BadRequest("Email is not verified");
         }

         if (!TokenGenerator.VerifyPasswordHash(user, request))
         {
            return BadRequest("Wrong Password");
         }

         var token = TokenGenerator.CreateToken(user, _config);
         return Ok(token);
      }

      return NotFound("User does not exist");
   }

   [HttpPost("verify")]
   public async Task<IActionResult> Verify(string token)
   {
      var user = await _context.Users.FirstOrDefaultAsync(u => u.VerificationToken == token);
      if (user == null)
      {
         return BadRequest("Invalid token.");
      }

      user.IsVerify = true;
      await _context.SaveChangesAsync();

      return Ok("User verified!)");
   }

   [HttpPost("forgot-password")]
   public async Task<IActionResult> ForgotPassword([FromBody] Forgot forgot)
   {
      var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == forgot.Email);
      if (user == null)
      {
         return BadRequest("User not found.");
      }

      user.PasswordResetToken = TokenGenerator.CreateRandomPasswordToken();
      user.PasswordTokenExpires = DateTime.Now.AddDays(1);
      await _context.SaveChangesAsync();

      return Ok(user.PasswordResetToken);
   }

   [HttpPost("reset-password")]
   public async Task<IActionResult> ResetPassword(Reset request)
   {
      var user = await _context.Users.FirstOrDefaultAsync(u => u.PasswordResetToken == request.Token);
      if (user == null || user.PasswordTokenExpires < DateTime.Now)
      {
         return BadRequest("Invalid Token.");
      }

      TokenGenerator.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

      user.PasswordHash = passwordHash;
      user.PasswordSalt = passwordSalt;
      user.PasswordResetToken = null;
      user.PasswordTokenExpires = null;

      await _context.SaveChangesAsync();

      return Ok("Password successfully reset.");
   }
}