using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using e_shop.Data;
using e_shop.Models;
using Microsoft.AspNetCore.Identity;

namespace e_shop.Controllers
{
    public class Rapport : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<IdentityUser> _userManager;

        public Rapport(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Rapport
        public async Task<IActionResult> Index()
        {
            List<Articles> listeArticles = new List<Articles>();

            var userRapport = await _userManager.FindByNameAsync(User.Identity.Name);

            listeArticles =  _context.Articles.ToList().FindAll(articles => articles.User == userRapport);

            return View(listeArticles);
        }

        public async Task<IActionResult> RapportConssolider()
        {
            List<Articles> listeArticles = new List<Articles>();
            var userRapport = await _userManager.FindByNameAsync(User.Identity.Name);
            listeArticles = _context.Articles.ToList().FindAll(articles => articles.User == userRapport);

            //LINQ pour agrouper l'information
            var rapportConsolide = listeArticles.GroupBy(article=> new { Mois = article.DateAchat.Value.Month, Annee = article.DateAchat.Value.Year })
                                    .Select(consolide=> new RapportConssolider {Mois = consolide.Key.Mois.ToString("00"), Annee = consolide.Key.Annee.ToString(), TotalPrice = consolide.Sum(p=>p.Price) }).ToList();            

            return View(rapportConsolide);

        }
    }
}
