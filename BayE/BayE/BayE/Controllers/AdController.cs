using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BayE.Entities;
using BayE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BayE.Controllers
{
    public class AdController : Controller
    {
        //Stores user's validation token's string
        public const string SessionTokenName = "_Token";
        //Stores user's username
        public const string SessionUsername = "_Username";
        //Stores user's role
        public const string SessionUserRole = "_UserRole";
        //Stores user's ID
        public const string SessionUserID = "_UserID";
        //Sores user's Cart
        public const string SessionUserCart = "_UserCart";

        private readonly BayEContext _context;

        public AdController(BayEContext context)
        {
            _context = context;
        }

        [Route("/Auction/{id}")]
        public IActionResult Auction([FromRoute] int id)
        {
            try
            {
                Auctionad auctionInfo = _context.Auctionad.Select(o => new Auctionad
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
                    Bid = o.Bid,
                    Status = o.Status
                })
                    .FirstOrDefault(o => o.Id == id);

                ViewBag.Bids = _context.Bid.Select(o => new Bid
                {
                    Id = o.Id,
                    Price = o.Price,
                    FkUser = o.FkUser,
                    FkUserId = o.FkUserId,
                    FkAuctionAd = o.FkAuctionAd,
                    FkAuctionAdId = o.FkAuctionAdId,
                    Date = o.Date
                })
                    .OrderByDescending(o => o.Price)
                    .ToList();

                var path = "wwwroot\\auctionAds\\" + id + "\\display";

                var files = Directory.GetFiles(path);
                string[] finalPictures = new string[files.Count()];

                for (int i = 0; i < files.Count(); i++)
                {
                    var finalPath = files[i].Substring(7, files[i].Length - 7);
                    finalPictures[i] = finalPath;
                }

                ViewBag.AdPictures = finalPictures;

                return View(auctionInfo);
            }
            catch (Exception e)
            {
                TempData["Message"] = e.Message;

                ViewBag.Bids = null;

                return View(null);
            }
        }

        [Route("/Payment")]
        public async Task<IActionResult> Payment(string cardHolder, string cardNumber, int csv, int expMM, int expYY)
        {
            var adsInCart = HttpContext.Session.GetObject<List<int>>(SessionUserCart);

            if (adsInCart == null || adsInCart.Count == 0)
                return Redirect("/Home");

            try
            {
                if (HttpContext.Session.GetString(SessionTokenName) == null)
                {
                    foreach (var id in adsInCart)
                    {
                        Entities.Ad ad = await _context.Ad.FindAsync(id);
                        Payment newPayment = new Payment();
                        newPayment.CardHolder = cardHolder;
                        newPayment.CardNumber = cardNumber;
                        newPayment.FkAd = ad;
                        newPayment.FkAdId = ad.Id;
                        newPayment.Price = ad.Price;

                        await _context.Payment.AddAsync(newPayment);
                        await _context.SaveChangesAsync();
                    }
                }
                else
                {
                    foreach (var id in adsInCart)
                    {
                        Entities.Ad ad = await _context.Ad.FindAsync(id);
                        Payment newPayment = new Payment();
                        newPayment.CardHolder = cardHolder;
                        newPayment.CardNumber = cardNumber;
                        newPayment.FkAd = ad;
                        newPayment.FkAdId = ad.Id;
                        newPayment.Price = ad.Price;
                        newPayment.FkUserId = int.Parse(User.Identity.Name);
                        newPayment.FkUser = await _context.User.FindAsync(int.Parse(User.Identity.Name));

                        await _context.Payment.AddAsync(newPayment);
                        await _context.SaveChangesAsync();
                    }
                }

                HttpContext.Session.SetObject(SessionUserCart, null);

                return Redirect("/Home");
            }
            catch (Exception e)
            {
                TempData["Message"] = e.Message;

                return Redirect("/Home");
            }
        }

        [Route("/AddToCart/{id}")]
        public IActionResult AddToCart([FromRoute] int id)
        {
            var message = "";

            List<int> cartList = new List<int>();
            if (HttpContext.Session.GetObject<List<int>>(SessionUserCart) != null)
            {
                var oldCartList = HttpContext.Session.GetObject<List<int>>(SessionUserCart);
                if (oldCartList.FirstOrDefault(o => o.Equals(id)) > 0)
                {
                    message = "Item already exists in the cart.";

                    return Json(new { message });
                }
                foreach (var cartItem in oldCartList) { cartList.Add(cartItem); }
                cartList.Add(id);
                HttpContext.Session.SetObject(SessionUserCart, cartList);
            }
            else
            {
                List<int> newCartList = new List<int>();
                newCartList.Add(id);
                HttpContext.Session.SetObject(SessionUserCart, newCartList);
            }

            message = "Item was added to the cart.";

            return Json(new { message });
        }

        [Route("/CartRemove/{id}")]
        public IActionResult RemoveFromCart([FromRoute] int id)
        {
            var adsInCart = HttpContext.Session.GetObject<List<int>>(SessionUserCart);

            if (adsInCart == null || adsInCart.Count == 0)
                return Redirect("/ShoppingCart");

            try
            {
                adsInCart.Remove(id);

                HttpContext.Session.SetObject(SessionUserCart, adsInCart);

                return Ok();
            }
            catch (Exception e)
            {
                TempData["Message"] = e.Message;

                return Redirect("/ShoppingCart");
            }
        }

        [Route("/ShoppingCart")]
        public async Task<IActionResult> ShoppingCart()
        {
            var adsInCart = HttpContext.Session.GetObject<List<int>>(SessionUserCart);

            if (adsInCart == null || adsInCart.Count == 0)
                return View(null);

            try
            {
                List<Entities.Ad> ads = new List<Entities.Ad>();

                foreach (var id in adsInCart)
                {
                    Entities.Ad ad = await _context.Ad.FindAsync(id);
                    ads.Add(ad);
                }

                return View(ads);
            }
            catch (Exception e)
            {
                TempData["Message"] = e.Message;

                return View(null);
            }
        }

        [Route("/CartTotal")]
        public async Task<JsonResult> CartTotal()
        {
            decimal total = 0;

            var adsInCart = HttpContext.Session.GetObject<List<int>>(SessionUserCart);

            if (adsInCart == null || adsInCart.Count == 0)
                return Json(total);

            try
            {
                List<Entities.Ad> ads = new List<Entities.Ad>();

                foreach (var id in adsInCart)
                {
                    Entities.Ad ad = await _context.Ad.FindAsync(id);
                    total += ad.Price;
                }

                return Json(total);
            }
            catch (Exception e)
            {
                var message = e.Message;

                return Json(message);
            }
        }

        [Route("/CartCount")]
        public JsonResult CartCount()
        {
            var total = 0;

            var adsInCart = HttpContext.Session.GetObject<List<int>>(SessionUserCart);

            if (adsInCart == null || adsInCart.Count == 0)
                return Json(total);
            else
                return Json(adsInCart.Count);
        }

        [Route("/Ad/{id}")]
        public IActionResult AdInfo([FromRoute] int id)
        {
            ViewBag.Comments = _context.Adcomments
                .Where(o => o.FkAdId == id)
                .Select(o => new Adcomments
                {
                    Id = o.Id,
                    Text = o.Text,
                    Date = o.Date,
                    FkUserId = o.FkUserId,
                    FkUser = o.FkUser,
                    FkAdId = o.FkAdId,
                    FkAd = o.FkAd
                })
                .ToList();

            Models.Ad adInfo = _context.Ad
                .Select(o => new Models.Ad
                {
                    Id = o.Id,
                    Title = o.Title,
                    Description = o.Description,
                    Date = o.Date,
                    Price = o.Price,
                    FkUserId = o.FkUserId,
                    FkCategoryId = o.FkCategoryId,
                    FkSubcategoryId = o.FkSubcategoryId,
                    Username = o.FkUser.Username,
                    Category = o.FkCategory.Name,
                    SubCategory = o.FkSubcategory.Name
                })
                .FirstOrDefault(o => o.Id == id);

            var path = "wwwroot\\ads\\" + id + "\\display";

            var files = Directory.GetFiles(path);
            string[] finalPictures = new string[files.Count()];

            for (int i = 0; i < files.Count(); i++)
            {
                var finalPath = files[i].Substring(12, files[i].Length - 12);
                finalPictures[i] = finalPath;
            }

            ViewBag.AdPictures = finalPictures;

            // recommendations

            // if user is not logged in
            if (HttpContext.Session.GetString(SessionTokenName) == null)
            {
                var viewedAdList = _context.Viewedad.OrderByDescending(o => o.Count).ToList();

                List<Entities.Ad> adsList = new List<Entities.Ad>();

                for (int i = 0; i < 5; i++)
                {
                    adsList.Add(_context.Ad.Find(viewedAdList[i].FkAdId));
                }

                ViewBag.Recommendations = adsList;
            }
            // if user is logged in but hasn't viewed a single ad
            else if (!_context.Viewedad.Any(o => o.FkUserId == int.Parse(User.Identity.Name)))
            {
                var viewedAdList = _context.Viewedad.OrderByDescending(o => o.Count).ToList();

                var user = _context.User.Find(int.Parse(User.Identity.Name));


            }
            // user if logged in and has at least one viewed ad
            else
            {
                var uid = int.Parse(User.Identity.Name);

                var categoriesCount = _context.Adcategory.Count();

                int[] categories = new int[categoriesCount + 1];
                for (int i = 0; i < categoriesCount; i++) { categories[i] = 0; }
                

                var userViewedAds = _context.Viewedad.OrderByDescending(o => o.Count).Where(o => o.FkUserId == uid).Select(o => new Viewedad 
                {
                    Id = o.Id,
                    Count = o.Count,
                    FkUser = o.FkUser,
                    FkUserId = o.FkUserId,
                    FkAd = o.FkAd,
                    FkAdId = o.FkAdId
                });

                foreach (var ad in userViewedAds)
                {
                    for (int i = 1; i <= categoriesCount; i++)
                    {
                        if (ad.FkAd.FkCategoryId == i)
                        {
                            categories[i] += ad.Count;
                            break;
                        }
                    }
                }

                var userBoughtAds = _context.Payment.Select(o => new Payment
                {
                    Id = o.Id,
                    FkAd = o.FkAd,
                    FkAdId = o.FkAdId,
                    FkUserId = o.FkUserId,
                    FkUser = o.FkUser
                })
                    .Where(o => o.FkUserId == uid)
                    .ToList();

                foreach (var ad in userBoughtAds)
                {
                    for (int i = 1; i <= categoriesCount; i++)
                    {
                        if (ad.FkAd.FkCategoryId == i)
                        {
                            categories[i]++;
                            break;
                        }
                    }
                }

                var userWishList = _context.Wishlist.Select(o => new Wishlist
                {
                    Id = o.Id,
                    FkUserId = o.FkUserId,
                    FkUser = o.FkUser,
                    FkAd = o.FkAd,
                    FkAdId = o.FkAdId
                })
                    .Where(o => o.FkUserId == uid)
                    .ToList();

                foreach (var ad in userWishList)
                {
                    for (int i = 1; i <= categoriesCount; i++)
                    {
                        if (ad.FkAd.FkCategoryId == i)
                        {
                            categories[i]++;
                            break;
                        }
                    }
                }

                var popularCategory = -1;
                for (int i = 1; i < categoriesCount; i++)
                {
                    if (categories[i] == categories.Max())
                    {
                        popularCategory = i;
                        break;
                    }
                }

                var subCategoriesCount = _context.Adsubcategory.Count(o => o.FkCategoryId == popularCategory);
                int[] subCategories = new int[subCategoriesCount];
                for (int i = 0; i < subCategoriesCount; i++) { subCategories[i] = 0; }

                foreach (var ad in userViewedAds)
                {
                    for (int i = 1; i <= subCategoriesCount; i++)
                    {
                        if (ad.FkAd.FkCategoryId == popularCategory && ad.FkAd.FkSubcategoryId == i)
                        {
                            subCategories[i] += ad.Count;
                            break;
                        }
                    }
                }

                foreach (var ad in userBoughtAds)
                {
                    for (int i = 1; i <= subCategoriesCount; i++)
                    {
                        if (ad.FkAd.FkCategoryId == popularCategory && ad.FkAd.FkSubcategoryId == i)
                        {
                            subCategories[i]++;
                            break;
                        }
                    }
                }

                foreach (var ad in userWishList)
                {
                    for (int i = 1; i <= subCategoriesCount; i++)
                    {
                        if (ad.FkAd.FkCategoryId == popularCategory && ad.FkAd.FkSubcategoryId == i)
                        {
                            subCategories[i]++;
                            break;
                        }
                    }
                }

                var popularSubCategory = -1;
                for (int i = 1; i <= subCategoriesCount; i++)
                {
                    if (subCategories[i] == subCategories.Max())
                    {
                        popularSubCategory = i;
                        break;
                    }
                }

                var adsList = _context.Ad.Where(o => o.FkCategoryId == popularCategory && o.FkSubcategoryId == popularSubCategory).OrderByDescending(o => o.Date).ToList();

                ViewBag.Recommendations = adsList;
            }

            return View(adInfo);
        }

        public IActionResult MyAds()
        {
            if (HttpContext.Session.GetString(SessionTokenName) == null)
                return Redirect("/Users/Login");

            try
            {
                var uid = int.Parse(User.Identity.Name);

                ViewBag.Categories = _context.Adcategory.Select(o => new Adcategory { Id = o.Id, Name = o.Name }).ToList();

                ViewBag.SubCategories = _context.Adsubcategory.Select(o => new Adsubcategory { Id = o.Id, Name = o.Name, FkCategoryId = o.FkCategoryId });

                var ads = _context.Ad.Select(o => new Entities.Ad
                {
                    Id = o.Id,
                    FkCategory = o.FkCategory,
                    FkSubcategoryId = o.FkSubcategoryId,
                    FkUserId = o.FkUserId,
                    Title = o.Title,
                    Description = o.Description,
                    Price = o.Price,
                    Date = o.Date,
                    Status = o.Status
                })
                    .OrderByDescending(o => o.Date)
                    .Where(o => o.FkUserId == uid)
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
                    .OrderByDescending(o => o.Date)
                    .Where(o => o.FkUserId == uid)
                    .ToList();

                return View(ads);
            }
            catch (Exception e)
            {
                TempData["Message"] = e.Message;

                return View();
            }
        }

        [Route("/GetCategories")]
        public JsonResult GetCategories()
        {
            return Json(_context.Adcategory.Select(o => new Adcategory { Id = o.Id, Name = o.Name }));
        }

        [Route("/GetSubCategories")]
        public JsonResult GetSubCategories()
        {
            return Json(_context.Adsubcategory.Select(o => new Adsubcategory { Id = o.Id, Name = o.Name, FkCategoryId = o.FkCategoryId }));
        }

        public IActionResult CreateAd(Entities.Ad skelbimas, List<IFormFile> images, bool isAuction, DateTime deadline)
        {
            //image/png
            //image/jpeg
            //image/jpg
            //image/gif

            if (HttpContext.Session.GetString(SessionTokenName) == null)
                return Redirect("/Users/Login");

            try
            {
                var uid = int.Parse(User.Identity.Name);
                var newAdID = -1;
                var path = "";

                if (isAuction)
                {
                    Auctionad newAuction = new Auctionad();
                    newAuction.Name = skelbimas.Title;
                    newAuction.Description = skelbimas.Description;
                    newAuction.Length = deadline;
                    newAuction.FkUserId = uid;
                    newAuction.FkUser = _context.User.Find(uid);
                    newAuction.FkCategoryId = skelbimas.FkCategoryId;
                    newAuction.FkCategory = _context.Adcategory.Find(skelbimas.FkCategoryId);
                    newAuction.FkSubcategoryId = skelbimas.FkSubcategoryId;
                    newAuction.FkSubcategory = _context.Adsubcategory.Find(skelbimas.FkSubcategoryId);
                    _context.Auctionad.Add(newAuction);
                    _context.SaveChanges();
                    newAdID = newAuction.Id;

                    path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\auctionAds\\" + newAdID);
                }
                else
                {
                    skelbimas.FkUserId = uid;
                    _context.Ad.Add(skelbimas);
                    _context.SaveChanges();
                    newAdID = skelbimas.Id;

                    path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\ads\\" + newAdID);
                }

                if (!System.IO.Directory.Exists(path + "\\index"))
                    Directory.CreateDirectory(path + "\\index");

                if (images[0] != null && images[0].Length > 0)
                {
                    if (images[0].ContentType == "image/jpg" || images[0].ContentType == "image/jpeg" || images[0].ContentType == "image/png")
                    {
                        var fileTypeCount = 0;
                        if (images[0].ContentType == "image/jpeg")
                            fileTypeCount = images[0].ContentType.Substring(images[0].ContentType.IndexOf("/"), images[0].ContentType.Length - images[0].ContentType.IndexOf("/")).Count() - 1;
                        else
                            fileTypeCount = images[0].ContentType.Substring(images[0].ContentType.IndexOf("/"), images[0].ContentType.Length - images[0].ContentType.IndexOf("/")).Count();
                        var fileType = images[0].FileName.Substring(images[0].FileName.Length - fileTypeCount, fileTypeCount);
                        // not using fileType
                        var finalpath = Path.Combine(path + "\\index", "indexPicture.png");

                        FileStream haaa = new FileStream(finalpath, FileMode.Create);

                        images[0].CopyTo(haaa);
                        haaa.Close();
                    }

                }

                if (!System.IO.Directory.Exists(path + "\\display"))
                    Directory.CreateDirectory(path + "\\display");

                foreach (var picture in images)
                {
                    if (picture != null && picture.Length > 0)
                    {
                        var fileTypeCount = 0;
                        if (picture.ContentType == "image/jpeg")
                            fileTypeCount = picture.ContentType.Substring(picture.ContentType.IndexOf("/"), picture.ContentType.Length - picture.ContentType.IndexOf("/")).Count() - 1;
                        else
                            fileTypeCount = picture.ContentType.Substring(picture.ContentType.IndexOf("/"), picture.ContentType.Length - picture.ContentType.IndexOf("/")).Count();
                        var fileType = picture.FileName.Substring(picture.FileName.Length - fileTypeCount, fileTypeCount);
                        var finalpath = Path.Combine(path + "\\display", picture.FileName);
                        //string fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\items", HttpContext.Session.GetString(SessionUsername) + fileType);

                        if (picture.ContentType == "image/jpg" || picture.ContentType == "image/jpeg" || picture.ContentType == "image/png")
                        {
                            string picturePathPNG = Path.Combine(path, ".png");
                            string picturePathJPEG = Path.Combine(path, ".jpeg");
                            string picturePathJPG = Path.Combine(path, ".jpg");

                            if (System.IO.File.Exists(picturePathPNG))
                                System.IO.File.Delete(picturePathPNG);
                            else if (System.IO.File.Exists(picturePathJPEG))
                                System.IO.File.Delete(picturePathJPEG);
                            else if (System.IO.File.Exists(picturePathJPG))
                                System.IO.File.Delete(picturePathJPG);

                            FileStream haaa = new FileStream(finalpath, FileMode.Create);

                            picture.CopyTo(haaa);
                            haaa.Close();
                        }
                    }
                }

                return RedirectToAction("MyAds");
            }
            catch (Exception e)
            {
                TempData["Message"] = e.Message;

                return RedirectToAction("MyAds");
            }
        }

        public IActionResult DeleteAd(int id)
        {
            if (HttpContext.Session.GetString(SessionTokenName) == null)
                return Redirect("/Users/Login");

            try
            {
                // check if user is the owner of the skelbimas
                var uid = int.Parse(User.Identity.Name);
                // role = true if user is admin, false if not
                var role = _context.Admin.Where(o => o.FkUserId == uid).Any();
                var skelbimas = _context.Ad.FirstOrDefault(o => o.Id == id);
                if (uid != skelbimas.FkUserId)
                    if (role == false)
                        return BadRequest(new { message = "Authorization to delete requested ad was not granted!" });

                _context.Ad.Remove(skelbimas);
                _context.SaveChanges();

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\ads\\" + id);

                System.IO.DirectoryInfo di = new DirectoryInfo(path);

                if (Directory.Exists(path))
                {
                    foreach (FileInfo file in di.GetFiles())
                    {
                        file.Delete();
                    }
                    foreach (DirectoryInfo dir in di.GetDirectories())
                    {
                        dir.Delete(true);
                    }
                    Directory.Delete(path);
                }

                return RedirectToAction("MyAds");
            }
            catch (Exception e)
            {
                TempData["Message"] = e.Message;

                return RedirectToAction("MyAds");
            }
        }

        [Route("/Auction/Delete/{id}")]
        public async Task<IActionResult> DeleteAuctionAd([FromRoute] int id)
        {
            if (HttpContext.Session.GetString(SessionTokenName) == null)
                return Redirect("/Users/Login");

            try
            {
                // check if user is the owner of the skelbimas
                var uid = int.Parse(User.Identity.Name);
                var auctionAd = _context.Auctionad.FindAsync(id).Result;
                if (uid != auctionAd.FkUserId)
                    if (HttpContext.Session.GetString(SessionUserRole) != "1")
                        return BadRequest(new { message = "Authorization to delete requested auction ad was not granted!" });

                _context.Auctionad.Remove(auctionAd);
                await _context.SaveChangesAsync();

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\auctionAds\\" + id);

                System.IO.DirectoryInfo di = new DirectoryInfo(path);

                if (Directory.Exists(path))
                {
                    foreach (FileInfo file in di.GetFiles())
                    {
                        file.Delete();
                    }
                    foreach (DirectoryInfo dir in di.GetDirectories())
                    {
                        dir.Delete(true);
                    }
                    Directory.Delete(path);
                }

                return RedirectToAction("MyAds");
            }
            catch (Exception e)
            {
                TempData["Message"] = e.Message;

                return RedirectToAction("MyAds");
            }
        }

        [Route("/ad/updatead/{id}")]
        public IActionResult UpdateAd(Entities.Ad skelbimas, List<IFormFile> images, IFormFile indexPicture)
        {
            if (HttpContext.Session.GetString(SessionTokenName) == null)
                return Redirect("/Users/Login");

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\ads\\" + skelbimas.Id);

            foreach (var picture in images)
            {
                if (picture != null && picture.Length > 0)
                {
                    var fileTypeCount = 0;
                    if (picture.ContentType == "image/jpeg")
                        fileTypeCount = picture.ContentType.Substring(picture.ContentType.IndexOf("/"), picture.ContentType.Length - picture.ContentType.IndexOf("/")).Count() - 1;
                    else
                        fileTypeCount = picture.ContentType.Substring(picture.ContentType.IndexOf("/"), picture.ContentType.Length - picture.ContentType.IndexOf("/")).Count();
                    var fileType = picture.FileName.Substring(picture.FileName.Length - fileTypeCount, fileTypeCount);
                    var finalpath = Path.Combine(path + "\\display", picture.FileName);
                    //string fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\items", HttpContext.Session.GetString(SessionUsername) + fileType);
                    try
                    {
                        if (picture.ContentType == "image/jpg" || picture.ContentType == "image/jpeg" || picture.ContentType == "image/png")
                        {
                            if (System.IO.File.Exists(finalpath))
                                System.IO.File.Delete(finalpath);

                            FileStream haaa = new FileStream(finalpath, FileMode.Create);

                            picture.CopyTo(haaa);
                            haaa.Close();
                        }
                    }
                    catch
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }

            try
            {
                if (indexPicture != null && indexPicture.Length > 0)
                {
                    if (indexPicture.ContentType == "image/jpg" || indexPicture.ContentType == "image/jpeg" || indexPicture.ContentType == "image/png")
                    {
                        var fileTypeCount = 0;
                        if (indexPicture.ContentType == "image/jpeg")
                            fileTypeCount = indexPicture.ContentType.Substring(indexPicture.ContentType.IndexOf("/"), indexPicture.ContentType.Length - indexPicture.ContentType.IndexOf("/")).Count() - 1;
                        else
                            fileTypeCount = indexPicture.ContentType.Substring(indexPicture.ContentType.IndexOf("/"), indexPicture.ContentType.Length - indexPicture.ContentType.IndexOf("/")).Count();
                        var fileType = indexPicture.FileName.Substring(indexPicture.FileName.Length - fileTypeCount, fileTypeCount);
                        var finalpath = Path.Combine(path + "\\index", "indexPicture" + fileType);

                        FileStream haaa = new FileStream(finalpath, FileMode.Create);

                        indexPicture.CopyTo(haaa);
                        haaa.Close();
                    }
                }
                var uid = int.Parse(User.Identity.Name);
                var oldAd = _context.Ad.FirstOrDefault(o => o.Id == skelbimas.Id);

                if (oldAd.FkUserId != uid)
                    return BadRequest(new { message = "Authorization to edit requested ad was not granted!" });

                oldAd.Price = skelbimas.Price;
                oldAd.Title = skelbimas.Title;
                oldAd.Description = skelbimas.Description;
                oldAd.FkCategoryId = skelbimas.FkCategoryId;
                oldAd.FkSubcategoryId = skelbimas.FkSubcategoryId;

                _context.Ad.Update(oldAd);
                _context.SaveChanges();

                return RedirectToAction("MyAds");
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;

                return RedirectToAction("MyAds");
            }
        }

        [Route("/Viewed/{id}")]
        public async Task<JsonResult> ViewedAd([FromRoute] int id)
        {
            try
            {
                if (HttpContext.Session.GetString(SessionUserRole) != null)
                {
                    var uid = int.Parse(User.Identity.Name);

                    if (_context.Ad.FindAsync(id).Result.FkUserId == uid)
                        return Json(new { message = "" });

                    var exists = _context.Viewedad.Any(o => o.FkAdId == id && o.FkUserId == uid);

                    if (exists)
                    {
                        var viewedAd = _context.Viewedad.FirstOrDefault(o => o.FkAdId == id);
                        viewedAd.Count++;

                        _context.Viewedad.Update(viewedAd);
                        await _context.SaveChangesAsync();

                        return Json(new { message = "" });
                    }
                    else
                    {
                        Viewedad newAD = new Viewedad();
                        newAD.FkAdId = id;
                        newAD.FkUserId = uid;
                        newAD.FkAd = await _context.Ad.FindAsync(id);
                        newAD.FkUser = await _context.User.FindAsync(uid);

                        _context.Viewedad.Add(newAD);
                        await _context.SaveChangesAsync();

                        return Json(new { message = "" });
                    }
                }
                else
                {
                    return Json(new { message = "" });
                }
            }
            catch (Exception e)
            {
                return Json(new { message = e.Message });
            }
        }

        [Route("/ManualBid/{id}")]
        public async Task<JsonResult> ManualBid([FromRoute] int id, decimal value)
        {
            if (HttpContext.Session.GetString(SessionTokenName) == null)
            {
                TempData["Message"] = "You need to be logged in to bid.";
                return Json("test");
            }
            
            if (value <= 0 || value <= _context.Bid.Max(o => o.Price))
            {
                TempData["Message"] = "You can't bid lower or equal than the biggest bid.";
                return Json("test");
            }

            try
            {
                var uid = int.Parse(User.Identity.Name);

                Bid newBid = new Bid();
                newBid.Price = value;
                newBid.FkAuctionAdId = id;
                newBid.FkAuctionAd = await _context.Auctionad.FindAsync(id);
                newBid.FkUser = await _context.User.FindAsync(uid);
                newBid.FkUserId = uid;

                var autoBidList = _context.Autobid.Where(o => o.FkAuctionAdId == id).ToList();

                if (autoBidList.Count != 0)
                {
                    decimal highestMaxBid = autoBidList.Max(o => o.MaxPrice);

                    if (value < highestMaxBid)
                    {
                        Bid bid = new Bid();
                        bid.FkAuctionAd = await _context.Auctionad.FindAsync(id);
                        bid.FkAuctionAdId = id;
                        bid.FkUser = autoBidList.FirstOrDefault(o => o.MaxPrice == highestMaxBid).FkUser;
                        bid.FkUserId = autoBidList.FirstOrDefault(o => o.MaxPrice == highestMaxBid).FkUserId;
                        bid.Price = value + (decimal)0.1;

                        _context.Bid.Add(bid);
                        _context.Bid.Add(newBid);

                        // send message
                        var message = $"You lost the first place on <a href='/Auction/{id}'>{_context.Auctionad.Find(id).Name}</a>";

                        Privatemessage newMessage = new Privatemessage();
                        // system account for managing auto generated messages
                        newMessage.FkSenderId = 7;
                        newMessage.FkSender = await _context.User.FindAsync(7);
                        newMessage.FkReceiverId = uid;
                        newMessage.FkReceiver = await _context.User.FindAsync(uid);
                        newMessage.Text = message;

                        _context.Privatemessage.Add(newMessage);
                    }
                    else if (value > highestMaxBid)
                    {
                        _context.Bid.Add(newBid);
                        // send every auto bidder a message
                        foreach (var autoBid in autoBidList)
                        {
                            var message = $"Your auto bid was surpassed on <a href='/Auction/{id}'>{_context.Auctionad.Find(id).Name}</a>";

                            Privatemessage newMessage = new Privatemessage();
                            // system account for managing auto generated messages
                            newMessage.FkSenderId = 7;
                            newMessage.FkSender = await _context.User.FindAsync(7);
                            newMessage.FkReceiverId = autoBid.FkUserId;
                            newMessage.FkReceiver = autoBid.FkUser;
                            newMessage.Text = message;

                            _context.Privatemessage.Add(newMessage);
                        }
                    }
                    else
                    {
                        Bid bid = new Bid();
                        bid.FkAuctionAd = await _context.Auctionad.FindAsync(id);
                        bid.FkAuctionAdId = id;
                        bid.FkUser = autoBidList.FirstOrDefault(o => o.MaxPrice == highestMaxBid).FkUser;
                        bid.FkUserId = autoBidList.FirstOrDefault(o => o.MaxPrice == highestMaxBid).FkUserId;
                        bid.Price = autoBidList.FirstOrDefault(o => o.MaxPrice == highestMaxBid).MaxPrice;

                        _context.Bid.Add(bid);
                    }
                }

                await _context.SaveChangesAsync();

                return Json("test");
            }
            catch (Exception e)
            {
                TempData["Message"] = e.Message;

                //return Redirect("/Auction/" + id);
                return Json("test");
            }
        }

        [Route("/AutoBid/{id}")]
        public async Task<JsonResult> AutoBid([FromRoute] int id, decimal maxValue)
        {
            if (HttpContext.Session.GetString(SessionTokenName) == null)
            {
                TempData["Message"] = "You need to be logged in to bid.";
                return Json("test");
            }

            if (maxValue <= 0 || maxValue <= _context.Bid.Max(o => o.Price))
            {
                TempData["Message"] = "You can't bid lower or equal than the biggest bid.";
                return Json("test");
            }

            try
            {
                var uid = int.Parse(User.Identity.Name);

                var oldAutoBid = _context.Autobid.FirstOrDefault(o => o.FkAuctionAdId == id && o.FkUserId == uid);

                if (oldAutoBid != null && oldAutoBid.MaxPrice >= maxValue)
                {
                    TempData["Message"] = "You can't auto bid with the same or lower max amount";
                    return Json("You can't auto bid with the same or lower max amount");
                }
                else if (oldAutoBid != null && oldAutoBid.MaxPrice < maxValue)
                {
                    oldAutoBid.MaxPrice = maxValue;
                    oldAutoBid.Date = DateTime.Now;

                    _context.Autobid.Update(oldAutoBid);
                    await _context.SaveChangesAsync();

                    TempData["Message"] = "Auto bid updated.";

                    return Json("Auto bid updated.");
                }

                var autoBidList = _context.Autobid.Where(o => o.FkAuctionAdId == id).ToList();

                if (autoBidList.Count == 0)
                {
                    Bid bid = new Bid();
                    bid.FkAuctionAd = await _context.Auctionad.FindAsync(id);
                    bid.FkAuctionAdId = id;
                    bid.FkUser = await _context.User.FindAsync(uid);
                    bid.FkUserId = uid;
                    bid.Price = _context.Bid.Max(o => o.Price) + (decimal)0.1;

                    _context.Bid.Add(bid);

                    Autobid newAutoBid = new Autobid();
                    newAutoBid.MaxPrice = maxValue;
                    newAutoBid.FkUserId = uid;
                    newAutoBid.FkUser = await _context.User.FindAsync(uid);
                    newAutoBid.FkAuctionAdId = id;
                    newAutoBid.FkAuctionAd = await _context.Auctionad.FindAsync(id);

                    _context.Autobid.Add(newAutoBid);
                    await _context.SaveChangesAsync();

                    return Json("Test");
                }

                decimal highestMaxBid = autoBidList.Max(o => o.MaxPrice);

                foreach (var autoBid in autoBidList)
                {
                    if (autoBid.MaxPrice > highestMaxBid)
                        highestMaxBid = autoBid.MaxPrice;
                }

                if (highestMaxBid > maxValue)
                {
                    Bid bid = new Bid();
                    bid.FkAuctionAd = autoBidList.FirstOrDefault(o => o.MaxPrice == highestMaxBid).FkAuctionAd;
                    bid.FkAuctionAdId = id;
                    bid.FkUser = await _context.User.FindAsync(uid);
                    bid.FkUserId = uid;
                    bid.Price = maxValue;

                    _context.Bid.Add(bid);
                    // send message that auto bid lost to the higher bidder
                    var message = $"Your auto bid was surpassed on <a href='/Auction/{id}'>{_context.Auctionad.Find(id).Name}</a>";

                    Privatemessage newMessage = new Privatemessage();
                    // system account for managing auto generated messages
                    newMessage.FkSenderId = 7;
                    newMessage.FkSender = await _context.User.FindAsync(7);
                    newMessage.FkReceiverId = uid;
                    newMessage.FkReceiver = await _context.User.FindAsync(uid);
                    newMessage.Text = message;

                    _context.Privatemessage.Add(newMessage);

                    Bid bid2 = new Bid();
                    bid2.FkAuctionAd = autoBidList.FirstOrDefault(o => o.MaxPrice == highestMaxBid).FkAuctionAd;
                    bid2.FkAuctionAdId = id;
                    bid2.FkUser = autoBidList.FirstOrDefault(o => o.MaxPrice == highestMaxBid).FkUser;
                    bid2.FkUserId = autoBidList.FirstOrDefault(o => o.MaxPrice == highestMaxBid).FkUserId;
                    bid2.Price = maxValue + (decimal)0.1;

                    _context.Bid.Add(bid2);

                    // remove all auto bids that lost
                    foreach (var autoBid in autoBidList)
                    {
                        if (autoBid.MaxPrice != highestMaxBid)
                        {
                            // send message to auto bidders that they were surprassed
                            message = $"Your auto bid was surpassed on <a href='/Auction/{id}'>{_context.Auctionad.Find(id).Name}</a>";

                            newMessage = new Privatemessage();
                            // system account for managing auto generated messages
                            newMessage.FkSenderId = 7;
                            newMessage.FkSender = await _context.User.FindAsync(7);
                            newMessage.FkReceiverId = autoBid.FkUserId;
                            newMessage.FkReceiver = autoBid.FkUser;
                            newMessage.Text = message;

                            _context.Privatemessage.Add(newMessage);

                            _context.Autobid.Remove(autoBid);
                        }
                    }
                }
                else if (highestMaxBid < maxValue)
                {
                    Bid bid = new Bid();
                    bid.FkAuctionAd = autoBidList.FirstOrDefault(o => o.MaxPrice == highestMaxBid).FkAuctionAd;
                    bid.FkAuctionAdId = id;
                    bid.FkUser = autoBidList.FirstOrDefault(o => o.MaxPrice == highestMaxBid).FkUser;
                    bid.FkUserId = autoBidList.FirstOrDefault(o => o.MaxPrice == highestMaxBid).FkUserId;
                    bid.Price = autoBidList.FirstOrDefault(o => o.MaxPrice == highestMaxBid).MaxPrice;

                    _context.Bid.Add(bid);

                    Bid bid2 = new Bid();
                    bid2.FkAuctionAd = autoBidList.FirstOrDefault(o => o.MaxPrice == highestMaxBid).FkAuctionAd;
                    bid2.FkAuctionAdId = id;
                    bid2.FkUser = await _context.User.FindAsync(uid);
                    bid2.FkUserId = uid;
                    bid2.Price = autoBidList.FirstOrDefault(o => o.MaxPrice == highestMaxBid).MaxPrice + (decimal)0.1;

                    _context.Bid.Add(bid2);

                    Autobid newAutoBid = new Autobid();
                    newAutoBid.MaxPrice = maxValue;
                    newAutoBid.FkUserId = uid;
                    newAutoBid.FkUser = await _context.User.FindAsync(uid);
                    newAutoBid.FkAuctionAdId = id;
                    newAutoBid.FkAuctionAd = autoBidList.FirstOrDefault(o => o.MaxPrice == highestMaxBid).FkAuctionAd;

                    highestMaxBid = maxValue;

                    // remove all auto bids that lost
                    foreach (var autoBid in autoBidList)
                    {
                        if (autoBid.MaxPrice != highestMaxBid)
                        {
                            // send message to all auto bidders that they were surpassed
                            var message = $"Your auto bid was surpassed on <a href='/Auction/{id}'>{_context.Auctionad.Find(id).Name}</a>";

                            Privatemessage newMessage = new Privatemessage();
                            // system account for managing auto generated messages
                            newMessage.FkSenderId = 7;
                            newMessage.FkSender = await _context.User.FindAsync(7);
                            newMessage.FkReceiverId = autoBid.FkUserId;
                            newMessage.FkReceiver = autoBid.FkUser;
                            newMessage.Text = message;

                            _context.Privatemessage.Add(newMessage);

                            _context.Autobid.Remove(autoBid);
                        }
                    }

                    _context.Autobid.Add(newAutoBid);
                }
                else
                {
                    // send message that auto bid lost to the same max price bidder but he was first to chose that ammount
                    var message = $"Your auto bid was surpassed on <a href='/Auction/{id}'>{_context.Auctionad.Find(id).Name}</a>";

                    Privatemessage newMessage = new Privatemessage();
                    // system account for managing auto generated messages
                    newMessage.FkSenderId = 7;
                    newMessage.FkSender = await _context.User.FindAsync(7);
                    newMessage.FkReceiverId = uid;
                    newMessage.FkReceiver = await _context.User.FindAsync(uid);
                    newMessage.Text = message;

                    _context.Privatemessage.Add(newMessage);

                    Bid bid = new Bid();
                    bid.FkAuctionAd = autoBidList.FirstOrDefault(o => o.MaxPrice == highestMaxBid).FkAuctionAd;
                    bid.FkAuctionAdId = id;
                    bid.FkUser = autoBidList.FirstOrDefault(o => o.MaxPrice == highestMaxBid).FkUser;
                    bid.FkUserId = autoBidList.FirstOrDefault(o => o.MaxPrice == highestMaxBid).FkUserId;
                    bid.Price = autoBidList.FirstOrDefault(o => o.MaxPrice == highestMaxBid).MaxPrice;

                    // remove all auto bids that lost
                    foreach (var autoBid in autoBidList)
                    {
                        if (autoBid.MaxPrice != highestMaxBid)
                        {
                            // send message to all auto bidders that they were surpassed
                            message = $"Your auto bid was surpassed on <a href='/Auction/{id}'>{_context.Auctionad.Find(id).Name}</a>";

                            newMessage = new Privatemessage();
                            // system account for managing auto generated messages
                            newMessage.FkSenderId = 7;
                            newMessage.FkSender = await _context.User.FindAsync(7);
                            newMessage.FkReceiverId = autoBid.FkUserId;
                            newMessage.FkReceiver = autoBid.FkUser;
                            newMessage.Text = message;

                            _context.Privatemessage.Add(newMessage);

                            _context.Autobid.Remove(autoBid);
                        }
                    }

                    _context.Bid.Add(bid);
                }

                await _context.SaveChangesAsync();

                return Json("Test");
            }
            catch (Exception e)
            {
                TempData["Message"] = e.Message;

                return Json("test");
            }
        }

        [Route("/Ad/AddReview/{fkadid}")]
        public async Task<IActionResult> AddReview(Adcomments newComment)
        {
            if (HttpContext.Session.GetString(SessionTokenName) == null)
                return Redirect("/Users/Login");

            try
            {
                var uid = int.Parse(User.Identity.Name);
                newComment.FkUserId = uid;
                newComment.FkUser = await _context.User.FindAsync(uid);
                //newComment.FkAdId = id;
                newComment.FkAd = await _context.Ad.FindAsync(newComment.FkAdId);

                _context.Adcomments.Add(newComment);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return Redirect("/Ad/" + newComment.FkAdId);
            }
        }
    }
}