using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BayE.Entities;
using BayE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BayE.Controllers
{
    public class AdminController : Controller
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

        public AdminController(BayEContext context)
        {
            _context = context;
        }

        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetString(SessionTokenName) == null)
                return Redirect("/Users/Login");

            try
            {
                var uid = int.Parse(User.Identity.Name);

                if (!_context.Admin.Any(o => o.FkUserId == uid))
                    return Redirect("/Home");
            }
            catch (Exception e)
            {
                TempData["Message"] = e.Message;
                return Redirect("/Home");
            }

            ViewBag.usersCount = _context.User.Count();

            ViewBag.adsCount = _context.Ad.Count();

            List<Users> users = _context.User.Select(o => new Users
            {
                Id = o.Id,
                Username = o.Username,
                Email = o.Email,
                Date = o.Date,
                FirstName = o.FirstName,
                LastName = o.LastName,
                PhoneNumber = o.PhoneNumber,
                Address = o.Address,
                FkCityId = o.FkCityId,
                Role = _context.Admin.Any(z => z.FkUserId == o.Id)
            })
                .ToList();

            return View(users);
        }

        [Route("/Admin/User/{id}")]
        public IActionResult UserInfo([FromRoute] int id)
        {
            if (HttpContext.Session.GetString(SessionTokenName) == null)
                return Redirect("/Users/Login");

            try
            {
                var uid = int.Parse(User.Identity.Name);

                if (!_context.Admin.Any(o => o.FkUserId == uid))
                    return Redirect("/Home");
            }
            catch (Exception e)
            {
                TempData["Message"] = e.Message;
                return Redirect("/Home");
            }

            Users user = _context.User
                .Select(o => new Users
                {
                    Id = o.Id,
                    Username = o.Username,
                    Email = o.Email,
                    Date = o.Date,
                    FirstName = o.FirstName,
                    LastName = o.LastName,
                    PhoneNumber = o.PhoneNumber,
                    Address = o.Address,
                    FkCityId = o.FkCityId,
                    Role = _context.Admin.Any(z => z.FkUserId == o.Id),
                    Blocked = _context.Blockeduser.Any(z => z.FkUserId == o.Id)
                })
                .FirstOrDefault(o => o.Id == id);

            return View(user);
        }

        [Route("/BlockUser/{id}")]
        public IActionResult BlockUser([FromRoute] int id, string reason)
        {
            if (HttpContext.Session.GetString(SessionTokenName) == null)
                return Redirect("/Users/Login");

            var uid = int.Parse(User.Identity.Name);

            // check if admin is trying to block user
            if (!_context.Admin.Any(o => o.FkUserId == uid))
            {
                TempData["Message"] = "You need to be admin to block users.";
                return Redirect("/Admin/User/" + id);
            }

            // check if user is already blocked
            if (_context.Blockeduser.Any(o => o.FkUserId == id))
            {
                TempData["Message"] = "User is already blocked.";
                return Redirect("/Admin/User/" + id);
            }

            // check if user who's being blocked is not an admin
            if (_context.Admin.Any(o => o.FkUserId == id))
            {
                TempData["Message"] = "You cannot block admins.";
                return Redirect("/Admin/User/" + id);
            }

            try
            {
                var userToBlock = new Blockeduser();
                userToBlock.FkAdminId = uid;
                userToBlock.FkUserId = id;
                userToBlock.FkAdmin = _context.Admin.FirstOrDefault(o => o.FkUserId == uid);
                userToBlock.FkUser = _context.User.FirstOrDefault(o => o.Id == id);
                userToBlock.Reason = reason;

                _context.Blockeduser.Add(userToBlock);
                _context.SaveChanges();

                TempData["Message"] = "User was lul blocked.";

                return Redirect("/Admin/User/" + id);
            }
            catch (Exception e)
            {
                TempData["Message"] = e.Message;

                return Redirect("/Admin/User/" + id);
            }
        }

        [Route("/RemoveUser/{id}")]
        public IActionResult DeleteUser([FromRoute] int id)
        {
            if (HttpContext.Session.GetString(SessionTokenName) == null)
                return Redirect("/Users/Login");

            try
            {
                var uid = int.Parse(User.Identity.Name);

                // check if admin is trying to remove user
                if (!_context.Admin.Any(o => o.FkUserId == uid))
                {
                    TempData["Message"] = "You need to be admin to remove users.";
                    return Redirect("/Admin/User/" + id);
                }

                // check if user who's being blocked is not an admin
                if (_context.Admin.Any(o => o.FkUserId == id))
                {
                    TempData["Message"] = "You cannot remove admins.";
                    return Redirect("/Admin/User/" + id);
                }

                var userToDelete = _context.User.FirstOrDefault(o => o.Id == id);
                _context.User.Remove(userToDelete);
                _context.SaveChanges();

                return Redirect("/Admin/Dashboard");
            }
            catch (Exception e)
            {
                TempData["Message"] = e.Message;
                return Redirect("/Admin/User/" + id);
            }
        }
    }
}