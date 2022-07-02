using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductivityAppAPI.Models;

namespace ProductivityAppAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class StudentStatusController : ControllerBase
{
   private readonly DataContext _context;

   public StudentStatusController(DataContext context)
   {
      _context = context;
   }

   //Get all student statuses
   [HttpGet, Authorize(Roles = "User, Administrator")]
   public async Task<IActionResult> GetAllStudentStatuses()
   {
      var gradeStatuses = await _context.StudentStatuses.ToListAsync();
      return Ok(gradeStatuses);
   }

   //Get student status
   [HttpGet, Authorize(Roles = "Administrator")]
   [Route("{id:guid}")]
   [ActionName("GetStudentStatus")]
   public async Task<IActionResult> GetStudentStatus([FromRoute] Guid id)
   {
      var gradeStatus = await _context.StudentStatuses.FindAsync(id);
      if(gradeStatus != null)
      {
         return Ok(gradeStatus);
      }

      return NotFound(gradeStatus);
   }

   //Add student status
   [HttpPost, Authorize(Roles = "Administrator")]
   public async Task<IActionResult> AddStudentStatus([FromBody] StudentStatus studentStatus)
   {
      studentStatus.Id = Guid.NewGuid();
      await _context.StudentStatuses.AddAsync(studentStatus);
      await _context.SaveChangesAsync();
      
      return CreatedAtAction(nameof(GetStudentStatus), new { id = studentStatus.Id }, studentStatus);
   }

   //Update student status
   [HttpPut, Authorize(Roles = "Administrator")]
   [Route("{id:guid}")]
   public async Task<IActionResult> UpdateStudentStatus([FromRoute] Guid id, [FromBody] StudentStatus studentStatus)
   {
      var existingStudentStatus = await _context.StudentStatuses.FindAsync(id);
      if(existingStudentStatus != null)
      {
         existingStudentStatus.Status = studentStatus.Status;
         await _context.SaveChangesAsync();
         return Ok(existingStudentStatus);
      }

      return BadRequest(existingStudentStatus);
   }

   //Remove student status
   [HttpDelete, Authorize(Roles = "Administrator")]
   [Route("{id:guid}")]
   public async Task<IActionResult> RemoveStudentStatus([FromRoute] Guid id)
   {
      var studentStatus = await _context.StudentStatuses.FindAsync(id);
      if (studentStatus != null)
      {
         _context.StudentStatuses.Remove(studentStatus);
         await _context.SaveChangesAsync();
         return Ok(studentStatus);
      }

      return BadRequest(studentStatus);
   }
}
