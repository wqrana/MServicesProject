using Microsoft.AspNetCore.Authentication;
using POSWebClient.APIService;
using POSWebClient.APIService.Auth;
using POSWebClient.APIService.Operional;
using POSWebClient.Models;

namespace POSWebClient.APIGateWay
{
    public interface IAPIGateWayFactory
    {
        void setJWTSessionToken (string token);
        IAPIGenericService<CustomerModel> CustomerService {get;}
        IAuthenticateService AuthenicateService {get;}
        IAPIGenericService<ProductModel> ProductService { get; }
        IAPIProductRateService<ProductRateModel> ProductRateService { get; }
    }
}
