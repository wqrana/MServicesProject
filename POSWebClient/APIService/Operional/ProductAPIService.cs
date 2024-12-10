using Newtonsoft.Json;
using POSWebClient.Models;
using System.Reflection;

namespace POSWebClient.APIService.Operional
{
    public class ProductAPIService : IAPIGenericService<ProductModel>

    {
        private readonly HttpClient httpClient;

        public ProductAPIService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public ProductModel Create(ProductModel model)
        {
            var response = httpClient.PostAsJsonAsync("ProductAPI", model).Result;
            
            if (response.IsSuccessStatusCode)
            {
                string res = response.Content.ReadAsStringAsync().Result;
                var product = JsonConvert.DeserializeObject<ProductModel>(res);
                if (product != null)
                {
                    return product;
                }

            }
            else
            {
                var result = response.Content.ReadAsStringAsync().Result;
                throw new Exception(result);
            }
            return null;
        }

        public ProductModel Delete(int id)
        {
            var response = httpClient.DeleteAsync($"ProductAPI/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                return new ProductModel() { Id = id };
            }
            else
            {
                var result = response.Content.ReadAsStringAsync().Result;
                throw new Exception(result);
            }
            return null;
        }

        public ProductModel Get(int id)
        {
            var response = httpClient.GetAsync($"ProductAPI/{id}").Result;

            if (response.IsSuccessStatusCode)
            {
                string res = response.Content.ReadAsStringAsync().Result;
                var product = JsonConvert.DeserializeObject<ProductModel>(res);
                if (product != null)
                {
                    return product;
                }

            }
            else
            {
                var result = response.Content.ReadAsStringAsync().Result;
                throw new Exception(result);
            }
            return null;
        }

        public IEnumerable<ProductModel> GetAll()
        {
            var response = httpClient.GetAsync("ProductAPI").Result;

            if (response.IsSuccessStatusCode)
            {
                string res = response.Content.ReadAsStringAsync().Result;
                var productList = JsonConvert.DeserializeObject<List<ProductModel>>(res);
                if (productList != null)
                {
                    return productList;
                }

            }
            else
            {
                var result = response.Content.ReadAsStringAsync().Result;
                throw new Exception(result);
            }
            return null;
        }

        public ProductModel Update(ProductModel item)
        {
            var response = httpClient.PutAsJsonAsync("ProductAPI", item).Result;
            if (response.IsSuccessStatusCode)
            {
                return item;
            }
            else
            {
                var result = response.Content.ReadAsStringAsync().Result;
                throw new Exception(result);
            }
            return null;
        }
    }
}
