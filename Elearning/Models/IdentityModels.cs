using System.Data.Entity;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Elearning.Models
{
    // В профиль пользователя можно добавить дополнительные данные, если указать больше свойств для класса ApplicationUser. Подробности см. на странице https://go.microsoft.com/fwlink/?LinkID=317594.
    public class ApplicationUser : IdentityUser
    {
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

        public virtual ICollection<Post> Posts { get; set; }

        public virtual ICollection<UserToCourse> UserToCourses { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Обратите внимание, что authenticationType должен совпадать с типом, определенным в CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Здесь добавьте утверждения пользователя
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public System.Data.Entity.DbSet<Elearning.Models.User> Users { get; set; }

        public System.Data.Entity.DbSet<Elearning.Models.Category> Categories { get; set; }

        public System.Data.Entity.DbSet<Elearning.Models.Course> Courses { get; set; }

        public System.Data.Entity.DbSet<Elearning.Models.Material> Materials { get; set; }

        public System.Data.Entity.DbSet<Elearning.Models.Post> Posts { get; set; }

        public System.Data.Entity.DbSet<Elearning.Models.UserToCourse> UserToCourses { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}