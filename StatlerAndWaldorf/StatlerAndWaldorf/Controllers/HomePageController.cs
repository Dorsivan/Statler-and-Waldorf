using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StatlerAndWaldorf.Controllers
{
    public class HomePageController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            HttpContext.Session.SetInt32("isSignedIn", 0); //1 signed in 0 not signed in
            HttpContext.Session.SetInt32("Role", 0); //0 guest, 1 user, 2 admin
            HttpContext.Session.SetInt32("CurrentUserId", 0);
            HttpContext.Session.SetInt32("CurrentMovieId", 0);
            HttpContext.Session.SetString("CurrentUsername", "");

            return View();
        }
    }
}
