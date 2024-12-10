using System.ComponentModel.DataAnnotations;

namespace MSCustomerAPI.Model
{
    public class Customer
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [StringLength(50)]
        public string? LastName { get; set; }
        [EmailAddress]
        [StringLength(100)]
        public string? Email { get; set; }
        [Required]
        [StringLength(15)]
        public string Phone { get; set; }
        [StringLength(250)]
        public string? Address { get; set; }
    }
}
