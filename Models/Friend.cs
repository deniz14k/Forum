using Microsoft.EntityFrameworkCore;
using StudentsUnite_II.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Models
{

    [Keyless]
    public class Friend
    {
        public ForumUser user {  get; set; }
        public ForumUser friend {  get; set; }
         


    }
}
