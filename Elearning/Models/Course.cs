using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Elearning.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "You should fill this field in!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "You should fill this field in!")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        [Display(Name = "Category")]
        public virtual Category Category { get; set; }

        public virtual ICollection<Material> Materials { get; set; }

        //public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<UserToCourse> UserToCourses { get; set; }
    }
}