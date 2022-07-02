using Microsoft.AspNetCore.Mvc;
using ProductivityAppAPI.Models;
using ProductivityAppAPI.Models.Sub_Models;
using ProductivityAppAPI.Static;

namespace ProductivityAppAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class RequestUserController : ControllerBase
{
   private readonly DataContext _context;

   public RequestUserController(DataContext context)
   {
      _context = context;
   }

   //Get all users
   [HttpGet, Authorize(Roles = "Administrator")]
   public async Task<IActionResult> GetAllUsers()
   {
      var user = await _context.RequestUsers
         .Include(c => c.ProgramHandled)
         .Include(s => s.Schedules)
         .Include(t => t.ToDos)
         .Include(n => n.Notes)
         .ToListAsync(); ;

      return Ok(user);
   }

   //Get user
   [HttpGet, Authorize(Roles = "Administrator")]
   [Route("{id:guid}")]
   [ActionName("GetUser")]
   public async Task<IActionResult> GetUser([FromRoute] Guid id)
   {
      var user = await _context.RequestUsers
         .Include(c => c.ProgramHandled)
         .Include(s => s.Schedules)
         .Include(t => t.ToDos)
         .Include(n => n.Notes)
         .FirstOrDefaultAsync(x => x.Id == id);

      if (user != null)
      {
         return Ok(user);
      }

      return NotFound("User not found.");
   }

   //Add user
   [HttpPost, Authorize(Roles = "Administrator")]
   public async Task<IActionResult> AddUser([FromBody] SubUser subUser)
   {
      TokenGenerator.CreatePasswordHash(subUser.Password, out byte[] passwordHash, out byte[] passwordSalt);

      RequestUser user = new RequestUser();
      user.Id = Guid.NewGuid();
      user.FirstName = subUser.FirstName;
      user.LastName = subUser.LastName;
      user.Username = subUser.Username;
      user.Email = subUser.Email;
      user.PasswordHash = passwordHash;
      user.PasswordSalt = passwordSalt;
      user.VerificationToken = TokenGenerator.CreateRandomVerificationToken();
      user.Role = subUser.Role;
      user.UserCreated = DateTime.UtcNow.ToShortDateString();

      await _context.RequestUsers.AddAsync(user);
      await _context.SaveChangesAsync();

      return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
   }

   //Update user
   [HttpPut, Authorize(Roles = "Administrator")]
   [Route("{id:guid}")]
   public async Task<IActionResult> UpdateUser([FromRoute] Guid id, [FromBody] User user)
   {
      var existingUser = await _context.RequestUsers.FindAsync(id);
      if (existingUser != null)
      {
         existingUser.FirstName = user.FirstName;
         existingUser.LastName = user.LastName;
         existingUser.Username = user.Username;
         existingUser.ProgramHandled = user.ProgramHandled;
         existingUser.SubjectHandled = user.SubjectHandled;
         existingUser.StudentHandled = user.StudentHandled;
         existingUser.Schedules = user.Schedules;
         existingUser.ToDos = user.ToDos;
         existingUser.Notes = user.Notes;
         existingUser.Role = user.Role;

         await _context.SaveChangesAsync();
         return Ok(existingUser);
      }

      return NotFound("User not found.");
   }

   //Remove user
   [HttpDelete, Authorize(Roles = "Administrator")]
   [Route("{id:guid}")]
   public async Task<IActionResult> RemoveUser([FromRoute] Guid id)
   {
      var user = await _context.RequestUsers
         .Include(p => p.ProgramHandled)
         .Include(s => s.SubjectHandled)
         .Include(s => s.StudentHandled)
         .Include(s => s.Schedules)
         .Include(t => t.ToDos)
         .Include(n => n.Notes)
         .FirstOrDefaultAsync(x => x.Id == id);

      if (user != null)
      {
         _context.RequestUsers.Remove(user);
         await _context.SaveChangesAsync();

         return Ok(user);
      }

      return NotFound("User not found.");
   }

   //Remove user
   [HttpPost, Authorize(Roles = "Administrator")]
   [Route("Users")]
   public async Task<IActionResult> RemoveUsers([FromBody] RequestUser[] requestUsers)
   {
      for (int i = 0; i < requestUsers.Length; i++)
      {
         var user = await _context.RequestUsers
         .Include(p => p.ProgramHandled)
         .Include(s => s.SubjectHandled)
         .Include(s => s.StudentHandled)
         .Include(s => s.Schedules)
         .Include(t => t.ToDos)
         .Include(n => n.Notes)
         .FirstOrDefaultAsync(x => x.Email == requestUsers[i].Email);

         if (user != null)
         {
            _context.RequestUsers.Remove(user);
         }
      }

      await _context.SaveChangesAsync();

      return Ok();
   }
}
