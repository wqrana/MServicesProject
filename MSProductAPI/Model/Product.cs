using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSProductAPI.Model
{
    [Table("Product")]
    public class Product
    {
        public int Id { get; set; }
        [StringLength(25)]
        public string Code { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(250)]
        public string? Description { get; set; }
        [StringLength(50)]
        public string? CategoryType { get; set; }     
        
        [StringLength(50)]
        public string? Brand { get; set; }
        public decimal? DefaultDiscountPct { get; set; }
        public decimal? DefaultSalesPrice { get; set; }
    }
}
