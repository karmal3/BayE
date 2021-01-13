using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BayE.Models;
using Microsoft.AspNetCore.Http;
using BayE.Entities;

namespace BayE.Controllers
{
    public class HomeController : Controller
    {
        //Stores user's validation token's string
        public const string SessionTokenName = "_Token";
        //Stores user's username
        public const string SessionUsername = "_Username";
        //Stores user's role
        public const string SessionUserRole = "_UserRole";
        //Stores user's ID
        public const string SessionUserID = "_UserID";

        private readonly BayEContext _context;

        public HomeController(BayEContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString(SessionUserRole) != null)
            {
                TempData["Role"] = HttpContext.Session.GetString(SessionUserRole);
                TempData["Username"] = HttpContext.Session.GetString(SessionUsername);
            }
            else
            {
                TempData["Role"] = null;
                TempData["Username"] = null;
            }

            try
            {
                List<Entities.Ad> adsList = _context.Ad.Select(o => new Entities.Ad
                {
                    Id = o.Id,
                    Title = o.Title,
                    Description = o.Description,
                    Date = o.Date,
                    Price = o.Price,
                    FkUserId = o.FkUserId,
                    FkCategoryId = o.FkCategoryId,
                    FkSubcategoryId = o.FkSubcategoryId,
                    FkCategory = o.FkCategory,
                    FkSubcategory = o.FkSubcategory,
                    FkUser = o.FkUser,
                    Status = o.Status
                })
                .Where(o => o.Status == 1)
                .OrderByDescending(o => o.Date)
                .ToList();

                ViewBag.Auctions = _context.Auctionad.Select(o => new Auctionad
                {
                    Id = o.Id,
                    Name = o.Name,
                    FkUser = o.FkUser,
                    FkUserId = o.FkUserId,
                    Description = o.Description,
                    Length = o.Length,
                    Date = o.Date,
                    FkCategoryId = o.FkCategoryId,
                    FkCategory = o.FkCategory,
                    FkSubcategory = o.FkSubcategory,
                    FkSubcategoryId = o.FkSubcategoryId,
                    Status = o.Status,
                    Bid = o.Bid
                })
                .Where(o => o.Status == 1)
                .OrderByDescending(o => o.Date)
                .ToList();

                return View(adsList);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;

                return View(null);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
