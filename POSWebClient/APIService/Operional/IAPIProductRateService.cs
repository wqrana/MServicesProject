using Newtonsoft.Json;
using POSWebClient.APIService.Helper;
using POSWebClient.Models;
using System.Reflection;

namespace POSWebClient.APIService.Operional
{
    public interface IAPIProductRateService<T> : IAPIGenericService<T> where T : class
    {
       IEnumerable<ProductRateModel> GetAllEffectiveProductRate();
       IEnumerable<ProductRateModel> GetAllProductRateByProduct(int id);
       T GetEffectiveProductRateByProduct(int id);
        
    }
    public class ProductRateAPIService : IAPIProductRateService<ProductRateModel>
    {
        private readonly HttpClient httpClient;
        private readonly APIActionHelper<ProductRateModel> apiActionHelper;
        public ProductRateAPIService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            apiActionHelper = new APIActionHelper<ProductRateModel>(httpClient);
            apiActionHelper.APIUpStreamURL="ProductRateAPI";
        }
        public ProductRateModel Create(ProductRateModel model)
        {
           apiActionHelper.APIUpStreamActionURL = "PostProductRate";
           var retResult= apiActionHelper.Create(model);

           return retResult;
        }   

        public ProductRateModel Delete(int id)
        {
            ProductRateModel returnModel = null;
            apiActionHelper.APIUpStreamActionURL = "DeleteProductRate";
            var retResult = apiActionHelper.Delete(id);

            if (retResult ==true)
                returnModel = new ProductRateModel() { Id = id };
            return returnModel;
            
        }

        public ProductRateModel Get(int id)
        {
            apiActionHelper.APIUpStreamActionURL = "GetProductRateById";
            var retResult = apiActionHelper.Get(id);
            
            return retResult;
        }
        public IEnumerable<ProductRateModel> GetAll()
        {
            apiActionHelper.APIUpStreamActionURL = "GetAllProductRates";
            var retResult = apiActionHelper.GetAll();

            return retResult;
           
        }
        public IEnumerable<ProductRateModel> GetAllEffectiveProductRate()
        {
            apiActionHelper.APIUpStreamActionURL = "GetAllProductEffectiveRates";
            var retResult = apiActionHelper.GetAll();

            return retResult;
           
        }
        public IEnumerable<ProductRateModel> GetAllProductRateByProduct(int id)
        {
            apiActionHelper.APIUpStreamActionURL = $"GetAllProductRateByProduct/{id}";
            var retResult = apiActionHelper.GetAll();

            return retResult;           
        }

        public ProductRateModel GetEffectiveProductRateByProduct(int id)
        {
            apiActionHelper.APIUpStreamActionURL = "GetProductEffectiveRateByProduct";
            var retResult = apiActionHelper.Get(id);
            
            return retResult;
        }

        public ProductRateModel Update(ProductRateModel model)
        {
            apiActionHelper.APIUpStreamActionURL = "PutProductRate";
            var retResult = apiActionHelper.Update(model);

            return retResult;
            
        }
    }
}
