using StudentsUnite_II.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Models
{
    public class Comment
    {
        public Guid Id { get; set; }

        public Guid discussionId { get; set; }
        public Discussion discussion { get; set; } = null;
        [ForeignKey("Id")] public String user { get; set; }


        [DataType(DataType.DateTime)]
        public DateTime dateOfCreation { get; set; }
        public string content { get; set; }

        public Guid? parentId { get; set; } // Nullable to indicate top-level comments
        public virtual Comment Parent { get; set; }
        public virtual ICollection<Comment> Replies { get; set; } = new List<Comment>();



    }
}