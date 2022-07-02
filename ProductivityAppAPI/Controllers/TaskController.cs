using Microsoft.AspNetCore.Mvc;
using ProductivityAppAPI.Models;

namespace ProductivityAppAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Administrator")]
public class TaskController : Controller
{
   private readonly DataContext _context;

   public TaskController(DataContext context)
   {
      _context = context;
   }

   //Get all Task
   [HttpGet]
   public async Task<IActionResult> GetAllTask()
   {
      var tasks = await _context.Tasks.ToListAsync();
      return Ok(tasks);
   }

   //Get task
   [HttpGet]
   [Route("{id:guid}")]
   [ActionName("GetTask")]
   public async Task<IActionResult> GetTask([FromRoute] Guid id)
   {
      var task = await _context.Tasks.FirstOrDefaultAsync(x => x.Id == id);
      if(task != null)
      {
         return Ok(task);
      }

      return NotFound(task);
   }

   //Add task
   [HttpPost]
   public async Task<IActionResult> AddTask([FromBody] Models.Tasks task)
   {
      task.Id = Guid.NewGuid();
      await _context.Tasks.AddAsync(task);
      await _context.SaveChangesAsync();

      return CreatedAtAction(nameof(AddTask), new { id = task.Id }, task);
   }

   //Edit task
   [HttpPut]
   [Route("{id:guid}")]
   public async Task<IActionResult> EditTask([FromRoute] Guid id, [FromBody] Models.Tasks task)
   {
      var existingTask = await _context.Tasks.FindAsync(id);
      if(existingTask != null)
      {
         existingTask.Title = task.Title;
         existingTask.Author = task.Author;
         existingTask.IsCompleted = task.IsCompleted;
         existingTask.Status = task.Status;
         existingTask.DateUpdated = task.DateUpdated;

         await _context.SaveChangesAsync();
         return Ok(existingTask);
      }

      return BadRequest(existingTask);
   }

   //Delete task
   [HttpDelete]
   [Route("{id:guid}")]
   public async Task<IActionResult> DeleteTask([FromRoute] Guid id)
   {
      var task = await _context.Tasks.FirstOrDefaultAsync(x => x.Id == id);
      if(task != null)
      {
         _context.Tasks.Remove(task);
         await _context.SaveChangesAsync();
         return Ok(task);
      }

      return NotFound(task);   
   }
}
