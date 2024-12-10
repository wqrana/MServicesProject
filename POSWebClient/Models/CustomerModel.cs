using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace POSWebClient.Models
{
    public class CustomerModel
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        
        public string? LastName { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        [Phone]
        [Required]
        public string Phone { get; set; }
        public string? Address { get; set; }
    }
}
