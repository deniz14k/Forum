using Forum.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StudentsUnite_II.Areas.Identity.Data;
using StudentsUnite_II.Data;
using StudentsUnite_II.Models;
using System.Text;

namespace StudentsUnite_II.Controllers
{

    public class DiscussionsController : Controller
    {

        private readonly UserManager<ForumUser> _userManager;

        private readonly StudentsUnite_IIContext dbContext;
        public DiscussionsController(
            StudentsUnite_IIContext dbContext,
            UserManager<ForumUser> userManager
            )
        {
            _userManager = userManager;
            this.dbContext = dbContext;
        }

        [HttpGet]
        [Authorize]
        public IActionResult AddDiscussion()
        {
            return View();
        }

        private Task<ForumUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddDiscussion(AddDiscussionViewModel addDiscussionViewModel)
        {

            if (ModelState.IsValid) { 

                var tagNames = addDiscussionViewModel.category.TrimStart(' ').Split(',');

                var tags = new List<Tag>();

                foreach(var tagName in tagNames)
                {
                    var tagNameTrimmed = tagName.Trim();
                    if (!tagNameTrimmed.IsNullOrEmpty())
                    {
                        var tag = await dbContext.Tags.FirstOrDefaultAsync(t => t.name == tagNameTrimmed);
                        if (tag == null)
                        {
                            tag = new Tag
                            {
                                name = tagNameTrimmed,
                                dateOfCreation = DateTime.Now,
                                user = await _userManager.GetUserNameAsync(GetCurrentUserAsync().Result)

                            };
                            await dbContext.Tags.AddAsync(tag);
                        }
                        tags.Add(tag);
                    }

                }


                var discussion = new Discussion
                {
                    name = addDiscussionViewModel.name.TrimStart(' '),
                    category = string.Join(" , ",tagNames),
                    description = addDiscussionViewModel.description.TrimStart(' '),
                    dateOfCreation = DateTime.Now,
                    user = await _userManager.GetUserNameAsync(GetCurrentUserAsync().Result)
                    };

            
                await dbContext.Discussions.AddAsync(discussion);

                await dbContext.SaveChangesAsync();

                foreach(var tag in tags)
                {
                   var discussionTag = new DiscussionTag
                    {
                        discussionId = discussion.Id,
                        tagId = tag.Id
                    };
                    await dbContext.AddAsync(discussionTag);
                }
                await dbContext.SaveChangesAsync();


                return RedirectToAction("ListDiscussion", "Discussions");

            }
            else {
                return View(addDiscussionViewModel); 
            }

        }


        [HttpGet]
        public async Task<IActionResult> ListDiscussion(string searchString, bool myDiscussions = false)
        {
            var discussions = await dbContext.Discussions.OrderByDescending(d => d.dateOfCreation).ToListAsync();

            if (!String.IsNullOrEmpty(searchString))
            {
                discussions = await dbContext.Discussions.Where(d => d.name.Contains(searchString)).OrderByDescending(d => d.dateOfCreation).ToListAsync();

                if (myDiscussions)
                {
                    var userName = await _userManager.GetUserNameAsync(GetCurrentUserAsync().Result);
                    discussions = await dbContext.Discussions.Where(d => d.name.Contains(searchString) && d.user == userName).OrderByDescending(d => d.dateOfCreation).ToListAsync();
                }
            }

            if (myDiscussions)
            {
                var userName = await _userManager.GetUserNameAsync(GetCurrentUserAsync().Result);
                discussions = await dbContext.Discussions.Where(d=>  d.user == userName).OrderByDescending(d => d.dateOfCreation).ToListAsync();
            }


            ViewData["CurrentFilter"] = searchString;
            ViewData["MyDiscussions"] = myDiscussions;

            return View(discussions);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditDiscussion(Guid Id)
        {
            var discussion = await dbContext.Discussions.FindAsync(Id);


            return View(discussion);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditDiscussion(Discussion viewModel)
        { 
            var discussion = await dbContext.Discussions.FindAsync(viewModel.Id);
            string[] tagNames;
            var tags = new List<Tag>();

            if (discussion != null)
            {
                discussion.name = viewModel.name;

                if (discussion.description.Contains("[Edited]"))
                {   
                    discussion.description = viewModel.description;
                }
                else
                {
                     discussion.description = "[Edited]\n" + viewModel.description;
                }

                discussion.category = viewModel.category;

                tagNames = viewModel.category.Split(',');

                foreach (var tagName in tagNames)
                {
                    var tagNameTrimmed = tagName.Trim();
                    if (!tagNameTrimmed.IsNullOrEmpty())
                    {
                        var tag = await dbContext.Tags.FirstOrDefaultAsync(t => t.name == tagNameTrimmed);
                        if (tag == null)
                        {
                            tag = new Tag
                            {
                                name = tagNameTrimmed,
                                dateOfCreation = DateTime.Now,
                                user = await _userManager.GetUserNameAsync(GetCurrentUserAsync().Result)

                            };
                            await dbContext.Tags.AddAsync(tag);
                        }
                        tags.Add(tag);
                    }

                }

                var disussionTagsToRemove = await dbContext.DiscussionTags.Where(dt => dt.discussionId == discussion.Id).ToListAsync();

                dbContext.DiscussionTags.RemoveRange(disussionTagsToRemove);

                foreach (var tag in tags)
                {
                    var discussionTag = new DiscussionTag
                    {
                        discussionId = discussion.Id,
                        tagId = tag.Id
                    };
                    await dbContext.AddAsync(discussionTag);
                }

                await dbContext.SaveChangesAsync();
            }

         
            return RedirectToAction("ListDiscussion", "Discussions");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteDiscussion(Discussion viewModel)
        {
            var discussion = await dbContext.Discussions.AsNoTracking().FirstOrDefaultAsync(x => x.Id == viewModel.Id);

            if (discussion != null)
            {
                dbContext.Discussions.Remove(viewModel);
                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("ListDiscussion", "Discussions");
        }

    }


}

