using StudentsUnite_II.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Forum.Models
{
    public class Tag
    {
        public Guid Id { get; set; }

        public string name { get; set; }

        [ForeignKey("Id")] public string user { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime dateOfCreation { get; set; }

        public ICollection<DiscussionTag> discussionTags { get; set; }

    }
}
