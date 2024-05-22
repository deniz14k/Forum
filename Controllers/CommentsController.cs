using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using StudentsUnite_II.Areas.Identity.Data;
using StudentsUnite_II.Data;
using StudentsUnite_II.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Forum.Models;

namespace StudentsUnite_II.Controllers
{
    public class CommentsController : Controller
    {
        private readonly UserManager<ForumUser> _userManager;
        private readonly StudentsUnite_IIContext dbContext;

        public CommentsController(StudentsUnite_IIContext dbContext, UserManager<ForumUser> userManager)
        {
            _userManager = userManager;
            this.dbContext = dbContext;
        }

        private Task<ForumUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddComment(AddCommentViewModel addCommentViewModel)
        {
            var currentUser = await GetCurrentUserAsync();

            var comment = new Comment
            {
                content = addCommentViewModel.content,
                dateOfCreation = DateTime.Now,
                user = currentUser.UserName,
                discussionId = addCommentViewModel.discussionId
            };
            await dbContext.Comments.AddAsync(comment);
            await dbContext.SaveChangesAsync();

            return RedirectToAction("OpenDiscussion", new { id = addCommentViewModel.discussionId });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddReply(AddReplyViewModel addReplyViewModel)
        {
            var currentUser = await GetCurrentUserAsync();

            var reply = new Comment
            {
                content = addReplyViewModel.content,
                dateOfCreation = DateTime.Now,
                user = currentUser.UserName,
                discussionId = addReplyViewModel.discussionId,
                parentId = addReplyViewModel.parentId
            };
            await dbContext.Comments.AddAsync(reply);
            await dbContext.SaveChangesAsync();

            return RedirectToAction("OpenDiscussion", new { id = addReplyViewModel.discussionId });
        }

        [HttpGet]
        public async Task<IActionResult> OpenDiscussion(Guid id)
        {
            var discussion = await dbContext.Discussions
                .Include(d => d.Comments)
                    .ThenInclude(c => c.Replies) // Include replies
                .FirstOrDefaultAsync(d => d.Id == id);

            if (discussion == null)
            {
                return NotFound();
            }

            ViewData["DiscussionId"] = id;

            return View(discussion);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditComment(Guid id)
        {
            var comment = await dbContext.Comments.FindAsync(id);
            var currentUser = await GetCurrentUserAsync();

            if (comment == null || comment.user != currentUser.UserName)
            {
                return Forbid();
            }

            return View(comment);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdateComment(Comment comment)
        {
            var currentUser = await GetCurrentUserAsync();

            
            
                var existingComment = await dbContext.Comments.FindAsync(comment.Id);
                if (existingComment == null || existingComment.user != currentUser.UserName)
                {
                    return Forbid();
                }

                existingComment.content = comment.content;
                dbContext.Comments.Update(existingComment);
                await dbContext.SaveChangesAsync();
                return RedirectToAction("OpenDiscussion", new { id = comment.discussionId });
        
            return View("EditComment", comment);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteComment(Guid id)
        {
            var comment = await dbContext.Comments
                .Include(c => c.Replies)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (comment == null)
            {
                return NotFound();
            }

            // Manually delete replies
            foreach (var reply in comment.Replies)
            {
                dbContext.Comments.Remove(reply);
            }

            dbContext.Comments.Remove(comment);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("OpenDiscussion", new { id = comment.discussionId });
        }

    }
}
