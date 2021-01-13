using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BayE.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BayE.Controllers
{
    public class CompetitionsController : Controller
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

        public CompetitionsController(BayEContext context)
        {
            _context = context;
        }

        [Route("/Competition")]
        public IActionResult Competitions()
        {
            if (HttpContext.Session.GetString(SessionTokenName) == null)
                return Redirect("/Users/Login");

            try
            {
                var competitions = _context.Competition
                    .Select(o => new Models.Competition
                    {
                        Id = o.Id,
                        Name = o.Name,
                        Description = o.Description,
                        Deadline = o.Deadline,
                        Date = o.Date,
                        FkAdmin = o.FkAdmin,
                        FkAdminId = o.FkAdminId,
                        Username = _context.User.FirstOrDefault(z => z.Id == o.FkAdmin.FkUserId).Username
                    })
                    .OrderByDescending(o => o.Date)
                    .ToList();

                return View(competitions);
            }
            catch (Exception e)
            {
                TempData["Message"] = e.Message;

                return Redirect("/Home");
            }
        }

        [Route("/Competition/{id}")]
        public IActionResult CompetitionInfo([FromRoute] int id)
        {
            if (HttpContext.Session.GetString(SessionTokenName) == null)
                return Redirect("/Users/Login");

            try
            {
                var uid = int.Parse(User.Identity.Name);

                var competitionInfo = _context.Competition
                    .Select(o => new Competition
                    {
                        Id = o.Id,
                        Name = o.Name,
                        Description = o.Description,
                        Deadline = o.Deadline,
                        Date = o.Date,
                        FkAdmin = o.FkAdmin,
                        FkAdminId = o.FkAdminId
                    })
                    .FirstOrDefault(o => o.Id == id);

                ViewBag.Participants = _context.Participant.Count(o => o.FkCompetitionId == id);

                ViewBag.isParticipating = _context.Participant.Any(o => o.FkCompetitionId == id && o.FkUserId == uid);

                return View(competitionInfo);
            }
            catch (Exception e)
            {
                TempData["Message"] = e.Message;

                return Redirect("/Competitions");
            }
        }

        [Route("/Competition/Create")]
        public IActionResult CreateCompetition(Competition newCompetition)
        {
            if (HttpContext.Session.GetString(SessionTokenName) == null)
                return Redirect("/Users/Login");

            if (HttpContext.Session.GetString(SessionUserRole) != "1")
            {
                // unauthorized user

                return Redirect("/Home");
            }

            try
            {
                var uid = int.Parse(User.Identity.Name);

                newCompetition.FkAdminId = uid;
                newCompetition.FkAdmin = _context.Admin.FirstOrDefault(o => o.FkUserId == uid);

                _context.Competition.Add(newCompetition);
                _context.SaveChanges();

                return Redirect("/Competition");
            }
            catch (Exception e)
            {
                TempData["Message"] = e.Message;

                return Redirect("/Competition");
            }
        }

        [Route("/Competition/Participate/{id}")]
        public async Task<JsonResult> Participate([FromRoute] int id)
        {
            if (HttpContext.Session.GetString(SessionTokenName) == null)
                return Json("Login");

            try
            {
                var uid = int.Parse(User.Identity.Name);

                if (_context.Participant.Any(o => o.FkUserId == uid && o.FkCompetitionId == id))
                    return Json("User is already in the competition");

                Participant newParticipant = new Participant();
                newParticipant.FkUser = await _context.User.FindAsync(uid);
                newParticipant.FkUserId = uid;
                newParticipant.FkCompetitionId = id;
                newParticipant.FkCompetition = await _context.Competition.FindAsync(id);

                await _context.Participant.AddAsync(newParticipant);
                await _context.SaveChangesAsync();

                return Json("Added");
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }
    }
}
