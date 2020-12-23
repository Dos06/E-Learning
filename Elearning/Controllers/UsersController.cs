using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Elearning.Data;
using Elearning.Models;

namespace Elearning.Controllers
{
    [HandleError]
    public class UsersController : Controller
    {
        private ElearningDbContext db = new ElearningDbContext();


        public JsonResult GetEmailSearchValue(string search)
        {
            List<User> allSearch = db.Users.Where(x => x.Name.Contains(search)).Select(x => new User()
            {
                Id = x.Id,
                Email = x.Email
            }).ToList();
            return new JsonResult { Data = allSearch, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpGet]
        public JsonResult CheckEmail(string name)
        {
            var result = !(name == "Email");
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        // GET: Users
        public async Task<ActionResult> Index()
        {
            //ViewBag.Users = db.Users.ToListAsync();
            //return View(await db.Users.ToListAsync());
            var users = await db.Users.ToListAsync();
            ViewBag.Users = users;
            //return View(db.Users.ToList());
            return View();
        }
        //public ActionResult Index()
        //{
        //    var users = db.Users.ToList();
        //    ViewBag.Users = users;
        //    //return View(db.Users.ToList());
        //    return View();
        //}

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }



            var Results = from c in db.Courses
                          select new
                          {
                              c.Id,
                              c.Name,
                              Checked = ((from uc in db.UserToCourses
                                          where (uc.UserId == id) & (uc.CourseId == c.Id)
                                          select uc).Count() > 0)
                          };
            var MyViewModel = new UsersViewModel();

            MyViewModel.UserId = id.Value;
            MyViewModel.Name = user.Name;
            MyViewModel.Surname = user.Surname;
            MyViewModel.Email = user.Email;
            MyViewModel.Password = user.Password;
            MyViewModel.PasswordConfirm = user.PasswordConfirm;
            MyViewModel.Tel = user.Tel;
            MyViewModel.PictureUrl = user.PictureUrl;

            MyViewModel.Posts = db.Posts.Where(p => p.UserId == user.Id).ToList();

            var MyCheckBoxList = new List<CheckBoxViewModel>();

            foreach (var item in Results)
            {
                MyCheckBoxList.Add(new CheckBoxViewModel { Id = item.Id, Name = item.Name, Checked = item.Checked });
            }

            MyViewModel.Courses = MyCheckBoxList;

            return View(MyViewModel);
            //return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Email,Password,PasswordConfirm,Name,Surname,Tel,PictureUrl")] User user)
        //public ActionResult Create(User user)
        {
            //if (ModelState.IsValid)
            //{
                user.Role = "user";
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            //}

            //return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            var Results = from c in db.Courses
                          select new
                          {
                              c.Id,
                              c.Name,
                              Checked = ((from uc in db.UserToCourses
                                          where (uc.UserId == id) & (uc.CourseId == c.Id)
                                          select uc).Count() > 0)
                          };
            var MyViewModel = new UsersViewModel();

            MyViewModel.UserId = id.Value;
            MyViewModel.Name = user.Name;
            MyViewModel.Surname = user.Surname;
            MyViewModel.Email = user.Email;
            MyViewModel.Password= user.Password;
            MyViewModel.PasswordConfirm = user.PasswordConfirm;
            MyViewModel.Tel = user.Tel;
            MyViewModel.PictureUrl = user.PictureUrl;

            MyViewModel.Posts = db.Posts.Where(p => p.UserId == user.Id).ToList();

            var MyCheckBoxList = new List<CheckBoxViewModel>();

            foreach (var item in Results)
            {
                MyCheckBoxList.Add(new CheckBoxViewModel { Id = item.Id, Name = item.Name, Checked = item.Checked });
            }

            MyViewModel.Courses = MyCheckBoxList;

            return View(MyViewModel);
        }




        // GET: Users/Profile
        public ActionResult Profile()
        {
            if (Session["User"] != null)
            {
                User user = (User)Session["User"];
                var Results = from c in db.Courses
                              select new
                              {
                                  c.Id,
                                  c.Name,
                                  Checked = ((from uc in db.UserToCourses
                                              where (uc.UserId == user.Id) & (uc.CourseId == c.Id)
                                              select uc).Count() > 0)
                              };
                var MyViewModel = new UsersViewModel();

                MyViewModel.UserId = user.Id;
                MyViewModel.Name = user.Name;
                MyViewModel.Surname = user.Surname;
                MyViewModel.Email = user.Email;
                MyViewModel.Password = user.Password;
                MyViewModel.PasswordConfirm = user.PasswordConfirm;
                MyViewModel.Tel = user.Tel;
                MyViewModel.PictureUrl = user.PictureUrl;

                //MyViewModel.Posts = user.Posts;
                MyViewModel.Posts = db.Posts.Where(p => p.UserId == user.Id).ToList();

                var MyCheckBoxList = new List<CheckBoxViewModel>();

                foreach (var item in Results)
                {
                    MyCheckBoxList.Add(new CheckBoxViewModel { Id = item.Id, Name = item.Name, Checked = item.Checked });
                }

                MyViewModel.Courses = MyCheckBoxList;

                return View(MyViewModel);
                //return View();
            }
            return RedirectToAction("Login");
        }

        [HttpPost]
        public ActionResult UploadAvatar(HttpPostedFileBase UploadedImage, UsersViewModel user)
        {
            if (UploadedImage != null && UploadedImage.ContentLength > 0)
            {
                string ImageFileName = Path.GetFileName(UploadedImage.FileName);
                string FolderPath = Path.Combine(Server.MapPath("~/Avatars"), ImageFileName);
                UploadedImage.SaveAs(FolderPath);

                var MyUser = db.Users.Find(user.UserId);
                MyUser.PictureUrl = ImageFileName;
                db.SaveChanges();
                Session["User"] = MyUser;

                ViewBag.imagename = UploadedImage.FileName;
                ViewBag.Message = "Image File Uploaded Successfully!";
            }
            return RedirectToAction("Profile");
        }

        public ActionResult Login()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Login(User user)
        {
            //if (ModelState.IsValid)
            //{
            var MyUser = db.Users.Single(u => u.Email == user.Email && u.Password == user.Password);
                if (MyUser != null)
                {
                    Session["User"] = MyUser;
                    Session["UserRole"] = MyUser.Role;

                    HttpCookie cookie = Response.Cookies.Get("ASP.NET_SessionId");
                    cookie.Expires = DateTime.Now.AddHours(1);
                    this.ControllerContext.HttpContext.Response.Cookies.Set(cookie);
                    return RedirectToAction("Profile");
                }
                else
                {
                    ModelState.AddModelError("", "Email or Password is wrong!");
                }
            //}
            return View(user);
        }


        [HttpPost]
        public ActionResult LogOff()
        {
            Session["User"] = null;
            Session["UserRole"] = null;
            Response.Cookies.Remove("ASP.NET_SessionId");
            return RedirectToAction("Login");
        }



        public ActionResult EditProfile(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            var Results = from c in db.Courses
                          select new
                          {
                              c.Id,
                              c.Name,
                              Checked = ((from uc in db.UserToCourses
                                          where (uc.UserId == id) & (uc.CourseId == c.Id)
                                          select uc).Count() > 0)
                          };
            var MyViewModel = new UsersViewModel();

            MyViewModel.UserId = id.Value;
            MyViewModel.Name = user.Name;
            MyViewModel.Surname = user.Surname;
            MyViewModel.Email = user.Email;
            MyViewModel.Password = user.Password;
            MyViewModel.PasswordConfirm = user.PasswordConfirm;
            MyViewModel.Tel = user.Tel;
            MyViewModel.PictureUrl = user.PictureUrl;

            MyViewModel.Posts = db.Posts.Where(p => p.UserId == user.Id).ToList();

            var MyCheckBoxList = new List<CheckBoxViewModel>();

            foreach (var item in Results)
            {
                MyCheckBoxList.Add(new CheckBoxViewModel { Id = item.Id, Name = item.Name, Checked = item.Checked });
            }

            MyViewModel.Courses = MyCheckBoxList;

            return View(MyViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(UsersViewModel user)
        {
            if (ModelState.IsValid)
            {
                var MyUser = db.Users.Find(user.UserId);
                MyUser.Name = user.Name;
                MyUser.Surname = user.Surname;
                MyUser.Tel = user.Tel;
                MyUser.Email = user.Email;
                if (user.Password == user.PasswordConfirm)
                {
                    MyUser.Password = user.Password;
                    MyUser.PasswordConfirm = user.PasswordConfirm;
                }
                if (user.PictureUrl != null && user.PictureUrl != "")
                {
                    MyUser.PictureUrl = user.PictureUrl;
                }

                foreach (var item in db.UserToCourses)
                {
                    if (item.UserId == user.UserId)
                    {
                        db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                    }
                }

                foreach (var item in user.Courses)
                {
                    if (item.Checked)
                    {
                        db.UserToCourses.Add(new UserToCourse() { UserId = user.UserId, CourseId = item.Id });
                    }
                }

                //db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                User userSession = (User)Session["User"];
                if (userSession != null && userSession.Id == user.UserId)
                {
                    Session["User"] = db.Users.Find(user.UserId);
                }
                return RedirectToAction("Profile");
            }
            return View(user);
        }





        // POST: Users/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,Email,Password,PasswordConfirm,Name,Surname,Tel,PictureUrl")] User user)
        public ActionResult Edit(UsersViewModel user)
        {
            if (ModelState.IsValid)
            {
                var MyUser = db.Users.Find(user.UserId);
                MyUser.Name = user.Name;
                MyUser.Surname = user.Surname;
                MyUser.Tel = user.Tel;
                MyUser.Email = user.Email;
                if (user.Password == user.PasswordConfirm)
                {
                    MyUser.Password = user.Password;
                    MyUser.PasswordConfirm = user.PasswordConfirm;
                }
                if (user.PictureUrl != null && user.PictureUrl != "")
                {
                    MyUser.PictureUrl = user.PictureUrl;
                }

                foreach (var item in db.UserToCourses)
                {
                    if (item.UserId == user.UserId)
                    {
                        db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                    }
                }

                foreach (var item in user.Courses)
                {
                    if (item.Checked)
                    {
                        db.UserToCourses.Add(new UserToCourse() { UserId = user.UserId, CourseId = item.Id });
                    }
                }

                //db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();

                //User userSession = (User)Session["User"];
                //if (userSession != null && userSession.Id == user.UserId)
                //{
                //    Session["User"] = db.Users.Find(user.UserId);
                //}
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }


        // CUSTOM DELETE METHOD FOR MODEL DELETION
        public JsonResult DeleteUser(int id)
        {
            bool result = false;
            var user = db.Users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                db.Users.Remove(user);
                db.SaveChanges();
                result = true;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }




        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
