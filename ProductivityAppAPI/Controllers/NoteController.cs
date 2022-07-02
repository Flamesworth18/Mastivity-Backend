using Microsoft.AspNetCore.Mvc;
using ProductivityAppAPI.Models;

namespace ProductivityAppAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Administrator")]
public class NoteController : Controller
{
   private readonly DataContext _context;

   public NoteController(DataContext context)
   {
      _context = context;
   }

   //Get all notes
   [HttpGet]
   public async Task<IActionResult> GetAllCourse()
   {
      var courses = await _context.Notes.ToListAsync();
      return Ok(courses);
   }

   //Get note
   [HttpGet]
   [Route("{id:guid}")]
   [ActionName("GetNote")]
   public async Task<IActionResult> GetNote([FromRoute] Guid id)
   {
      var note = await _context.Notes.FirstOrDefaultAsync(x => x.Id == id);
      if(note != null)
      {
         return Ok(note);
      }

      return NotFound("Note not found,");
   }

   //Add note
   [HttpPost]
   public async Task<IActionResult> AddNote([FromBody] Notes note)
   {
      note.Id = Guid.NewGuid();
      await _context.Notes.AddAsync(note);
      await _context.SaveChangesAsync();

      return CreatedAtAction(nameof(GetNote), new { id = note.Id }, note);
   }

   //Edit note
   [HttpPut]
   [Route("{id:guid}")]
   public async Task<IActionResult> EditNote([FromRoute] Guid id, [FromBody] Notes note)
   {
      var existingNote = await _context.Notes.FindAsync(id);
      if(existingNote != null)
      {
         existingNote.Title = note.Title;
         existingNote.Description = note.Description;
         existingNote.Author = note.Author;
         existingNote.IsArchived = note.IsArchived;
         existingNote.DateUpdated = note.DateUpdated;

         await _context.SaveChangesAsync();
         return Ok(existingNote);
      }

      return NotFound("Note not found.");
   }

   //Remove note
   [HttpDelete]
   [Route("{id:guid}")]
   public async Task<IActionResult> RemoveNote([FromRoute] Guid id)
   {
      var note = await _context.Notes.FirstOrDefaultAsync(x => x.Id == id);
      if(note != null)
      {
         _context.Notes.Remove(note);
         await _context.SaveChangesAsync();
         return Ok(note);
      }

      return NotFound("Note not found.");
   }
}
