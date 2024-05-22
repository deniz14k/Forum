using StudentsUnite_II.Areas.Identity.Data;
using StudentsUnite_II.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
namespace Forum.Models
{
    public class Discussion
    {
        public Guid Id { get; set; }


        public string category { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime dateOfCreation { get; set; }

        [ForeignKey("Id")] public string user { get; set; }

        public string name { get; set; }

        public string description { get; set; }

        public ICollection<DiscussionTag> discussionTags { get; set; }
        public ICollection<Comment> Comments { get; set; }

    }
}
