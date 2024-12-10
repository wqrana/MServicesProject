using System.ComponentModel.DataAnnotations;

namespace POSWebClient.Models
{
    public class UserLoginModel
    {
        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(50), DataType(DataType.Password), MinLength(6)]
        public string Password { get; set; }
    }

    public class UserRegistrationModel
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [StringLength(50)]
        public string? LastName { get; set; }
        [StringLength(100)]
        [Required]
        public string Email { get; set; }
        [StringLength(50), DataType(DataType.Password), MinLength(6)]
        [Required]
        public string Password { get; set; }

        [StringLength(50), DataType(DataType.Password), MinLength(6)]
        [Required]
        [Compare("Password")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }

    [Serializable]
    public class UserSessionInfo
    {
       
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string JwtToken { get; set; }

    }
   
}