using System.ComponentModel.DataAnnotations;

namespace Ispit.Todo.Models
{
    public class Task1
    {


        [Key]

        public int Id { get; set; }
        public int TaskId { get; set; }

        

        [Required]
        [Display(Name = "Title")]
        public string TaskTitle { get; set; }

    }
}
