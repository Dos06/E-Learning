using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Elearning.Models
{
    public class Material
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "You should fill this field in!")]
        [Display(Name = "Course")]
        public int CourseId { get; set; }

        [ForeignKey("CourseId")]
        [Display(Name = "Course")]
        public virtual Course Course { get; set; }

        [Required(ErrorMessage = "You should fill this field in!")]
        [StringLength(1000, ErrorMessage = "Content is too short!")]
        public string Title { get; set; }

        [Display(Name = "Picture URL")]
        [DataType(DataType.ImageUrl)]
        public string PictureUrl { get; set; }

        [Required(ErrorMessage = "You should fill this field in!")]
        //[StringLength(1000, MinimumLength = 8, ErrorMessage = "Content is too short!")]
        public string Text { get; set; }

        [Range(0, 10, ErrorMessage = "You should enter a valid Rating [0-10]")]
        [Required(ErrorMessage = "You should fill this field in!")]
        public double Rating { get; set; }


        [Required(ErrorMessage = "You should fill this field in!")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //[Range(typeof(DateTime), "1/1/1900", "1/1/2021", ErrorMessage = "Wrong value!")]
        public DateTime Date { get; set; }
    }
}