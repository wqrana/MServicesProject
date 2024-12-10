namespace POSWebClient.Models
{
    public class SalesViewModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string SalesInvoiceNo { get; set; }       
        public DateTime SalesDate { get; set; }
    }
    public class SalesDetailViewModel
    {
        public int Id { get; set; }
        public int SalesId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal ProductRate { get; set; }
        public decimal ProductDisPct { get; set; }
        public decimal ProductDisAmount { get; set; }

        public decimal ProductTotal
        {
            get { return (Quantity * ProductRate); }
        }
        public decimal ProductDiscountTotal
        {
            get { return (Quantity * ProductDisAmount); }

        }
        public decimal getProductDisAmount()
        {
          if (ProductDisPct > 0) 
             return (Math.Round(ProductRate * (ProductDisPct / 100),2)); 
          else return 0; 
            
        }
        public decimal getProductDisPct()
        {
            if (ProductDisAmount > 0)
                return (Math.Round((ProductDisAmount/ProductRate) * 100, 2));
             else return 0;
         }
    }
}
