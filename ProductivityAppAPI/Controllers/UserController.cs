using Microsoft.AspNetCore.Mvc;
using ProductivityAppAPI.Models;
using ProductivityAppAPI.Models.Sub_Models;
using ProductivityAppAPI.Static;

namespace ProductivityAppAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : Controller
{
   private readonly DataContext _context;

   public UserController(DataContext context)
   {
      _context = context;
   }

   #region-----------------------------USERS-----------------------------

   //Get all users
   [HttpGet, Authorize(Roles = "Administrator")]
   public async Task<IActionResult> GetAllUsers()
   {
      var user = await _context.Users
         .Include(c => c.ProgramHandled)
         .Include(s => s.Schedules)
         .Include(t => t.ToDos)
         .Include(n => n.Notes)
         .ToListAsync(); ;

      return Ok(user);
   }

   //Get user
   [HttpGet, Authorize(Roles = "Administrator")]
   [Route("{id:guid}")]
   [ActionName("GetUser")]
   public async Task<IActionResult> GetUser([FromRoute] Guid id)
   {
      var user = await _context.Users
         .Include(c => c.ProgramHandled)
         .Include(s => s.Schedules)
         .Include(t => t.ToDos)
         .Include(n => n.Notes)
         .FirstOrDefaultAsync(x => x.Id == id);

      if(user != null)
      {
         return Ok(user);
      }

      return NotFound("User not found.");
   }

   //Add user
   [HttpPost, Authorize(Roles = "Administrator")]
   [Route("Add-User")]
   public async Task<IActionResult> AddUser([FromBody] SubUser subUser)
   {
      TokenGenerator.CreatePasswordHash(subUser.Password, out byte[] passwordHash, out byte[] passwordSalt);

      RequestUser user = new RequestUser();
      user.Id = Guid.NewGuid();
      user.FirstName = subUser.FirstName;
      user.LastName = subUser.LastName;
      user.Username = subUser.Username;
      user.Email = subUser.Email;
      user.PasswordHash = passwordHash;
      user.PasswordSalt = passwordSalt;
      user.VerificationToken = TokenGenerator.CreateRandomVerificationToken();
      user.Role = subUser.Role;
      user.UserCreated = DateTime.UtcNow.ToShortDateString();

      await _context.RequestUsers.AddAsync(user);
      await _context.SaveChangesAsync();

      return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
   }

   //Add user
   [HttpPost, Authorize(Roles = "Administrator")]
   [Route("Request-Users")]
   public async Task<IActionResult> AddRequestUsers([FromBody] RequestUser[] requestUsers)
   {

      for (int i = 0; i < requestUsers.Length; i++)
      {
         User user = new User();
         user.Id = requestUsers[i].Id;
         user.FirstName = requestUsers[i].FirstName;
         user.LastName = requestUsers[i].LastName;
         user.Username = requestUsers[i].Username;
         user.Email = requestUsers[i].Email;
         user.PasswordHash = requestUsers[i].PasswordHash;
         user.PasswordSalt = requestUsers[i].PasswordSalt;
         user.VerificationToken = requestUsers[i].VerificationToken;
         user.IsVerify = requestUsers[i].IsVerify;
         user.PasswordResetToken = requestUsers[i].PasswordResetToken;
         user.PasswordTokenExpires = requestUsers[i].PasswordTokenExpires;
         user.Role = requestUsers[i].Role;
         user.UserCreated = requestUsers[i].UserCreated;

         await _context.Users.AddAsync(user);
      }

      await _context.SaveChangesAsync();

      return Ok();
   }

   //Update user
   [HttpPut, Authorize(Roles = "Administrator")]
   [Route("{id:guid}")]
   public async Task<IActionResult> UpdateUser([FromRoute] Guid id, [FromBody] User user)
   {
      var existingUser = await _context.Users.FindAsync(id);
      if(existingUser != null)
      {
         existingUser.FirstName = user.FirstName;
         existingUser.LastName = user.LastName;
         existingUser.Username = user.Username;
         existingUser.ProgramHandled = user.ProgramHandled;
         existingUser.SubjectHandled = user.SubjectHandled;
         existingUser.StudentHandled = user.StudentHandled;
         existingUser.Schedules = user.Schedules;
         existingUser.ToDos = user.ToDos;
         existingUser.Notes = user.Notes;
         existingUser.Role = user.Role;
         existingUser.IsVerify = user.IsVerify;

         await _context.SaveChangesAsync();
         return Ok(existingUser);
      }

      return NotFound("User not found.");
   }

   //Remove user
   [HttpDelete, Authorize(Roles = "Administrator")]
   [Route("{id:guid}")]
   public async Task<IActionResult> RemoveUser([FromRoute] Guid id)
   {
      var user = await _context.Users
         .Include(p => p.ProgramHandled)
         .Include(s => s.SubjectHandled)
         .Include(s => s.StudentHandled)
         .Include(s => s.Schedules)
         .Include(t => t.ToDos)
         .Include(n => n.Notes)
         .FirstOrDefaultAsync(x => x.Id == id);

      if(user != null)
      {
         _context.Users.Remove(user);
         await _context.SaveChangesAsync();

         return Ok(user);
      }

      return NotFound("User not found.");
   }

   #endregion

   #region-----------------------------NOTES-----------------------------

   //Get all user notes
   [HttpGet("{id:guid}/Notes"), Authorize(Roles = "User, Administrator")]
   public async Task<IActionResult> GetAllUserNotes([FromRoute] Guid id)
   {
      var user = await _context.Users.Include(n => n.Notes).FirstOrDefaultAsync(u => u.Id == id);
      if(user != null)
      {
         var userNotes =  user.Notes.ToList();
         if(userNotes != null)
         {
            return Ok(userNotes);
         }
         return Ok();
      }

      return NotFound(user);
   }

   //Add user note
   [HttpPost("{id:guid}/Notes"), Authorize(Roles = "User, Administrator")]
   public async Task<IActionResult> AddUserNote([FromRoute] Guid id, [FromBody] Notes note)
   {
      var user = await _context.Users.Include(n => n.Notes).FirstOrDefaultAsync(u => u.Id == id);
      if(user != null)
      {
         note.Id = Guid.NewGuid();

         user.Notes.Add(note);
         await _context.Notes.AddAsync(note);
         await _context.SaveChangesAsync();
         return Ok(note);
      }

      return NotFound(user);
   }

   //Update user note
   [HttpPut("{id:guid}/Notes/{noteId:guid}"), Authorize(Roles = "User, Administrator")]
   public async Task<IActionResult> UpdateUserNote([FromRoute] Guid id, [FromRoute] Guid noteId, [FromBody] Notes note)
   {
      var user = await _context.Users.Include(n => n.Notes).FirstOrDefaultAsync(u => u.Id == id);
      if(user != null)
      {
         var existingNote = user.Notes.FirstOrDefault(n => n.Id == noteId);

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

         return NotFound(existingNote);
      }

      return NotFound(user);
   }

   //Remove user note
   [HttpDelete("{id:guid}/Notes/{noteId:guid}"), Authorize(Roles = "User, Administrator")]
   public async Task<IActionResult> RemoveUserNote([FromRoute] Guid id, [FromRoute] Guid noteId)
   {
      var user = await _context.Users.Include(n => n.Notes).FirstOrDefaultAsync(u => u.Id == id);
      if(user != null)
      {
         var note = user.Notes?.FirstOrDefault(n => n.Id == noteId);
         user.Notes?.Remove(note);

         _context.Notes.Remove(note);
         await _context.SaveChangesAsync();
         return Ok(note);
      }

      return NotFound(user);
   }
   #endregion

   #region -----------------------------TODOS-----------------------------

   //Get all user todos
   [HttpGet("{id:guid}/Todos"), Authorize(Roles = "User, Administrator")]
   public async Task<IActionResult> GetAllUserTodos([FromRoute] Guid id)
   {
      var user = await _context.Users.Include(t => t.ToDos).ThenInclude(t => t.Tasks).FirstOrDefaultAsync(u => u.Id == id);
      if (user != null)
      {
         var userTodos = user.ToDos.ToList();
         if(userTodos != null)
         {
            return Ok(userTodos);
         }
         return Ok();
      }

      return NotFound(user);
   }

   //Add user todo
   [HttpPost("{id:guid}/Todos"), Authorize(Roles = "User, Administrator")]
   public async Task<IActionResult> AddUserTodo([FromRoute] Guid id, [FromBody] ToDos todo)
   {
      var user = await _context.Users.Include(t => t.ToDos).ThenInclude(t => t.Tasks).FirstOrDefaultAsync(u => u.Id == id);
      if (user != null)
      {
         todo.Id = Guid.NewGuid();


         user.ToDos.Add(todo);

         await _context.ToDos.AddAsync(todo);

         await _context.SaveChangesAsync();
         return Ok(todo);
      }

      return NotFound(user);
   }

   //Update user todo
   [HttpPut("{id:guid}/Todos/{todoId:guid}"), Authorize(Roles = "User, Administrator")]
   public async Task<IActionResult> UpdateUserTodo([FromRoute] Guid id, [FromRoute] Guid todoId, [FromBody] ToDos todo)
   {
      var user = await _context.Users.Include(t => t.ToDos).ThenInclude(t => t.Tasks).FirstOrDefaultAsync(u => u.Id == id);
      if (user != null)
      {
         var existingTodo = user.ToDos.FirstOrDefault(t => t.Id == todoId);

         if(existingTodo != null)
         {
            existingTodo.Title = todo.Title;
            existingTodo.Due = todo.Due;

            foreach(var task in existingTodo.Tasks)
            {
               if (!todo.Tasks.Contains(task))
               {
                  existingTodo.Tasks.Remove(task);
                  _context.Tasks.Remove(task);
               }
            }

            foreach(var task in todo.Tasks)
            {
               var t = existingTodo.Tasks.FirstOrDefault(x => x.Id == task.Id);
               if(t != null)
               {
                  t.Title = task.Title;
                  t.IsCompleted = task.IsCompleted;
               }

               if (!existingTodo.Tasks.Contains(task))
               {
                  task.Id = Guid.NewGuid();
                  existingTodo.Tasks.Add(task);
                  await _context.Tasks.AddAsync(task);
               }
            }

            existingTodo.TaskCompleted = todo.TaskCompleted;
            existingTodo.StatusId = todo.StatusId;
            existingTodo.Status = todo.Status;

            await _context.SaveChangesAsync();
            return Ok(existingTodo);
         }

         return NotFound(existingTodo);
         
      }

      return NotFound(user);
   }

   //Remove user todo
   [HttpDelete("{id:guid}/Todos/{todoId:guid}"), Authorize(Roles = "User, Administrator")]
   public async Task<IActionResult> RemoveUserTodo([FromRoute] Guid id, [FromRoute] Guid todoId)
   {
      var user = await _context.Users.Include(t => t.ToDos).ThenInclude(t => t.Tasks).FirstOrDefaultAsync(u => u.Id == id);
      if (user != null)
      {
         var todo = user.ToDos.FirstOrDefault(t => t.Id == todoId);

         user.ToDos.Remove(todo);
         _context.ToDos.Remove(todo);

         var task = todo.Tasks.ToList();
         for (int i = 0; i < task.Count; i++)
         {
            _context.Remove(task[i]);
         }

         await _context.SaveChangesAsync();
         return Ok(todo);
      }

      return NotFound(user);
   }
   #endregion

   #region -----------------------------PROGRAMS-----------------------------

   //Get all user programs
   [HttpGet("{id:guid}/Programs"), Authorize(Roles = "User, Administrator")]
   public async Task<IActionResult> GetAllUserPrograms([FromRoute] Guid id)
   {
      var user = await _context.Users.Include(u => u.ProgramHandled).ThenInclude(p => p.Subjects).FirstOrDefaultAsync(u => u.Id == id);
      if (user != null)
      {
         var userProgams = user.ProgramHandled.ToList();
         
         return Ok(userProgams);
      }
      return NotFound(user);
   }

   //Add user program
   [HttpPost("{id:guid}/Programs"), Authorize(Roles = "User, Administrator")]
   public async Task<IActionResult> AddUserProgram([FromRoute] Guid id, [FromBody] UserProgram program)
   {
      var user = await _context.Users.Include(u => u.ProgramHandled).ThenInclude(p => p.Subjects).FirstOrDefaultAsync(u => u.Id == id);
      if (user != null)
      {
         program.Id = Guid.NewGuid();

         user.ProgramHandled.Add(program);

         var programSubjects = program.Subjects.ToList();
         if(programSubjects != null)
         {
            for(int i = 0; i < programSubjects.Count; i++)
            {
               programSubjects[i].Id = Guid.NewGuid();
               await _context.UserSubSubjects.AddAsync(programSubjects[i]);
            }
         }

         await _context.UserPrograms.AddAsync(program);
         await _context.SaveChangesAsync();
         return Ok(program);
      }

      return NotFound(user);
   }

   //update user program
   [HttpPut("{id:guid}/Programs/{programId:guid}"), Authorize(Roles = "User, Administrator")]
   public async Task<IActionResult> UpdateUserProgram([FromRoute] Guid id, [FromRoute] Guid programId, [FromBody] UserProgram program)
   {
      var user = await _context.Users.Include(u => u.ProgramHandled).ThenInclude(p => p.Subjects).FirstOrDefaultAsync(u => u.Id == id);
      if(user != null)
      {
         var userProgram = await _context.UserPrograms.Include(p => p.Subjects).FirstOrDefaultAsync(p => p.Id == programId);
         if(userProgram != null)
         {
            var userSubjects = userProgram.Subjects.ToList();
            if(userSubjects != null)
            {

               foreach (var s in userSubjects)
               {
                  if (!program.Subjects.Contains(s))
                  {
                     userProgram.Subjects.Remove(s);
                     _context.UserSubSubjects.Remove(s);
                  }
               }

               foreach (var s in program.Subjects)
               {
                  if (!userSubjects.Contains(s))
                  {
                     s.Id = Guid.NewGuid();
                     userProgram.Subjects.Add(s);
                     await _context.UserSubSubjects.AddAsync(s);
                  }
               }

               await _context.SaveChangesAsync();
               return Ok(userProgram);
            }

            return BadRequest(userProgram);
         }

         return BadRequest(userProgram);
      }

      return NotFound(user);
   }

   //Remove user program
   [HttpDelete("{id:guid}/Programs/{programId:guid}"), Authorize(Roles = "User, Administrator")]
   public async Task<IActionResult> RemoveUserProgram([FromRoute] Guid id, [FromRoute] Guid programId)
   {
      var user = await _context.Users.Include(u => u.ProgramHandled).ThenInclude(p => p.Subjects).FirstOrDefaultAsync(u => u.Id == id);
      if (user != null)
      {
         var program = await _context.UserPrograms.FirstOrDefaultAsync(p => p.Id == programId);
         if(program != null)
         {

            user.ProgramHandled?.Remove(program);
            _context.UserPrograms.Remove(program);
            await _context.SaveChangesAsync();

            return Ok(program);
         }

         return NotFound(program);
      }

      return NotFound(user);
   }

   #endregion

   #region -----------------------------SUBJECTS-----------------------------

   //Get all user subjects
   [HttpGet("{id:guid}/Subjects"), Authorize(Roles = "User, Administrator")]
   public async Task<IActionResult> GetAllUserSubjects([FromRoute] Guid id)
   {
      var user = await _context.Users.Include(u => u.SubjectHandled).ThenInclude(p => p.Students).FirstOrDefaultAsync(u => u.Id == id);
      if (user != null)
      {
         var userSubjects = user.SubjectHandled.ToList();

         return Ok(userSubjects);
      }
      return NotFound(user);
   }

   //Add user subject
   [HttpPost("{id:guid}/Subjects"), Authorize(Roles = "User, Administrator")]
   public async Task<IActionResult> AddUserSubject([FromRoute] Guid id, [FromBody] UserSubject subject)
   {
      var user = await _context.Users.Include(u => u.SubjectHandled).ThenInclude(s => s.Students).FirstOrDefaultAsync(u => u.Id == id);
      if (user != null)
      {
         subject.Id = Guid.NewGuid();

         user.SubjectHandled.Add(subject);

         var subjectStudents = subject.Students.ToList();
         if (subjectStudents != null)
         {
            for (int i = 0; i < subjectStudents.Count; i++)
            {
               subjectStudents[i].Id = Guid.NewGuid();
               await _context.UserSubStudents.AddAsync(subjectStudents[i]);
            }
         }

         await _context.UserSubjects.AddAsync(subject);
         await _context.SaveChangesAsync();
         return Ok(subject);
      }

      return NotFound(user);
   }

   //update user subject
   [HttpPut("{id:guid}/Subjects/{subjectId:guid}"), Authorize(Roles = "User, Administrator")]
   public async Task<IActionResult> UpdateUserSubject([FromRoute] Guid id, [FromRoute] Guid subjectId, [FromBody] UserSubject subject)
   {
      var user = await _context.Users.Include(u => u.SubjectHandled).ThenInclude(p => p.Students).FirstOrDefaultAsync(u => u.Id == id);
      if (user != null)
      {
         var userSubject = await _context.UserSubjects.Include(p => p.Students).FirstOrDefaultAsync(p => p.Id == subjectId);
         if (userSubject != null)
         {
            var userStudents = userSubject.Students.ToList();
            if (userStudents != null)
            {

               foreach (var s in userStudents)
               {
                  if (!subject.Students.Contains(s))
                  {
                     userSubject.Students.Remove(s);
                     _context.UserSubStudents.Remove(s);
                  }
               }

               foreach (var s in subject.Students)
               {
                  if (!userStudents.Contains(s))
                  {
                     s.Id = Guid.NewGuid();
                     userSubject.Students.Add(s);
                     await _context.UserSubStudents.AddAsync(s);
                  }
               }

               await _context.SaveChangesAsync();
               return Ok(userSubject);
            }

            return BadRequest(userSubject);
         }

         return BadRequest(userSubject);
      }

      return NotFound(user);
   }

   //Remove user subject
   [HttpDelete("{id:guid}/Subjects/{subjectId:guid}"), Authorize(Roles = "User, Administrator")]
   public async Task<IActionResult> RemoveUserSubject([FromRoute] Guid id, [FromRoute] Guid subjectId)
   {
      var user = await _context.Users.Include(u => u.SubjectHandled).ThenInclude(p => p.Students).FirstOrDefaultAsync(u => u.Id == id);
      if (user != null)
      {
         var subject = await _context.UserSubjects.FirstOrDefaultAsync(p => p.Id == subjectId);
         if (subject != null)
         {
            user.SubjectHandled?.Remove(subject);
            _context.UserSubjects.Remove(subject);
            await _context.SaveChangesAsync();

            return Ok(subject);
         }

         return NotFound(subject);
      }

      return NotFound(user);
   }

   #endregion

   #region -----------------------------SCHEDULE-----------------------------

   //Get all user schedules
   [HttpGet("{id:guid}/Schedules"), Authorize(Roles = "User, Administrator")]
   public async Task<IActionResult> GetAllUserSchedules([FromRoute] Guid id)
   {
      var user = await _context.Users.Include(u => u.Schedules).FirstOrDefaultAsync(u => u.Id == id);
      if (user != null)
      {
         var userSchedules = user.Schedules.ToList();

         return Ok(userSchedules);
      }
      return NotFound(user);
   }

   //Add user schedule
   [HttpPost("{id:guid}/Schedules"), Authorize(Roles = "User, Administrator")]
   public async Task<IActionResult> AddUserSchedule([FromRoute] Guid id, [FromBody] Schedule schedule)
   {
      var user = await _context.Users.Include(u => u.Schedules).FirstOrDefaultAsync(u => u.Id == id);
      if (user != null)
      {
         schedule.Id = Guid.NewGuid();

         user.Schedules.Add(schedule);

         await _context.Schedules.AddAsync(schedule);
         await _context.SaveChangesAsync();
         return Ok(schedule);
      }

      return NotFound(user);
   }

   //update user schedule
   [HttpPut("{id:guid}/Schedules/{scheduleId:guid}"), Authorize(Roles = "User, Administrator")]
   public async Task<IActionResult> UpdateUserSchedule([FromRoute] Guid id, [FromRoute] Guid scheduleId, [FromBody] Schedule schedule)

   {
      var user = await _context.Users.Include(n => n.Schedules).FirstOrDefaultAsync(u => u.Id == id);
      if (user != null)
      {
         var existingSchedule = user.Schedules.FirstOrDefault(s => s.Id == scheduleId);

         if (existingSchedule != null)
         {
            existingSchedule.Title = schedule.Title;
            existingSchedule.Date = schedule.Date;
            existingSchedule.Color = schedule.Color;
            existingSchedule.Author = schedule.Author;
            existingSchedule.DateUpdated = schedule.DateUpdated;

            await _context.SaveChangesAsync();
            return Ok(existingSchedule);
         }

         return NotFound(existingSchedule);
      }

      return NotFound(user);
   }

   //Remove user schedule
   [HttpDelete("{id:guid}/Schedules/{scheduleId:guid}"), Authorize(Roles = "User, Administrator")]
   public async Task<IActionResult> RemoveUserSchedule([FromRoute] Guid id, [FromRoute] Guid scheduleId)
   {
      var user = await _context.Users.Include(s => s.Schedules).FirstOrDefaultAsync(u => u.Id == id);
      if (user != null)
      {
         var schedule = user.Schedules?.FirstOrDefault(s => s.Id == scheduleId);
         user.Schedules?.Remove(schedule);

         _context.Schedules.Remove(schedule);
         await _context.SaveChangesAsync();
         return Ok(schedule);
      }

      return NotFound(user);
   }
   #endregion
}
