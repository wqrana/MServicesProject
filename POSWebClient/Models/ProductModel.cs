using System.ComponentModel.DataAnnotations;

namespace POSWebClient.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        [StringLength(25)]
        [Required]
        public string Code { get; set; }
        [StringLength(100)]
        [Required]
        public string Name { get; set; }
        [StringLength(250)]
        public string? Description { get; set; }
        [StringLength(50)]
        [Required]
        [Display(Name = "Category Type")]
        public string? CategoryType { get; set; }

        [StringLength(50)]
        [Required]
        public string? Brand { get; set; }
        [Display(Name = "Discount %")]
        public decimal? DefaultDiscountPct { get; set; }
        [Display(Name = "Initial Price")]
        public decimal? DefaultSalesPrice { get; set; }
    }

}
