using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BayE.Entities;
using BayE.Helpers;
using BayE.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BayE.Controllers
{
    public class UsersController : Controller
    {
        //Stores user's validation token's string
        public const string SessionTokenName = "_Token";
        //Stores user's username
        public const string SessionUsername = "_Username";
        //Stores user's role
        public const string SessionUserRole = "_UserRole";
        //Stores user's ID
        public const string SessionUserID = "_UserID";

        private readonly IUserService _userService;
        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;
        private readonly BayEContext  _context;

        public UsersController(IUserService userService, IOptions<AppSettings> appSettings, IMapper mapper, BayEContext context)
        {
            _userService = userService;
            _appSettings = appSettings.Value;
            _mapper = mapper;
            _context = context;
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            var cities = _context.City.Select(o => new City { Id = o.Id, Name = o.Name }).ToList();

            return View(cities);
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult MyAds()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [Route("users/create")]
        public IActionResult CreateUser(UserDto useris)
        {
            try
            {
                // map dto to entity
                var user = _mapper.Map<User>(useris);

                try
                {
                    // save 
                    _userService.Create(user, useris.Password);
                    return RedirectToAction(nameof(Login));
                }
                catch (Exception ex)
                {
                    // return error message if there was an exception
                    return BadRequest(new { message = ex.Message });
                }
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        [Route("users/authenticate")]
        public IActionResult Authenticate(UserDto userDto)
        {
            try
            {
                var user = _userService.Authenticate(userDto.Email, userDto.Password);

                if (user == null)
                    return BadRequest(new { message = "Email or password is incorrect" });

                // check if user is blocked
                if (_context.Blockeduser.Any(o => o.FkUserId == user.Id))
                    throw new Exception("Your account was blocked by "
                        + _context.User.FirstOrDefault(x => x.Id == _context.Admin.FirstOrDefault(o => o.FkUserId == _context.Blockeduser.FirstOrDefault(z => z.FkUserId == user.Id).FkAdminId).FkUserId).Username
                        + " on " + _context.Blockeduser.FirstOrDefault(o => o.FkUserId == user.Id).Date
                        + " . Reason - " + _context.Blockeduser.FirstOrDefault(o => o.FkUserId == user.Id).Reason);

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                // return basic user info (without password) and token to store client side
                HttpContext.Session.SetString(SessionTokenName, tokenString);
                HttpContext.Session.SetString(SessionUsername, user.Username);
                HttpContext.Session.SetString(SessionUserRole, _context.Admin.Count(o => o.FkUserId == user.Id).ToString());
                HttpContext.Session.SetString(SessionUserID, user.Id.ToString());

                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                TempData["Message"] = e.Message;
                return Redirect("/Users/Login");
            }
        }
    }
}