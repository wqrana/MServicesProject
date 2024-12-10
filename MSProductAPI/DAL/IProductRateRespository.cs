namespace MSProductAPI.DAL
{
    public interface IProductRateRespository<TRate> : IGenericRespository<TRate> where TRate : class 
    {           
        IEnumerable<TRate> GetAllRateByProductId(int id);
        IEnumerable<TRate> GetAllEffectiveRate();
        TRate GetEffectiveRateByProductId(int id);
       
    }
}
