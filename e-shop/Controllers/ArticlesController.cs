using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using e_shop.Data;
using e_shop.Models;
using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using System.Security.Policy;
using Microsoft.Identity.Client.Extensions.Msal;

namespace e_shop.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<IdentityUser> _userManager;

        public ArticlesController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            List<Articles> ListArticles = JsonSerializer.Deserialize<List<Articles>>(HttpContext.Request.Cookies["CookiesClient"]);
            return View(ListArticles);
            //await _context.Articles.ToListAsync()
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Articles article)
        {
            List<Articles> ListArticles;

            if (HttpContext.Request.Cookies["CookiesClient"] == null)
            {
                ListArticles = new List<Articles>();

                HttpContext.Response.Cookies.Append ("CookiesClient",JsonSerializer.Serialize<List<Articles>>(ListArticles));
            }

            ListArticles = JsonSerializer.Deserialize<List<Articles>>(HttpContext.Request.Cookies["CookiesClient"]);

            /*System.Collections.ArrayList listeDENimporteQuoi = new System.Collections.ArrayList();

            listeDENimporteQuoi.Add("dfhjdfkjdfkdfjdfkj");
            listeDENimporteQuoi.Add(12344545);
            listeDENimporteQuoi.Add(article);

            List <string> listeDeString = new List<string>();

            List<Articles> art = new List<Articles>();

            art.Add("Guillaume");

            art.Add(article);


            listeDeString.Add(1);
            */

            if (ListArticles != null)
            {
                ListArticles.Add(article);

                HttpContext.Response.Cookies.Delete("CookiesClient");
                HttpContext.Response.Cookies.Append("CookiesClient", JsonSerializer.Serialize<List<Articles>>(ListArticles));

                return Redirect("Index");
            }

            return View();
        }

        public ActionResult Payer() 
        {
            List<Articles> ListArticles = JsonSerializer.Deserialize<List<Articles>>(HttpContext.Request.Cookies["CookiesClient"]);

            return View(ListArticles);
        }

        public async Task<IActionResult> ConclurePaiement([Bind("Id,IdentityUser,DateAchat,Title,Price")] Articles articles)
        {

            List<Articles> ListArticles = JsonSerializer.Deserialize<List<Articles>>(HttpContext.Request.Cookies["CookiesClient"]);

            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            foreach (var article in ListArticles)
            {
                article.DateAchat = DateTime.Now;
                article.User = user;
                _context.Add(article);
                await _context.SaveChangesAsync();
            }
            
            ListArticles.Clear();
            HttpContext.Response.Cookies.Delete("CookiesClient");
            HttpContext.Response.Cookies.Append("CookiesClient", JsonSerializer.Serialize<List<Articles>>(ListArticles));
            return Redirect("Index");
        }
    }
}
