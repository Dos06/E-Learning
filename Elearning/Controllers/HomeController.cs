using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using Elearning.Models;
using System.Web.Helpers;
using System.Threading;
using System.Globalization;

namespace Elearning.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Search()
        {
            return View();
        }

        public ActionResult ShowResults()
        {
            string searchQuery = Request["search"];
            string cx = "YOUR_CX";
            string apiKey = "YOUR_GOOGLE_API";
            var request = WebRequest.Create("https://www.googleapis.com/customsearch/v1?key=" + apiKey + "&cx=" + cx + "&q=" + searchQuery);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseString = reader.ReadToEnd();
            dynamic jsonData = JsonConvert.DeserializeObject(responseString);

            var results = new List<Result>();
            foreach (var item in jsonData.items)
            {
                results.Add(new Result
                {
                    Title = item.title,
                    Link = item.link,
                    Snippet = item.snippet,
                });
            }
            return View(results.ToList());
        }

        public ActionResult Chat()
        {
            return View();
        }

        //public ActionResult Index(string language)
        public ActionResult Index()
        {
            //Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language);
            //Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "The contact page.";
            return View();
        }

        [HttpPost]
        public ActionResult SendMail()
        {
            ViewBag.Message = "The contact page.";

            //string recipient = Request["to"];
            string recipient = "YOUR_EMAIL";
            string subject = Request["subject"];
            string body = Request["to"] + "<br/><br/>" + System.Environment.NewLine + Request["name"] + " has left a request: <br/>" + Environment.NewLine + Request["body"];

            WebMail.SmtpServer = "smtp.gmail.com";
            WebMail.SmtpPort = 587;

            WebMail.EnableSsl = true;

            WebMail.SmtpUseDefaultCredentials = true;
            WebMail.UserName = "YOUR_EMAIL";
            WebMail.Password = "YOUR_EMAIL_PASSWORD";

            WebMail.Send(to: recipient, subject: subject, body: body, isBodyHtml: true);

            return RedirectToAction("Contact");
        }
    }
}