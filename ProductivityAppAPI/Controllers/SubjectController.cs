using Microsoft.AspNetCore.Mvc;
using ProductivityAppAPI.Models;

namespace ProductivityAppAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubjectController : Controller
{
   private readonly DataContext _context;

   public SubjectController(DataContext context)
   {
      _context = context;
   }

   //Get all subjects
   [HttpGet, Authorize(Roles = "User, Administrator")]
   public async Task<IActionResult> GetAllSubjects()
   {
      var subjects = await _context.Subjects.Include(s => s.Students).Include(d => d.Days).ToListAsync();
      return Ok(subjects);
   }

   //Get subject
   [HttpGet, Authorize(Roles = "Administrator")]
   [Route("{id:guid}")]
   [ActionName("GetSubject")]
   public async Task<IActionResult> GetSubject([FromRoute] Guid id)
   {
      var subject = await _context.Subjects.Include(s => s.Students).Include(d => d.Days).FirstOrDefaultAsync(x => x.Id == id);
      if(subject != null)
      {
         return Ok(subject);
      }

      return NotFound("Subject not found.");
   }

   //Add subject
   [HttpPost, Authorize(Roles = "Administrator")]
   public async Task<IActionResult> AddSubject([FromBody] Subject subject)
   {
      subject.Id = Guid.NewGuid();
      await _context.Subjects.AddAsync(subject);
      await _context.SaveChangesAsync();

      return CreatedAtAction(nameof(GetSubject), new { id = subject.Id }, subject);
   }

   //Edit subject
   [HttpPut, Authorize(Roles = "Administrator")]
   [Route("{id:guid}")]
   public async Task<IActionResult> EditSubject([FromRoute] Guid id, [FromBody] Subject subject)
   {
      var existingSubject = await _context.Subjects.Include(s => s.Students).Include(d => d.Days).FirstOrDefaultAsync(s => s.Id == id);
      if(existingSubject != null)
      {
         existingSubject.SubjectCode = subject.SubjectCode;
         existingSubject.SubjectName = subject.SubjectName;
         existingSubject.StartingTime = subject.StartingTime;
         existingSubject.EndingTime = subject.EndingTime;
         existingSubject.StartingTimeView = subject.StartingTimeView;
         existingSubject.EndingTimeView = subject.EndingTimeView;
         existingSubject.StartingDate = subject.StartingDate;
         existingSubject.EndingDate = subject.EndingDate;
         existingSubject.DaysView = subject.DaysView;
         existingSubject.OnMonday = subject.OnMonday;
         existingSubject.OnTuesday = subject.OnTuesday;
         existingSubject.OnWednesday = subject.OnWednesday;
         existingSubject.OnThursday = subject.OnThursday;
         existingSubject.OnFriday = subject.OnFriday;
         existingSubject.Department = subject.Department;

         var students = existingSubject.Students.ToList();
         var days = existingSubject.Days.ToList();
         if (students != null && days != null)
         {

            for (int i = 0; i < existingSubject.Students.Count; i++)
            {
               _context.SubStudents.Remove(existingSubject.Students[i]);
            }
            existingSubject.Students.Clear();

            for (int i = 0; i < subject.Students.Count; i++)
            {
               subject.Students[i].Id = Guid.NewGuid();
               existingSubject.Students.Add(subject.Students[i]);
               await _context.SubStudents.AddAsync(subject.Students[i]);
            }

            for (int i = 0; i < existingSubject.Days.Count; i++)
            {
               _context.Days.Remove(days[i]);
            }
            existingSubject.Days.Clear();

            for (int i = 0; i < subject.Days.Count; i++)
            {
               subject.Days[i].Id = Guid.NewGuid();
               existingSubject.Days.Add(subject.Days[i]);
               await _context.Days.AddAsync(subject.Days[i]);
            }

            await _context.SaveChangesAsync();
            return Ok(existingSubject);
         }

         return NotFound(existingSubject);
      }

      return BadRequest(existingSubject);
   }

   //Remove subject
   [HttpDelete, Authorize(Roles = "Administrator")]
   [Route("{id:guid}")]
   public async Task<IActionResult> RemoveSubject([FromRoute] Guid id)
   {
      var subject = await _context.Subjects.Include(s => s.Students).Include(d => d.Days).FirstOrDefaultAsync(x => x.Id == id);
      if(subject != null)
      {
         subject.Students.Clear();
         subject.Days.Clear();
         _context.Subjects.Remove(subject);
         await _context.SaveChangesAsync();
         return Ok(subject);
      }

      return NotFound("Subject not found.");
   }

   //Add subject day
   [HttpPost("{id:guid}/Days"), Authorize(Roles = "User, Administrator")]
   public async Task<IActionResult> AddSubjectDay([FromRoute] Guid id, [FromBody] Day day)
   {
      var subject = await _context.Subjects.Include(p => p.Days).FirstOrDefaultAsync(u => u.Id == id);
      if (subject != null)
      {
         day.Id = Guid.NewGuid();

         subject.Days.Add(day);

         await _context.Days.AddAsync(day);
         await _context.SaveChangesAsync();
         return Ok(day);
      }

      return NotFound(subject);
   }

   //Remove subject day
   [HttpDelete("{id:guid}/Days/abbreviation"), Authorize(Roles = "User, Administrator")]
   public async Task<IActionResult> RemoveSubjectDay([FromRoute] Guid id, [FromRoute] string abbreviation)
   {
      var subject = await _context.Subjects.Include(p => p.Days).FirstOrDefaultAsync(u => u.Id == id);
      if (subject != null)
      {
         var day = await _context.Days.FirstOrDefaultAsync(p => p.Abbreviation == abbreviation);
         if (day != null)
         {
            subject.Days?.Remove(day);
            _context.Days.Remove(day);
            await _context.SaveChangesAsync();

            return Ok(day);
         }

         return NotFound(subject);
      }

      return NotFound(subject);
   }
}
