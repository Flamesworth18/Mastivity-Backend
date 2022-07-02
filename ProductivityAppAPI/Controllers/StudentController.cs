using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductivityAppAPI.Models;

namespace ProductivityAppAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class StudentController : ControllerBase
{
   private readonly DataContext _context;

   public StudentController(DataContext context)
   {
      _context = context;
   }

   //Get all students
   [HttpGet, Authorize(Roles = "User, Administrator")]
   public async Task<IActionResult> GetAllStudents()
   {
      var students = await _context.Students.ToListAsync();
      return Ok(students);
   }

   //Get student
   [HttpGet, Authorize(Roles = "Administrator")]
   [Route("{id:guid}")]
   [ActionName("GetStudent")]
   public async Task<IActionResult> GetStudent([FromRoute] Guid id)
   {
      var student = await _context.Students.FindAsync(id);
      if(student != null)
      {
         return Ok(student);
      }

      return NotFound(student);
   }

   //Add student
   [HttpPost, Authorize(Roles = "Administrator")]
   public async Task<IActionResult> AddStudent([FromBody] Student student)
   {
      student.Id = Guid.NewGuid();
      await _context.Students.AddAsync(student);
      await _context.SaveChangesAsync();

      return CreatedAtAction(nameof(AddStudent), new { id = student.Id }, student);
   }

   //Update student
   [HttpPut, Authorize(Roles = "User, Administrator")]
   [Route("{id:guid}")]
   public async Task<IActionResult> UpdateStudent([FromRoute] Guid id, [FromBody] Student student)
   {
      var existingStudent = await _context.Students.FindAsync(id);
      if(existingStudent != null)
      {
         existingStudent.FullName = student.FullName;
         existingStudent.YearLevel = student.YearLevel;
         existingStudent.Program = student.Program;
         existingStudent.Status = student.Status;

         await _context.SaveChangesAsync();
         return Ok(existingStudent);
      }

      return BadRequest(existingStudent);
   }

   //Remove student
   [HttpDelete, Authorize(Roles = "Administrator")]
   [Route("{id:guid}")]
   public async Task<IActionResult> RemoveUser([FromRoute] Guid id)
   {
      var student = await _context.Students.FindAsync(id);
      if(student != null)
      {
         _context.Students.Remove(student);
         await _context.SaveChangesAsync();
         return Ok(student);
      }

      return NotFound(student);
   }

}
