using POSWebClient.Models;
using Newtonsoft.Json;
using Humanizer;

namespace POSWebClient.APIService.Operional
{
    public class CustomerAPIService : IAPIGenericService<CustomerModel>
    {
        private HttpClient httpClient = null!;
        public CustomerAPIService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }



        public CustomerModel Create(CustomerModel item)
        {
            var response = httpClient.PostAsJsonAsync("CustomerAPI", item).Result;
            //string jsonData = JsonConvert.SerializeObject(item);
            //var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
            //var response = httpClient.PostAsync("Customer", content).Result;


            if (response.IsSuccessStatusCode)
            {
                string res = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<CustomerModel>(res);
                return data;
            }
            else
            {

                throw new Exception($"Error:{response.StatusCode.ToString()}");
            }
            return null;
        }

        public CustomerModel Delete(int id)
        {
            var response = httpClient.DeleteAsync($"CustomerAPI/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                return new CustomerModel() { Id = id };
            }
            return null;
        }

        public CustomerModel Get(int id)
        {
            var response = httpClient.GetAsync($"CustomerAPI/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                string res = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<CustomerModel>(res);
                return data;
            }
            return null;
        }

        public IEnumerable<CustomerModel> GetAll()
        {
            var response = httpClient.GetAsync("CustomerAPI").Result;
            if (response.IsSuccessStatusCode)
            {
                string res = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<List<CustomerModel>>(res);
                return data;
            }
            return null;
        }

        public CustomerModel Update(CustomerModel item)
        {
            var response = httpClient.PutAsJsonAsync("CustomerAPI", item).Result;


            if (response.IsSuccessStatusCode)
            {

                return item;
            }
            else
            {

                throw new Exception($"Error:{response.StatusCode.ToString()}");
            }
            return null;
        }
    }
}
