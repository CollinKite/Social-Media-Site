using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Social_Media_Site.Data;
using Social_Media_Site.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace Social_Media_Site.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext db)
        {
            _context = db;
        }

        public IActionResult Index()
        {
            //if user is logged in, redirect to profile page
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Profile", new { id = User.FindFirstValue(ClaimTypes.NameIdentifier) });
            }
            return View();
        }
        
        public IActionResult About()
        {
            return View();
        }

        [Authorize]
        public IActionResult Search()
        {
            var users = _context.Userprofiles.ToList();
            return View(users);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Search(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return View();
            }
            var users = _context.Userprofiles.Where(p => p.Name.ToLower().Contains(key.ToLower())).ToList();
            return View(users);

        }

        [Authorize]
        [HttpGet("/Images/{id}")]
        public IActionResult ViewImages(string id)
        {
            var images = _context.Images.Where(i => i.UserId == id).ToList();
            Tuple <string, List<Images>> data = new(id, images);
            return View(data);
        }

        [Authorize]
        public IActionResult EditUser()
        {
            //guid
            string x = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //get user from profile model DB
            var user = _context.Userprofiles.Where(u => u.UserId == x).FirstOrDefault();
            if (user == null)
            {
                //create user
                user = new UserProfile();
                user.UserId = x;
                _context.Userprofiles.Add(user);
                _context.SaveChanges();
            }
            return View(user);
        }
        [Authorize]
        [HttpPost]
        public IActionResult EditUser(UserProfile person)
        {
            //guid
            person.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _context.Userprofiles.Update(person);
            _context.SaveChanges();
            return RedirectToAction("Profile", new { id = person.UserId });
        }
        [Authorize]
        public IActionResult UploadImage()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult UploadImage(Images file)
        {
            //guid
            file.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _context.Images.Add(file);
            _context.SaveChanges();
            return RedirectToAction("Profile", new { id = file.UserId });
        }

        [Authorize]
        [HttpGet("/Comment/{id}")]
        public IActionResult Comment(string id)
        {
            Message comment = new Message();
            comment.ReceiverId = id;
            return View(comment);
        }

        [Authorize]
        [HttpPost("/Comment/{id}")]
        public IActionResult Comment(Message comment)
        {
            //guid
            comment.SenderId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _context.Messages.Add(comment);
            _context.SaveChanges();
            return RedirectToAction("Profile", new { id = comment.ReceiverId });
        }

        [Authorize]
        [HttpGet("/Profile/{id}")]
        public IActionResult Profile(string id)
        {
            var user = _context.Userprofiles.Where(u => u.UserId == id).FirstOrDefault();
            var messages = _context.Messages.Where(m => m.ReceiverId == id).OrderByDescending(m => m.Date).ToList();
            var images = _context.Images.Where(i => i.UserId == id).ToList();
            bool isUserPage = (id == User.FindFirstValue(ClaimTypes.NameIdentifier)) ? true : false;
            Tuple<UserProfile, List<Message>, List<Images>, bool> tuple = new(user, messages, images, isUserPage);
            return View(tuple);
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}