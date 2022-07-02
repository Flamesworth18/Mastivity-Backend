using ProductivityAppAPI.Models.Sub_Models;

namespace ProductivityAppAPI.Models;

public class Subject
{
   [Key]
   public Guid Id { get; set; }

   public string SubjectCode { get; set; }

   public string SubjectName { get; set; }

   public string StartingTime { get; set; }

   public string EndingTime { get; set; }

   public string StartingTimeView { get; set; }

   public string EndingTimeView { get; set; }

   public string StartingDate { get; set; }

   public string EndingDate { get; set; }

   public List<Day>? Days { get; set; }

   public string DaysView { get; set; } = "None";

   public bool OnMonday { get; set; } = false;

   public bool OnTuesday { get; set; } = false;

   public bool OnWednesday { get; set; } = false;

   public bool OnThursday { get; set; } = false;

   public bool OnFriday { get; set; } = false;

   public string Department { get; set; }

   public List<SubStudent>? Students { get; set; }
}
