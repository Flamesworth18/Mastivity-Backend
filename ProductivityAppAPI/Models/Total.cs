namespace ProductivityAppAPI.Models;

public class Total
{
   [Key]
   public Guid Id { get; set; }
   public int UserCount { get; set; }
   public int NoteCount { get; set; }
   public int TodoCount { get; set; }
   public int TaskCount { get; set; }
   public int ProgramCount { get; set; }
   public int SubjectCount { get; set; }
   public int StudentCount { get; set; }
}
