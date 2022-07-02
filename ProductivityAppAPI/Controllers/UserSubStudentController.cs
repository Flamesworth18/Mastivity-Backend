using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductivityAppAPI.Models.Sub_Models;

namespace ProductivityAppAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UserSubStudentController : ControllerBase
{
   private readonly DataContext _context;

   public UserSubStudentController(DataContext context)
   {
      _context = context;
   }

   //Update subject student
   [HttpPut("{id:guid}"), Authorize(Roles = "User, Administrator")]
   public async Task<IActionResult> UpdateSubjectStudent([FromRoute] Guid id, [FromBody] UserSubStudent student)
   {
      var existingStudent = await _context.UserSubStudents.FindAsync(id);
      if(existingStudent != null)
      {
         existingStudent.FullName = student.FullName;
         existingStudent.Status = student.Status;
         existingStudent.Remarks = student.Remarks;
         await _context.SaveChangesAsync();
         return Ok(existingStudent);
      }

      return NotFound(existingStudent);
   }
}
