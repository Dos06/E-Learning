using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Elearning.Models
{
    [Table("Users")]
    //public class User : IValidatableObject
    public class User : IValidatableObject
    {

        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "You should fill this field in!")]
        [System.Web.Mvc.Remote("CheckEmail", "User", ErrorMessage = "Email is not valid.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "You should fill this field in!")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Password is too short!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "You should fill this field in!")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords should be the same!")]
        public string PasswordConfirm { get; set; }

        [Required(ErrorMessage = "You should fill this field in!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "You should fill this field in!")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "You should fill this field in!")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Telephone number")]
        [StringLength(12)]
        public string Tel { get; set; }

        [Display(Name = "Picture URL")]
        [DataType(DataType.ImageUrl)]
        public string PictureUrl { get; set; }

        public string Role { get; set; }

        public virtual ICollection<Post> Posts { get; set; }

        //public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<UserToCourse> UserToCourses { get; set; }



        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (string.IsNullOrWhiteSpace(this.Name))
                errors.Add(new ValidationResult("Не указано имя"));

            if (string.IsNullOrWhiteSpace(this.Surname))
                errors.Add(new ValidationResult("Не указанa фамилия"));

            if (string.IsNullOrWhiteSpace(this.Email))
                errors.Add(new ValidationResult("Не указан адрес электронной почты"));

            if (string.IsNullOrWhiteSpace(this.Password))
                errors.Add(new ValidationResult("Не указан пароль"));

            return errors;
        }
    }
}