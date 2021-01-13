using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BayE.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BayE.Controllers
{
    public class MessagesController : Controller
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

        public MessagesController(BayEContext context)
        {
            _context = context;
        }

        [Route("/MyMessages")]
        public IActionResult MyMessages()
        {
            if (HttpContext.Session.GetString(SessionTokenName) == null)
                return Redirect("/Users/Login");

            var id = int.Parse(User.Identity.Name);

            List<Models.PM> PMInbox = _context.Privatemessage
                .Where(o => o.FkReceiverId == id && o.ReceiverDeleted == 0)
                .Select(o => new Models.PM
                {
                    Id = o.Id,
                    Text = o.Text,
                    Date = o.Date,
                    FkSenderId = o.FkSenderId,
                    FkReceiverId = o.FkReceiverId,
                    SenderUsername = _context.User.Where(x => x.Id == o.FkSenderId).Select(c => new { c.Username }).FirstOrDefault().Username,
                    ReceiverUsername = _context.User.Where(x => x.Id == o.FkReceiverId).Select(c => new { c.Username }).FirstOrDefault().Username
                })
                .OrderByDescending(o => o.Date)
                .ToList();

            ViewBag.Sent = _context.Privatemessage
                .Where(o => o.FkSenderId == id && o.SenderDeleted == 0)
                .Select(o => new Models.PM
                {
                    Id = o.Id,
                    Text = o.Text,
                    Date = o.Date,
                    FkSenderId = o.FkSenderId,
                    FkReceiverId = o.FkReceiverId,
                    SenderUsername = _context.User.Where(x => x.Id == o.FkSenderId).Select(c => new { c.Username }).FirstOrDefault().Username,
                    ReceiverUsername = _context.User.Where(x => x.Id == o.FkReceiverId).Select(c => new { c.Username }).FirstOrDefault().Username
                })
                .OrderByDescending(o => o.Date)
                .ToList();

            ViewBag.InboxCount = _context.Privatemessage.Count(o => o.FkReceiverId == id && o.ReceiverDeleted == 0);
            ViewBag.SentCount = _context.Privatemessage.Count(o => o.FkSenderId == id && o.SenderDeleted == 0);

            return View(PMInbox);
        }

        [Route("/Messages/Send")]
        public IActionResult SendMessage(string username, string text)
        {
            if (HttpContext.Session.GetString(SessionTokenName) == null)
                return Redirect("/Users/Login");

            var id = int.Parse(User.Identity.Name);
            var receiverId = _context.User.FirstOrDefault(o => o.Username == username).Id;

            if (id <= 0 || receiverId <= 0)
            {
                // add error message
                return RedirectToAction("MyMessages");
            }

            try
            {
                Privatemessage newMessage = new Privatemessage();

                newMessage.Text = text;
                newMessage.FkReceiverId = receiverId;
                newMessage.FkSenderId = id;

                _context.Privatemessage.Add(newMessage);
                _context.SaveChanges();

                return RedirectToAction("MyMessages");
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return RedirectToAction("MyMessages");
            }
        }

        [Route("/Messages/DeleteInbox/{id}")]
        public IActionResult DeleteInboxMessage([FromRoute] int id)
        {
            if (HttpContext.Session.GetString(SessionTokenName) == null)
                return Redirect("/Users/Login");

            int uid = int.Parse(User.Identity.Name);

            var message = _context.Privatemessage.FirstOrDefault(o => o.Id == id);
            if (message.FkReceiverId != uid)
            {
                ViewBag.Error = "You bad boi";
                return Redirect("/MyMessages");
            }

            try
            {
                message.ReceiverDeleted = 1;

                _context.Privatemessage.Update(message);
                _context.SaveChanges();

                return Redirect("/MyMessages");
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return Redirect("/MyMessages");
            }
        }

        [Route("/Messages/DeleteSent/{id}")]
        public IActionResult DeleteSentMessage([FromRoute] int id)
        {
            if (HttpContext.Session.GetString(SessionTokenName) == null)
                return Redirect("/Users/Login");

            int uid = int.Parse(User.Identity.Name);

            var message = _context.Privatemessage.FirstOrDefault(o => o.Id == id);
            if (message.FkSenderId != uid)
            {
                ViewBag.Error = "You bad boi";
                return Redirect("/MyMessages");
            }

            try
            {
                message.SenderDeleted = 1;

                _context.Privatemessage.Update(message);
                _context.SaveChanges();

                return Redirect("/MyMessages");
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return Redirect("/MyMessages");
            }
        }

        [Route("/Messages/UnreadCount")]
        public JsonResult UnreadCount()
        {
            if (HttpContext.Session.GetString(SessionTokenName) != null)
                return Json(_context.Privatemessage.Count(o => o.FkReceiverId == int.Parse(User.Identity.Name) && o.ReceiverRead == 0 && o.ReceiverDeleted == 0));

            return Json("");
        }

        [Route("/Messages/MarkRead")]
        public async Task<JsonResult> MarkMessagesRead()
        {
            var messageList = _context.Privatemessage.Where(o => o.FkReceiverId == int.Parse(User.Identity.Name) && o.ReceiverRead == 0).ToList();

            foreach (var message in messageList)
            {
                message.ReceiverRead = 1;

                _context.Update(message);
            }

            await _context.SaveChangesAsync();

            return Json("");
        }
    }
}