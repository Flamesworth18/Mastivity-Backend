using Microsoft.AspNetCore.Mvc;
using ProductivityAppAPI.Models;

namespace ProductivityAppAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Administrator")]
public class ScheduleController : Controller
{
   private readonly DataContext _context;

   public ScheduleController(DataContext context)
   {
      _context = context;
   }

   //Get all schedules
   [HttpGet]
   public async Task<IActionResult> GetAllSchedules()
   {
      var schedules = await _context.Schedules.ToListAsync();
      return Ok(schedules);
   }

   //Get schedule
   [HttpGet]
   [Route("{id:guid}")]
   [ActionName("GetSchedule")]
   public async Task<IActionResult> GetSchedule([FromRoute] Guid id)
   {
      var schedule = await _context.Schedules.FirstOrDefaultAsync(x => x.Id == id);
      if(schedule != null)
      {
         return Ok(schedule);
      }

      return NotFound("Schedule not found.");
   }

   //Add schedule
   [HttpPost]
   public async Task<IActionResult> AddSchedule([FromBody] Schedule schedule)
   {
      schedule.Id = Guid.NewGuid();
      await _context.Schedules.AddAsync(schedule);
      await _context.SaveChangesAsync();

      return CreatedAtAction(nameof(GetSchedule), new { id = schedule.Id }, schedule);
   }

   //Edit schedule
   [HttpPut]
   [Route("{id:guid}")]
   public async Task<IActionResult> EditSchedule([FromRoute] Guid id, [FromBody] Schedule schedule)
   {
      var existingSchedule = await _context.Schedules.FirstOrDefaultAsync(x => x.Id == id);
      if(existingSchedule != null)
      {
         existingSchedule.Title = schedule.Title;
         existingSchedule.Date = schedule.Date;
         existingSchedule.Color = schedule.Color;
         existingSchedule.Author = schedule.Author;
         existingSchedule.DateUpdated = schedule.DateUpdated;

         await _context.SaveChangesAsync();
         return Ok(existingSchedule);
      }

      return BadRequest(existingSchedule);
   }

   //Remove schedule
   [HttpDelete]
   [Route("{id:guid}")]
   public async Task<IActionResult> RemoveSchedule([FromRoute] Guid id)
   {
      var schedule = await _context.Schedules.FirstOrDefaultAsync(x => x.Id == id);
      if(schedule != null)
      {
         _context.Schedules.Remove(schedule);
         await _context.SaveChangesAsync();

         return Ok(schedule);
      }

      return NotFound("Schedule not found.");
   }
}
