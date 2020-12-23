using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Elearning.Models
{
    public class UserToCourse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CourseId { get; set; }

        public virtual User User { get; set; }
        public virtual Course Course { get; set; }
    }
}