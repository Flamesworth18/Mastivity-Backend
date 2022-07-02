using Microsoft.AspNetCore.Mvc;
using ProductivityAppAPI.Models;

namespace ProductivityAppAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProgramController : Controller
{
   private readonly DataContext _context;

   public ProgramController(DataContext context)
   {
      _context = context;
   }

   //Get all courses
   [HttpGet, Authorize(Roles = "User, Administrator")]
   public async Task<IActionResult> GetAllPrograms()
   {
      var programs = await _context.Programs.Include(p => p.Subjects).ToListAsync();
      return Ok(programs);
   }

   //Get course
   [HttpGet, Authorize(Roles = "Administrator")]
   [Route("{id:guid}")]
   [ActionName("GetProgram")]
   public async Task<IActionResult> GetProgram([FromRoute] Guid id)
   {
      var program = await _context.Programs.Include(p => p.Subjects).FirstOrDefaultAsync(x => x.Id == id);
      if(program != null)
      {
         return Ok(program);
      }

      return NotFound("Program not found.");
   }

   //Add Course
   [HttpPost, Authorize(Roles = "Administrator")]
   public async Task<IActionResult> AddProgram([FromBody] Programs program)
   {
      program.Id = Guid.NewGuid();
      await _context.Programs.AddAsync(program);
      await _context.SaveChangesAsync();

      return CreatedAtAction(nameof(GetProgram), new { id = program.Id }, program); 
   }

   //Edit Course
   [HttpPut, Authorize(Roles = "Administrator")]
   [Route("{id:guid}")]
   public async Task<IActionResult> EditProgram([FromRoute] Guid id, [FromBody] Programs program)
   {
      program.Id = Guid.NewGuid();
      var existingProgram = await _context.Programs.Include(p => p.Subjects).FirstOrDefaultAsync(p => p.Id == id);
      if(existingProgram != null)
      {
         existingProgram.ProgramAbbreviation = program.ProgramAbbreviation;
         existingProgram.ProgramName = program.ProgramName;
         existingProgram.Semester = program.Semester;
         existingProgram.SchoolYear = program.SchoolYear;
         existingProgram.Department = program.Department;

         var subjects = existingProgram.Subjects.ToList();
         if (subjects != null)
         {

            existingProgram.Subjects.Clear();
            for (int i = 0; i < subjects.Count; i++)
            {
               _context.SubSubjects.Remove(subjects[i]);
            }

            existingProgram.Subjects = program.Subjects;
            await _context.SaveChangesAsync();
            return Ok(existingProgram);
         }

         return NotFound(existingProgram);
      }

      return NotFound("Program not found.");
   }

   //Remove course
   [HttpDelete, Authorize(Roles = "Administrator")]
   [Route("{id:guid}")]
   public async Task<IActionResult> RemoveProgram([FromRoute] Guid id)
   {
      var program = await _context.Programs.Include(p => p.Subjects).FirstOrDefaultAsync(x => x.Id == id);
      if(program != null)
      {
         _context.Programs.Remove(program);
         await _context.SaveChangesAsync();
         return Ok(program);
      }

      return NotFound("Program not found.");
   }
}
