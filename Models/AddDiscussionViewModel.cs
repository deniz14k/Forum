using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StudentsUnite_II.Models
{
    public class AddDiscussionViewModel
    {
        public string category { get; set; }

        public string name { get; set; }

        public string description { get; set; }

    }
}
