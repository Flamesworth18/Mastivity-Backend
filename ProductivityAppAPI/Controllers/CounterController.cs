using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductivityAppAPI.Models;

namespace ProductivityAppAPI.Controllers;
[Route("api/[controller]")]
[Authorize(Roles = "Administrator")]
[ApiController]
public class CounterController : ControllerBase
{
   private readonly DataContext _context;

   public CounterController(DataContext context)
   {
      _context = context;
   }

   //Get all counters
   [HttpGet]
   [Authorize(Roles = "User, Administrator")]
   public async Task<IActionResult> GetAllCounters()
   {
      var counters = await _context.Counters.ToListAsync();
      return Ok(counters);
   }

   //Get number of visitors
   [HttpGet]
   [ActionName("GetCounter")]
   [Route("{id:guid}")]
   public async Task<IActionResult> GetCounter([FromRoute] Guid id)
   {
      var visitorCount = await _context.Counters.FindAsync(id);
      if(visitorCount != null)
      {
         return Ok(visitorCount);
      }

      return NotFound(visitorCount);
   }

   //Add visitor count
   [HttpPost]
   public async Task<IActionResult> AddCounter([FromBody] Counter counter)
   {
      counter.Id = Guid.NewGuid();
      await _context.Counters.AddAsync(counter);
      await _context.SaveChangesAsync();

      return CreatedAtAction(nameof(GetCounter), new { id = counter.Id }, counter);
   }

   //Update visitors count
   [HttpPut]
   [Route("{id:guid}")]
   public async Task<IActionResult> UpdateCounter([FromRoute] Guid id, [FromBody] Counter visitorCounter)
   {
      var visitorCount = await _context.Counters.FindAsync(id);
      if(visitorCount != null)
      {
         visitorCount.Count = visitorCounter.Count;
         visitorCount.OverallCount = visitorCounter.OverallCount;
         visitorCount.OnlineCount = visitorCounter.OnlineCount;
         await _context.SaveChangesAsync();
         return Ok(visitorCount);
      }

      return NotFound(visitorCount);
   }

}
