using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BayE.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BayE.Controllers
{
    public class ForumController : Controller
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

        public ForumController(BayEContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString(SessionTokenName) == null)
                TempData["Role"] = 0;
            else
                TempData["Role"] = HttpContext.Session.GetString(SessionUserRole);

            try
            {
                ViewBag.Forums = _context.Forum.Select(o => new Models.Forum { 
                    Id = o.Id, 
                    Name = o.Name, 
                    Description = o.Description, 
                    ViewCount = o.ViewCount,
                    FkUserId = o.FkUserId, 
                    Username = _context.User.Where(x => x.Id == o.FkUserId).Select(c => new { c.Username }).FirstOrDefault().Username, 
                    Total = _context.Topic.Count(g => g.FkForumId == o.Id) })
                    .ToList();

                return View();
            }
            catch
            {
                return View();
            }
        }

        public IActionResult CreateForum(Forum newForum)
        {
            if (HttpContext.Session.GetString(SessionTokenName) == null)
                return Redirect("/Users/Login");

            if (HttpContext.Session.GetString(SessionUserRole) != "1")
            {
                // unauthorized user

                return RedirectToAction("Index");
            }

            int uid = int.Parse(User.Identity.Name);
            // role = true if user is admin, false if not
            var role = _context.Admin.Where(o => o.FkUserId == uid).Any();

            if (role == false)
                return BadRequest(new { message = "Authorization to create new forum was not granted!" });

            newForum.FkUserId = uid;
            _context.Forum.Add(newForum);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult DeleteForum(int id)
        {
            if (HttpContext.Session.GetString(SessionTokenName) == null)
                return Redirect("/Users/Login");

            if (HttpContext.Session.GetString(SessionUserRole) != "1")
            {
                // unauthorized user

                return RedirectToAction("Index");
            }
            
            var uid = int.Parse(User.Identity.Name);
            // role = true if user is admin, false if not
            var role = _context.Admin.Where(o => o.FkUserId == uid).Any();
            var forum = _context.Forum.FirstOrDefault(o => o.Id == id);

            // check if user is the owner of the forum category
            if (uid != forum.FkUserId)
                if (role == false)
                    return BadRequest(new { message = "Authorization to delete requested forum was not granted!" });

            _context.Forum.Remove(forum);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult UpdateForum(Forum forum)
        {
            if (HttpContext.Session.GetString(SessionTokenName) == null)
                return Redirect("/Users/Login");

            if (HttpContext.Session.GetString(SessionUserRole) != "1")
            {
                // unauthorized user

                return RedirectToAction("Index");
            }

            var uid = int.Parse(User.Identity.Name);
            // role = true if user is admin, false if not
            var role = _context.Admin.Where(o => o.FkUserId == uid).Any();
            var forumToUpdate = _context.Forum.FirstOrDefault(o => o.FkUserId == uid);

            // check if user is the owner of the forum category
            if (uid != forumToUpdate.FkUserId)
                if (role == false)
                    return BadRequest(new { message = "Authorization to edit requested forum category was not granted!" });

            forumToUpdate.Name = forum.Name;
            forumToUpdate.Description = forum.Description;

            _context.Forum.Update(forumToUpdate);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [Route("Forum/{id}")]
        public IActionResult Discussion([FromRoute] int id)
        {
            // saves forum id or get's the last viewed forum id
            if (id == 0)
                id = HttpContext.Session.GetInt32("ForumID").GetValueOrDefault();
            else
                HttpContext.Session.SetInt32("ForumID", id);

            ViewBag.Topics = _context.Topic
                .Where(o => o.FkForumId == id)
                .Select(o => new Models.Topic { 
                    Id = o.Id,
                    Name = o.Name,
                    Description = o.Description,
                    Date = o.Date,
                    ViewCount = o.ViewCount,
                    FkForumId = o.FkForumId,
                    FkUserId = o.FkUserId,
                    Username = _context.User.Where(x => x.Id == o.FkUserId).Select(c => new { c.Username }).FirstOrDefault().Username,
                    CommentsCount = _context.Topiccomments.Count(x => x.FkTopicId == o.Id) })
                .ToList();

            return View();
        }

        public IActionResult CreateTopic(Topic newTopic)
        {
            if (HttpContext.Session.GetString(SessionTokenName) == null)
                return Redirect("/Users/Login");

            int uid = int.Parse(User.Identity.Name);
            newTopic.FkUserId = uid;
            newTopic.FkForumId = HttpContext.Session.GetInt32("ForumID").GetValueOrDefault();
            if (newTopic.FkForumId <= 0)
            {
                // return error message
            }
            _context.Topic.Add(newTopic);
            _context.SaveChanges();

            return Redirect("/Forum/0");
        }

        [Route("/admin/deletetopic/{id}")]
        public IActionResult AdminDeleteTopic([FromRoute] int id)
        {
            if (HttpContext.Session.GetString(SessionTokenName) == null)
                return Redirect("/Users/Login");

            // check if user is the owner of the topic
            var uid = int.Parse(User.Identity.Name);
            // role = true if user is admin, false if not
            var role = _context.Admin.Where(o => o.FkUserId == uid).Any();

            if (!role)
                return BadRequest(new { message = "Authorization to delete requested topic was not granted" });

            var topic = _context.Topic.FirstOrDefault(o => o.Id == id); 

            _context.Topic.Remove(topic);
            _context.SaveChanges();

            return Redirect("/Forum/0");
        }

        [Route("/admin/updatetopic/{id}")]
        public IActionResult AdminUpdateTopic([FromRoute] int id, Topic topic)
        {
            if (HttpContext.Session.GetString(SessionTokenName) == null)
                return Redirect("/Users/Login");

            var uid = int.Parse(User.Identity.Name);
            // role = true if user is admin, false if not
            var role = _context.Admin.Where(o => o.FkUserId == uid).Any();

            // check if admin is trying to update user's topic
            if (!role)
                return BadRequest(new { message = "Authorization to edit requested topic was not granted" });

            var updatedTopic = _context.Topic.FirstOrDefault(o => o.Id == id);
            updatedTopic.Name = topic.Name;
            updatedTopic.Description = topic.Description;

            _context.Topic.Update(updatedTopic);
             _context.SaveChanges();

            return Redirect("/Forum/0");
        }

        [Route("/Topic/{id}")]
        public IActionResult TopicInfo([FromRoute] int id)
        {
            ViewBag.Topic = _context.Topic.Where(o => o.Id == id)
                .Select(o => new Models.Topic {
                    Id = o.Id,
                    Name = o.Name,
                    Description = o.Description,
                    ViewCount = o.ViewCount,
                    FkForumId = o.FkForumId,
                    FkUserId = o.FkUserId,
                    Username = _context.User.Where(x => x.Id == o.FkUserId).Select(c => new { c.Username }).FirstOrDefault().Username
                })
                .FirstOrDefault();

            ViewBag.Comments = _context.Topiccomments
                .Select(o => new Models.Topiccomments
                {
                    Id = o.Id,
                    Text = o.Text,
                    Date = o.Date,
                    FkUserId = o.FkUserId,
                    FkTopicId = o.FkTopicId,
                    Username = o.FkUser.Username
                })
                .OrderByDescending(o => o.Date)
                .Where(o => o.FkTopicId == id)
                .ToList();

            return View();
        }

        [Route("/MyTopics")]
        public IActionResult MyTopics()
        {
            if (HttpContext.Session.GetString(SessionTokenName) == null)
                return Redirect("/Users/Login");

            var uid = int.Parse(User.Identity.Name);

            List<Topic> myTopics = _context.Topic
                .Where(o => o.FkUserId == uid)
                .Select(o => new Topic
                {
                    Id = o.Id,
                    Name = o.Name,
                    Description = o.Description,
                    ViewCount = o.ViewCount,
                    FkUserId = o.FkUserId,
                    FkForumId = o.FkForumId
                })
                .ToList();

            return View(myTopics);
        }

        [Route("/MyTopics/Delete/{id}")]
        public IActionResult DeleteMyTopic([FromRoute] int id)
        {
            if (HttpContext.Session.GetString(SessionUserRole) == null)
                return Redirect("/Users/Login");

            try
            {
                var uid = int.Parse(User.Identity.Name);

                var topic = _context.Topic.FirstOrDefault(o => o.Id == id);
                if (topic.FkUserId != uid)
                    return BadRequest(new { message = "Authorization to delete requested topic was not granted" });

                _context.Topic.Remove(topic);
                _context.SaveChanges();

                return Redirect("/MyTopics");
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return Redirect("/MyTopics");
            }
        }

        [Route("/MyTopics/Edit/{id}")]
        public IActionResult UpdateMyTopic([FromRoute] int id, Topic topic)
        {
            if (HttpContext.Session.GetString(SessionUserRole) == null)
                return Redirect("/Users/Login");

            try
            {
                var uid = int.Parse(User.Identity.Name);
                var oldTopic = _context.Topic.FirstOrDefault(o => o.Id == id);

                if (oldTopic.FkUserId != uid)
                    return BadRequest(new { message = "Authorization to edit requested topic was not granted" });

                oldTopic.Name = topic.Name;
                oldTopic.Description = topic.Description;

                _context.Topic.Update(oldTopic);
                _context.SaveChanges();

                return Redirect("/MyTopics");
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return Redirect("/MyTopics");
            }
        }

        [Route("/Topic/AddComment")]
        public IActionResult AddComment(Topiccomments newComment)
        {
            if (HttpContext.Session.GetString(SessionTokenName) == null)
                return Redirect("/Users/Login");

            var uid = int.Parse(User.Identity.Name);
            newComment.FkUserId = uid;

            try
            {
                _context.Topiccomments.Add(newComment);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return Redirect("/Topic/" + newComment.FkTopicId);
            }
        }

        [Route("/Topic/DeleteComment")]
        public IActionResult DeleteComment(int id, int fktopicid)
        {
            if (HttpContext.Session.GetString(SessionTokenName) == null)
                return Redirect("/Users/Login");

            var uid = int.Parse(User.Identity.Name);

            var topicComment = _context.Topiccomments.FirstOrDefault(o => o.Id == id && o.FkTopicId == fktopicid);
            if (uid != topicComment.FkUserId)
                return BadRequest(new { message = "Authorization to delete requested comment was not granted" });

            _context.Topiccomments.Remove(topicComment);
            _context.SaveChanges();
            return Ok();
        }

        [Route("/Topic/EditComment")]
        public IActionResult EditComment(int id, int fktopicid, string text)
        {
            if (HttpContext.Session.GetString(SessionTokenName) == null)
                return Redirect("/Users/Login");

            var uid = int.Parse(User.Identity.Name);
            var oldComment = _context.Topiccomments.FirstOrDefault(o => o.Id == id && o.FkTopicId == fktopicid);

            if (oldComment.FkUserId != uid)
                return BadRequest(new { message = "Authorization to edit requested comment was not granted" });

            oldComment.Text = text;

            try
            {
                _context.Topiccomments.Update(oldComment);
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return Redirect("/Topic/" + fktopicid);
            }
        }

        [Route("/ForumViewCounter/{id}")]
        public async Task<JsonResult> ForumViewCounter([FromRoute] int id)
        {
            try
            {
                var forum = _context.Forum.FindAsync(id);

                if (forum == null)
                    throw new Exception("Forum doesn't exist");

                forum.Result.ViewCount++;

                _context.Forum.Update(forum.Result);
                await _context.SaveChangesAsync();

                return Json(new { message = "Added" });
            }
            catch (Exception e)
            {
                return Json(new { message = e.Message });
            }
        }

        [Route("/TopicViewCounter/{id}")]
        public async Task<JsonResult> TopicViewCounter([FromRoute] int id)
        {
            try
            {
                var topic = _context.Topic.FindAsync(id);

                if (topic == null)
                    throw new Exception("Topic doesn't exist");

                topic.Result.ViewCount++;

                _context.Topic.Update(topic.Result);
                await _context.SaveChangesAsync();

                return Json(new { message = "Added" });
            }
            catch (Exception e)
            {
                return Json(new { message = e.Message });
            }
        }
    }
}