using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSProductAPI.Model
{
    [Table("ProductRate")]
    public class ProductRate
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        [StringLength(250)]
        public string Description { get; set; }
        public DateTime EffectiveDate { get; set; }       
        public decimal EffectiveRate { get; set; }       
        public Product? Product { get; set; }

    }


}
