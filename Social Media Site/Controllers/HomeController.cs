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
            return View();
        }
        
        public IActionResult About()
        {
            return View();
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

        [HttpPost]
        public IActionResult EditUser(UserProfile person)
        {
            //guid
            person.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _context.Userprofiles.Update(person);
            _context.SaveChanges();
            return View(person);
        }

        [Authorize]
        [HttpGet("/Profile/{id}")]
        public IActionResult Profile(string id)
        {
            var user = _context.Userprofiles.Where(u => u.UserId == id).FirstOrDefault();
            var messages = _context.Messages.Where(m => m.ReceiverId == id).ToList();
            var images = _context.Images.Where(i => i.UserId == id).ToList();
            Tuple<UserProfile, List<Message>, List<Images>> tuple = new Tuple<UserProfile, List<Message>, List<Images>>(user, messages, images);
            return View(tuple);
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}