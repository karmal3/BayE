using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BayE.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BayE.Controllers
{
    public class WishListController : Controller
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

        public WishListController(BayEContext context)
        {
            _context = context;
        }

        public IActionResult MyWishList()
        {
            return View();
        }

        [Route("/WishList/Add")]
        public IActionResult AddAdToWishList(int id)
        {
            if (HttpContext.Session.GetString(SessionTokenName) == null)
                return Redirect("/Users/Login");

            var message = "";

            try
            {
                var uid = int.Parse(User.Identity.Name);

                if (_context.Wishlist.Any(o => o.FkUserId == uid && o.FkAdId == id))
                {
                    message = "Ad already exist in your wishlist.";
                    return Json(new { message });
                }

                Wishlist newWishList = new Wishlist();
                newWishList.FkAdId = id;
                newWishList.FkUserId = uid;
                newWishList.FkAd = _context.Ad.FirstOrDefault(o => o.Id == id);
                newWishList.FkUser = _context.User.FirstOrDefault(o => o.Id == uid);

                _context.Wishlist.Add(newWishList);
                _context.SaveChanges();

                message = "Ad was added to the wishlist.";

                return Json(new { message });
            }
            catch (Exception e)
            {
                message = e.Message;
                return Json(new { message });
            }
        }
    }
}