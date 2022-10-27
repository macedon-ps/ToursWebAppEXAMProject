using ToursWebAppEXAMProject.Repositories;

namespace ToursWebAppEXAMProject.Models
{
	public class TechTaskViewModel
	{
		public int Id { get; set; }

		public string PageName { get; set; } = null!;
		
		public bool? IsExecuteTechTask1 { get; set; } 

		public bool? IsExecuteTechTask2 { get; set; } 

		public bool? IsExecuteTechTask3 { get; set; } 

		public bool? IsExecuteTechTask4 { get; set; } 

		public bool? IsExecuteTechTask5 { get; set; } 

		public bool? IsExecuteTechTask6 { get; set; } 

		public double? ExecuteTechTasksProgress { get; set; } = 0.0;
	}
}
