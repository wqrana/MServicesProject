using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json.Linq;
using NuGet.Common;
using POSWebClient.APIService;
using POSWebClient.APIService.Auth;
using POSWebClient.APIService.Operional;
using POSWebClient.Models;

namespace POSWebClient.APIGateWay
{
    public class APIGateWayService: IAPIGateWayFactory
    {
        private IAPIGenericService<CustomerModel> customerService = null;
        private IAuthenticateService authenticateService = null;
        private IAPIGenericService<ProductModel> productService = null;
        private IAPIProductRateService<ProductRateModel> productRateService = null;

        private string apiGateWayUrl = string.Empty;       
        private readonly HttpClient httpClient;
        private string jWTSessionToken = string.Empty;

        public APIGateWayService(string apiGateWayUrl)
        {
            this.apiGateWayUrl = apiGateWayUrl;
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(apiGateWayUrl);
        }
        public void setJWTSessionToken(string token) {
            
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }
        public IAPIGenericService<CustomerModel> CustomerService
        {
            get
            {
                if (customerService == null)
                {                   
                    
                    customerService = new CustomerAPIService(httpClient);

                }
                return customerService;
            }
        }
        public IAPIGenericService<ProductModel> ProductService
        {
            get
            {
                if (productService == null)
                {

                    productService = new ProductAPIService(httpClient);

                }
                return productService;
            }
        }
        public IAuthenticateService AuthenicateService { 
        
            get
            {
                if (authenticateService == null)
                {
                    authenticateService = new AuthenticateService(httpClient);
                }
                return authenticateService;
            }
        }

        public IAPIProductRateService<ProductRateModel> ProductRateService
        {

            get
            {
                if (productRateService == null)
                {
                    productRateService = new ProductRateAPIService(httpClient);
                }
                return productRateService;
            }
        }
    }
}
