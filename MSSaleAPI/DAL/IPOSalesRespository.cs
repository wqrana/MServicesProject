namespace MSSalesAPI.DAL
{
    public interface IPOSalesRespository<TSales,TSalesDetail> : IGenericRespository<TSales> where TSales : class where TSalesDetail : class
    {           
        IEnumerable<TSalesDetail> GetAllDetailsBySalesId(int id);
        TSalesDetail GetSalesDetailById(int id);
        TSalesDetail AddSalesDetail(TSalesDetail entity);
        int UpdateSalesDetail(TSalesDetail entity);       
        int DeleteSalesDetail(int id);

    }
}
