using Elearning.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Elearning.Data
{
    public class ElearningDbContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public ElearningDbContext() : base("name=ElearningDbContext")
        {
        }

        public System.Data.Entity.DbSet<Elearning.Models.User> Users { get; set; }

        public System.Data.Entity.DbSet<Elearning.Models.Category> Categories { get; set; }

        public System.Data.Entity.DbSet<Elearning.Models.Course> Courses { get; set; }

        public System.Data.Entity.DbSet<Elearning.Models.Material> Materials { get; set; }

        public System.Data.Entity.DbSet<Elearning.Models.Post> Posts { get; set; }


        public System.Data.Entity.DbSet<Elearning.Models.UserToCourse> UserToCourses { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    //base.OnModelCreating(modelBuilder);
        //    modelBuilder.Entity<User>()
        //                .HasMany<Course>(s => s.Courses)
        //                .WithMany(c => c.Users)
        //                .Map(cs =>
        //                    {
        //                        cs.MapLeftKey("UserRefId");
        //                        cs.MapLeftKey("UserRefId");
        //                        cs.ToTable("UserCourse");
        //                    });
        //}
    }
}
