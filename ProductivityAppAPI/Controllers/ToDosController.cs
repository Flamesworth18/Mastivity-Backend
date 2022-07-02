using Microsoft.AspNetCore.Mvc;
using ProductivityAppAPI.Models;

namespace ProductivityAppAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Administrator")]
public class ToDosController : Controller
{
   private readonly DataContext _context;

   public ToDosController(DataContext context)
   {
      _context = context;
   }

   //Get all todos
   [HttpGet]
   public async Task<IActionResult> GetAllToDos()
   {
      var todos = await _context.ToDos.Include(t => t.Tasks).ToListAsync();
      return Ok(todos);
   }

   //Get todo
   [HttpGet]
   [Route("{id:guid}")]
   [ActionName("GetToDo")]
   public async Task<IActionResult> GetToDo([FromRoute] Guid id)
   {
      var todo = await _context.ToDos.Include(t => t.Tasks).FirstOrDefaultAsync(x => x.Id == id);
      if(todo != null)
      {
         return Ok(todo);
      }

      return NotFound("Todo not found.");
   }

   //Add Todo
   [HttpPost]
   public async Task<IActionResult> AddToDo([FromBody] ToDos todo)
   {
      todo.Id = Guid.NewGuid();
      todo.StatusId = 2;

      await _context.ToDos.AddAsync(todo);
      await _context.SaveChangesAsync();

      return CreatedAtAction(nameof(GetToDo), new { id = todo.Id }, todo);
   }

   //Edit todo
   [HttpPut]
   [Route("{id:guid}")]
   public async Task<IActionResult> EditToDo([FromRoute] Guid id, [FromBody] ToDos todo)
   {
      var existingToDo = await _context.ToDos.FindAsync(id);
      if(existingToDo != null)
      {
         existingToDo.Title = todo.Title;
         existingToDo.Author = todo.Author;
         existingToDo.Due = todo.Due;
         existingToDo.Tasks = todo.Tasks;
         existingToDo.TaskCompleted = todo.TaskCompleted;
         existingToDo.StatusId = todo.StatusId;
         existingToDo.IsArchvied = todo.IsArchvied;
         existingToDo.DateUpdated = todo.DateUpdated;

         await _context.SaveChangesAsync();
         return Ok(existingToDo);
      }

      return BadRequest(existingToDo);
   }

   //Remove todo
   [HttpDelete]
   [Route("{id:guid}")]
   public async Task<IActionResult> RemoveToDo([FromRoute] Guid id)
   {
      var todo = await _context.ToDos.Include(t => t.Tasks).FirstOrDefaultAsync(x => x.Id == id);
      if (todo != null)
      {
         _context.ToDos.Remove(todo);
         await _context.SaveChangesAsync();

         return Ok(todo);
      }

      return NotFound("Todo not found.");
   }
}
