using ProductivityAppAPI.Models.Sub_Models;

namespace ProductivityAppAPI.Models;

public class Programs
{
   [Key]
   public Guid Id { get; set; }

   public string ProgramAbbreviation { get; set; }

   public string ProgramName { get; set; }

   public string Semester { get; set; }

   public string SchoolYear { get; set; }

   public string Department { get; set; }

   public virtual ICollection<SubSubject>? Subjects { get; set; }
}
