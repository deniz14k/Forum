using Forum.Models;

namespace StudentsUnite_II.Models
{
    public class DiscussionTag
    {
        public Guid discussionId { get; set; }
        public Discussion discussion { get; set; }
        
        public Guid tagId { get; set; }
        public Tag  tag { get; set; }
    }
}
