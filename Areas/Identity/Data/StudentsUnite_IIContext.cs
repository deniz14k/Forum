using Forum.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentsUnite_II.Areas.Identity.Data;
using StudentsUnite_II.Models;

namespace StudentsUnite_II.Data
{
    public class StudentsUnite_IIContext : IdentityDbContext<ForumUser>
    {
        public StudentsUnite_IIContext(DbContextOptions<StudentsUnite_IIContext> options)
            : base(options)
        {
        }

        public DbSet<Discussion> Discussions { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<DiscussionTag> DiscussionTags { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure DiscussionTag relationships
            builder.Entity<DiscussionTag>().HasKey(dt => new { dt.discussionId, dt.tagId });

            builder.Entity<DiscussionTag>()
                .HasOne(dt => dt.discussion)
                .WithMany(d => d.discussionTags)
                .HasForeignKey(dt => dt.discussionId);

            builder.Entity<DiscussionTag>()
                .HasOne(dt => dt.tag)
                .WithMany(t => t.discussionTags)
                .HasForeignKey(dt => dt.tagId);

            // Configure Comment relationships
            builder.Entity<Comment>()
                .HasOne(c => c.Parent)
                .WithMany(c => c.Replies)
                .HasForeignKey(c => c.parentId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading deletes

            builder.Entity<Comment>()
                .HasOne(c => c.discussion)
                .WithMany(d => d.Comments)
                .HasForeignKey(c => c.discussionId)
                .OnDelete(DeleteBehavior.Cascade); // Comments are deleted if the Discussion is deleted

            // Ensure the primary key for Comment
            builder.Entity<Comment>()
                .HasKey(c => c.Id);

            // Ensure the primary key for Discussion
            builder.Entity<Discussion>()
                .HasKey(d => d.Id);
        }
    }
}
