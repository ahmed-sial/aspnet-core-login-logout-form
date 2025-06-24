using System.Diagnostics;
using LoginForm.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace LoginForm.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly LoginFormDbContext loginFormDbContext;

        public HomeController(ILogger<HomeController> logger, LoginFormDbContext loginFormDbContext)
        {
            _logger = logger;
            this.loginFormDbContext = loginFormDbContext;
        }

        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("email") != null && HttpContext.Session.GetString("password") != null)
            {
                return RedirectToAction("Dashboard");
            }
            return View();
        }
        [HttpPost]

        public IActionResult Login(UsersCredential usersCredential)
        {
            if (usersCredential == null || loginFormDbContext.UsersCredentials == null)
            {
                return NotFound();
            }
            var userData = loginFormDbContext.UsersCredentials
                .Where(x => x.Email == usersCredential.Email && x.Password == usersCredential.Password)
                .FirstOrDefault();
            if (userData != null)
            {
                // if the data of user is found then create a session and pass data to the session
                HttpContext.Session.SetString("email", userData.Email);
                HttpContext.Session.SetString("password", userData.Password);
                return RedirectToAction("Dashboard");
            } else
            {
                ViewBag.loginFailed = "Login Failed.";
            }
            return View();
        }
        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetString("email") != null && HttpContext.Session.GetString("password") != null) {
                ViewBag.email = HttpContext.Session.GetString("email");
                ViewBag.password = HttpContext.Session.GetString("password");
            } else
            {
                return RedirectToAction("Login");
            }
                return View();
        }
        public IActionResult Logout()
        {
            if (HttpContext.Session.GetString("email") != null && HttpContext.Session.GetString("password") != null) {
                HttpContext.Session.Remove("email");
                HttpContext.Session.Remove("password");
                return RedirectToAction("Login");
            }
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
