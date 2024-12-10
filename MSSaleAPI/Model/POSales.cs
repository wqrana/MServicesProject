using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSSalesAPI.Model
{
    [Table("POSales")]
    public class POSales
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        [StringLength(25)]
        public string InvoiceNo { get; set; }
        public DateTime? InvoiceDate {  get; set; }   
        
        public ICollection<POSalesDetail> POSalesDetails { get; set; }
       
    }
}
