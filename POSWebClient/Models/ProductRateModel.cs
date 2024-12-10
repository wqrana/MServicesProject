using System.ComponentModel.DataAnnotations;

namespace POSWebClient.Models
{
    public class ProductRateModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Product Code")]
        public int ProductId { get; set; }
        [StringLength(250)]
        [Display(Name = "Rate Description")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Effective Date")]
        public DateTime EffectiveDate { get; set; }
        [Required]
        [Display(Name = "Effective Rate")]
        public decimal EffectiveRate { get; set; }
        public ProductModel? Product { get; set; }
    }
}
