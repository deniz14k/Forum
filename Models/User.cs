using System.ComponentModel.DataAnnotations;

namespace Forum.Models
{
    public class User
    {
        public ICollection<User> Friends { get; set; }
    }
}
