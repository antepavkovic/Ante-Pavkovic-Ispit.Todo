using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ispit.Todo.Models
{
   
        public class AspNetUser
        {
        [Key]
    
        public int AspNetUserId { get; set; }
        public int AspNetId { get; set; }
        public bool Status { get; set; } = false;
        [Display(Name = "First name")]
            [Required]
            public string FirstName { get; set; }
            [Display(Name = "Last name")]
            [Required]
            public string LastName { get; set; }
            [Display(Name = "Address")]
            [Required]
            public string Address { get; set; }
            [Required]
            [ForeignKey("fkTodoId")]
            public virtual ICollection<Todolist> Todolists { get; set; }
        
    }
}
