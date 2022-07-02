using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductivityAppAPI.Models;

namespace ProductivityAppAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TotalController : ControllerBase
{
   private readonly DataContext _context;

   public TotalController(DataContext context)
   {
      _context = context;
   }

   //GET totals
   [HttpGet]
   public async Task<IActionResult> GetTotals()
   {
      var totals = await _context.Totals.ToListAsync();
      if(totals == null)
      {
         return BadRequest("Total table does not exists!");
      }


      var users = await _context.Users.ToListAsync();
      var notes = await _context.Notes.ToListAsync();
      var todos = await _context.ToDos.ToListAsync();
      var tasks = await _context.Tasks.ToListAsync();
      var programs = await _context.Programs.ToListAsync();
      var subjects = await _context.Subjects.ToListAsync();
      var students = await _context.Students.ToListAsync();

      if(users == null &&
         notes == null &&
         todos == null &&
         tasks == null &&
         programs == null &&
         subjects == null &&
         students == null)
      {
         return BadRequest("One of the total values does not exists!");
      }

      totals[0].UserCount = users.Count;
      totals[0].NoteCount = notes.Count;
      totals[0].TodoCount = todos.Count;
      totals[0].TaskCount = tasks.Count;
      totals[0].ProgramCount = programs.Count;
      totals[0].SubjectCount = subjects.Count;
      totals[0].StudentCount = students.Count;

      return Ok(totals[0]);
   }

   
   //GET total
   [HttpGet("{id:guid}")]
   [ActionName("GetTotal")]
   public async Task<IActionResult> GetTotal([FromRoute] Guid id)
   {
      var total = await _context.Totals.FirstOrDefaultAsync(t => t.Id == id);
      if(total == null)
      {
         return BadRequest(total);
      }

      return Ok(total);
   }

   //POST total
   [HttpPost]
   public async Task<IActionResult> AddTotal([FromBody] Total total)
   {
      total.Id = Guid.NewGuid();
      await _context.Totals.AddAsync(total);
      await _context.SaveChangesAsync();

      return CreatedAtAction(nameof(GetTotal), new { id = total.Id }, total);
   }
}
