using Microsoft.AspNetCore.Mvc;
using ProductivityAppAPI.Models;

namespace ProductivityAppAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoStatusController : Controller
{
   private readonly DataContext _context;

   public TodoStatusController(DataContext context)
   {
      _context = context;
   }

   //Get all status
   [HttpGet, Authorize(Roles = "User, Administrator")]
   public async Task<IActionResult> GetAllStatus()
   {
      var statuses = await _context.Statuses.ToListAsync();
      return Ok(statuses);
   }

   //Get status
   [HttpGet, Authorize(Roles = "Administrator")]
   [Route("id")]
   [ActionName("GetStatus")]
   public async Task<IActionResult> GetStatus([FromRoute] int id)
   {
      var status = await _context.Statuses.FindAsync(id);
      if(status != null)
      {
         return Ok(status);
      }

      return NotFound(status);
   }

   //Add status
   [HttpPost, Authorize(Roles = "Administrator")]
   public async Task<IActionResult> AddStatus([FromBody] Status status)
   {
      await _context.Statuses.AddAsync(status);
      await _context.SaveChangesAsync();
      
      return CreatedAtAction(nameof(GetStatus), new { id = status.Id }, status);
   }

   //Edit status
   [HttpPut, Authorize(Roles = "Administrator")]
   [Route("{id}")]
   public async Task<IActionResult> EditStatus([FromRoute] int id, [FromBody] Status status)
   {
      var existingStatus = await _context.Statuses.FindAsync(id);
      if(existingStatus != null)
      {
         existingStatus.Id = status.Id;
         existingStatus.StatusName = status.StatusName;

         await _context.SaveChangesAsync();
         return Ok(existingStatus);
      }

      return NotFound(existingStatus);
   }

   //Remove status
   [HttpDelete, Authorize(Roles = "Administrator")]
   [Route("{id}")]
   public async Task<IActionResult> RemoveStatus([FromRoute] int id)
   {
      var status = await _context.Statuses.FirstOrDefaultAsync(x => x.Id == id);
      if(status != null)
      {
         _context.Statuses.Remove(status);
         await _context.SaveChangesAsync();
         return Ok(status);
      }

      return NotFound(status);
   }
}
