using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Ispit.Todo.Models
{
    public class Todolist
    {


        [Key]

        public int Id { get; set; }
        public int TodoId { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 2)]
        public string TodoTitle { get; set; }
        [Required]
        [StringLength(1000, MinimumLength = 2)]
        public string Description { get; set; }
        [ForeignKey("fkId")]
        public ICollection<Task1> Tasks1 { get; set; }
    }
}
