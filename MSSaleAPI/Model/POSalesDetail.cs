using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSSalesAPI.Model
{
    [Table("POSalesDetail")]
    public class POSalesDetail
    {
        public int Id { get; set; }
        [Required]
        public int POSalesId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal SalesPrice { get; set; }
        public decimal? SalesDiscount { get; set; }

        public virtual POSales POSales { get; set; }
        
    }
}
