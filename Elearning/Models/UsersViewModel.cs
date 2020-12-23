using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Elearning.Models
{
    public class UsersViewModel
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Tel { get; set; }
        public string PictureUrl { get; set; }
        public List<CheckBoxViewModel> Courses { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}