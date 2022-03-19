using System.ComponentModel.DataAnnotations;

namespace RiskyToDoApp.Models
{
    public class Task
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name ="Task Name")]
        public string Name { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }        
        public int Importance { get; set; }        
        public int Remove { get; set; }        
        public int Complete { get; set; }
    }
}
