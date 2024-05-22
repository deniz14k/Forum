namespace StudentsUnite_II.Models
{
    public class AddReplyViewModel
    {
        public string content { get; set; }
        public Guid discussionId { get; set; }
        public Guid parentId { get; set; }
    }

}
