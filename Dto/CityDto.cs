using System.ComponentModel.DataAnnotations;

namespace webApi_build_Real.Dto
{
    public class CityDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is mandatory field")]
        [StringLength(50, MinimumLength = 2)]
        [RegularExpression(".*[a-zA-Z]+.*",ErrorMessage ="only Alphabates are allow.")]
        public string Name { get; set; }
        [Required]
        [RegularExpression(".*[a-zA-Z]+.*", ErrorMessage = "only Alphabates are allow.")]
        public string Country { get; set; }
    }
}
