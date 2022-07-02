using Microsoft.AspNetCore.Mvc;
using ProductivityAppAPI.Models;

namespace ProductivityAppAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class DayController : ControllerBase
{
   private readonly DataContext _context;

   public DayController(DataContext context)
   {
      _context = context;
   }

   //get all days
   [HttpGet]
   [Authorize(Roles = "User, Administrator")]
   public async Task<IActionResult> GetAllDays()
   {
      var days = await _context.Days.ToListAsync();
      return Ok(days);
   }

   //get day
   [HttpGet]
   [Route("{id:guid}")]
   [ActionName("GetDay")]
   [Authorize(Roles = "Administrator")]
   public async Task<IActionResult> GetDay([FromRoute] Guid id)
   {
      var day = await _context.Days.FirstOrDefaultAsync(x => x.Id == id);
      if(day != null)
      {
         return Ok(day);
      }

      return NotFound(day);
   }

   //Add day
   [HttpPost]
   [Authorize(Roles = "Administrator")]
   public async Task<IActionResult> AddNote([FromBody] Day day)
   {
      day.Id = Guid.NewGuid();
      await _context.Days.AddAsync(day);
      await _context.SaveChangesAsync();

      return CreatedAtAction(nameof(GetDay), new { id = day.Id }, day);
   }

   //Edit day
   [HttpPut]
   [Route("{id:guid}")]
   [Authorize(Roles = "Administrator")]
   public async Task<IActionResult> EditDay([FromRoute] Guid id, [FromBody] Day days)
   {
      var existingDay = await _context.Days.FindAsync(id);
      if (existingDay != null)
      {
         existingDay.Abbreviation = days.Abbreviation;

         await _context.SaveChangesAsync();
         return Ok(existingDay);
      }

      return NotFound("Day not found.");
   }

   //Remove day
   [HttpDelete]
   [Route("{id:guid}")]
   [Authorize(Roles = "Administrator")]
   public async Task<IActionResult> RemoveDay([FromRoute] Guid id)
   {
      var day = await _context.Days.FirstOrDefaultAsync(x => x.Id == id);
      if (day != null)
      {
         _context.Days.Remove(day);
         await _context.SaveChangesAsync();
         return Ok(day);
      }

      return NotFound("Day not found.");
   }
}
