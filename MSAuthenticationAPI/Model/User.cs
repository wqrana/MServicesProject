using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSAuthenticationAPI.Model
{
    [Table(name: "ApplicationUser")]
    public class ApplicationUser
    {
        
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [StringLength(50)]
        public string? LastName { get; set; }
        [StringLength(100)]
        [Required]
        public string Email { get; set; }
        [StringLength(50)]
        [Required]
        public string Password { get; set; }

    }

    public class LoginUserModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class AuthenticatedUserModel
    {
       public int Id { get; set; }
       public string FirstName { get; set; }
       public string Email { get; set; }       
       public string JwtToken { get; set; }

    }
}
