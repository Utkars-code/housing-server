using System.ComponentModel.DataAnnotations;

namespace webApi_build_Real.Models
{
    public class City
    {
        [Key]
        public int Id {get; set; }
        [Required(ErrorMessage ="Name is mandatory field")]
        [StringLength(50,MinimumLength=2)]
        [RegularExpression(".*[a-zA-Z]+.*", ErrorMessage = "only Alphabates are allow.")]
        public string Name { get; set; }
        [Required]
        [RegularExpression(".*[a-zA-Z]+.*", ErrorMessage = "only Alphabates are allow.")]
        public string Country { get; set; }

        public DateTime LastUpdateOn { get; set; }
        public int LAstUpdatedBy { get; set; }
    }
}
