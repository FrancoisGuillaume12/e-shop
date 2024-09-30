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


    }
}
