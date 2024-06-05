using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using StudentsUnite_II.CustomValidations;

namespace StudentsUnite_II.Models
{
    public class AddDiscussionViewModel
    {
        [Required]
        [NoWhiteSpaceOnly]
        public string category { get; set; }

        [Required]
        [NoWhiteSpaceOnly]
        public string name { get; set; }

        [Required]
        [NoWhiteSpaceOnly]
        public string description { get; set; }

    }
}
