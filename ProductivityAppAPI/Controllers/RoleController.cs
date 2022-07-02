using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductivityAppAPI.Models;

namespace ProductivityAppAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class RoleController : ControllerBase
{
   private readonly DataContext _context;

   public RoleController(DataContext context)
   {
      _context = context;
   }

   //Get all roles
   [HttpGet]
   [Authorize(Roles = "User, Administrator")]
   public async Task<IActionResult> GetAllRoles()
   {
      var roles = await _context.Roles.ToListAsync();
      return Ok(roles);
   }

   //Get role
   [HttpGet]
   [Route("{id:guid}")]
   [Authorize(Roles = "Administrator")]
   [ActionName("GetRole")]
   public async Task<IActionResult> GetRole([FromRoute] Guid id)
   {
      var role = await _context.Roles.FindAsync(id);
      if(role != null)
      {
         return Ok(role);
      }
      return NotFound(role);
   }

   //Add role
   [HttpPost]
   [Authorize(Roles = "Administrator")]
   public async Task<IActionResult> AddRole([FromBody] Role role)
   {
      role.Id = Guid.NewGuid();
      await _context.Roles.AddAsync(role);
      await _context.SaveChangesAsync();

      return CreatedAtAction(nameof(GetRole), new { id = role.Id }, role);
   }

   //Update role
   [HttpPut]
   [Authorize(Roles = "Administrator")]
   [Route("{id:guid}")]
   public async Task<IActionResult> UpdateRole([FromRoute] Guid id,[FromBody] Role role)
   {
      var existingRole = await _context.Roles.FindAsync(id);
      if(existingRole != null)
      {
         existingRole.Name = role.Name;
         await _context.SaveChangesAsync();
         return Ok(existingRole);
      }

      return NotFound(existingRole);
   }

   //Remove role
   [HttpDelete]
   [Authorize(Roles = "Administrator")]
   [Route("{id:guid}")]
   public async Task<IActionResult> RemoveRole([FromRoute] Guid id)
   {
      var role = await _context.Roles.FindAsync(id);
      if(role != null)
      {
         _context.Roles.Remove(role);
         await _context.SaveChangesAsync();
         return Ok(role);
      }

      return BadRequest(role);
   }

}
