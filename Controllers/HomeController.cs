using Forum.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentsUnite_II.Areas.Identity.Data;
using StudentsUnite_II.Data;
using StudentsUnite_II.Models;
using System.Diagnostics;

namespace StudentsUnite_II.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly UserManager<ForumUser> _userManager;

        private readonly StudentsUnite_IIContext dbContext;


            public HomeController(
                ILogger<HomeController> logger,
                StudentsUnite_IIContext dbContext,
                UserManager<ForumUser> userManager)
            {
                _logger = logger;
                _userManager = userManager;
                this.dbContext = dbContext;
            }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Discussion> discussions = await dbContext.Discussions.OrderByDescending(d => d.dateOfCreation).ToListAsync();
            
            if(discussions.Count > 5)
            {
                discussions = discussions.Take(5).ToList();
            }

            return View(discussions);
        }


    }
}
