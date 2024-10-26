using System.ComponentModel.DataAnnotations;

namespace webApi_build_Real.Models
{
    public class Usercs
    {
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        public byte[] Password { get; set; }

        public byte[] PasswordKey { get; set; }
    }
}
