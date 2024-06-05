using Forum.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentsUnite_II.Areas.Identity.Data;
using StudentsUnite_II.Data;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;

namespace StudentsUnite_II.Controllers
{
    public class AdminController : Controller
    {

        private readonly UserManager<ForumUser> _userManager;

        private readonly StudentsUnite_IIContext dbContext;
        public AdminController(
            StudentsUnite_IIContext dbContext,
            UserManager<ForumUser> userManager
            )
        {
            _userManager = userManager;
            this.dbContext = dbContext;
        }


        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ListUsers()
        {
            var users = await _userManager.GetUsersInRoleAsync("Member");
            return View( users);
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> BlockUser(Guid Id)
        {
            var user = await _userManager.FindByIdAsync(Id.ToString());
            return View(user);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> BlockUser(ForumUser viewModel)
        {

            var user = await _userManager.FindByIdAsync(viewModel.Id);
            var lockoutDate = DateTimeOffset.MaxValue;



            await _userManager.SetLockoutEndDateAsync(user, lockoutDate);


            return RedirectToAction("ListUsers", "Admin");
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UnBlockUser(Guid Id)
        {
            var user = await _userManager.FindByIdAsync(Id.ToString());
            return View(user);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UnBlockUser(ForumUser viewModel)
        {

            var user = await _userManager.FindByIdAsync(viewModel.Id);

            await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.Now);

            return RedirectToAction("ListUsers", "Admin");
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult ListUsersDiscussions(ForumUser User)
        {
            var discussions = dbContext.Discussions.Where(d => d.user == User.Id).OrderByDescending(d => d.dateOfCreation).ToList();
            return View(discussions);
        }




        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteDiscussion(Guid Id)
        {
            var discussion = await dbContext.Discussions.AsNoTracking().FirstOrDefaultAsync(x => x.Id == Id);

            if (discussion != null)
            {
                var userName = discussion.user;

                dbContext.Discussions.Remove(discussion);
                await dbContext.SaveChangesAsync();

                if (userName != null)
                {
                    var url = Url.Content("~/Admin/ListUsersDiscussions/" + userName);
                    return Redirect(url);
                }
            }

            return RedirectToAction("ListUsers", "Admin");
        }




    }
}
