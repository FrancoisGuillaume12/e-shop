using e_shop.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using System.Text.Json;

namespace e_shop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            CookieOptions cookieOptions = new CookieOptions();
            cookieOptions.Secure = true;
            cookieOptions.Expires = DateTime.Now.AddMinutes(5);
            cookieOptions.HttpOnly = true;

            if (HttpContext.Request.Cookies["CookiesClient"] == null)
            {
                List<Articles> listeArticles = new List<Articles>();

                string jsonListe = JsonSerializer.Serialize(listeArticles);

                HttpContext.Response.Cookies.Append("CookiesClient", jsonListe, cookieOptions);
            }
            else
            {
                List<Articles> listeArticles = JsonSerializer.Deserialize<List<Articles>>(HttpContext.Request.Cookies["CookiesClient"]);
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

        public IActionResult About_us()
        {
            return View();
        }
    }
}
