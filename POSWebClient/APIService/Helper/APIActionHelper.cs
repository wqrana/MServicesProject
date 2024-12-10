using Newtonsoft.Json;
using POSWebClient.Models;
using System.Net.Http;
using System.Reflection;

namespace POSWebClient.APIService.Helper
{
    public class APIActionHelper<TModel> where TModel : class
    {
        private readonly HttpClient aPIHttpClient;
        public APIActionHelper(HttpClient httpClient)
        {
            aPIHttpClient = httpClient;
        }
       public string APIUpStreamURL { get; set; }
        public string? APIUpStreamActionURL { get; set; }=string.Empty;
       public IEnumerable<TModel> GetAll()
        {
            var response = aPIHttpClient.GetAsync($"{APIUpStreamURL}/{APIUpStreamActionURL}").Result;

            if (response.IsSuccessStatusCode)
            {
                string res = response.Content.ReadAsStringAsync().Result;
                var returnData = JsonConvert.DeserializeObject<List<TModel>>(res);
                if (returnData != null)
                {
                    return returnData;
                }
            }
            else
            {
                var result = response.Content.ReadAsStringAsync().Result;
                throw new Exception(result);
            }
            return null;
           
        }
       public TModel Get(int id)
        {
            APIUpStreamActionURL = string.IsNullOrEmpty(APIUpStreamActionURL) ? $"{id}" : $"{APIUpStreamActionURL}/{id}";
            var response = aPIHttpClient.GetAsync($"{APIUpStreamURL}/{APIUpStreamActionURL}").Result;

            if (response.IsSuccessStatusCode)
            {
                string res = response.Content.ReadAsStringAsync().Result;
                var returnData = JsonConvert.DeserializeObject<TModel>(res);
                if (returnData != null)
                {
                    return returnData;
                }
            }
            else
            {
                var result = response.Content.ReadAsStringAsync().Result;
                throw new Exception(result);
            }
            return null;
        }
       public TModel Create(TModel model)
        {
            var response = aPIHttpClient.PostAsJsonAsync($"{APIUpStreamURL}/{APIUpStreamActionURL}", model).Result;

            if (response.IsSuccessStatusCode)
            {
                string res = response.Content.ReadAsStringAsync().Result;
                var returnData = JsonConvert.DeserializeObject<TModel>(res);
                if (returnData != null)
                {
                    return returnData;
                }

            }
            else
            {
                var result = response.Content.ReadAsStringAsync().Result;
                throw new Exception(result);
            }
            return null;
        }
       public TModel Update(TModel model)
        {
            var response = aPIHttpClient.PutAsJsonAsync($"{APIUpStreamURL}/{APIUpStreamActionURL}", model).Result;

            if (response.IsSuccessStatusCode)
            {
                string res = response.Content.ReadAsStringAsync().Result;
                var returnData = JsonConvert.DeserializeObject<TModel>(res);
                if (returnData != null)
                {
                    return returnData;
                }

            }
            else
            {
                var result = response.Content.ReadAsStringAsync().Result;
                throw new Exception(result);
            }
            return null;
        }
       public bool Delete(int id)
        {
            APIUpStreamActionURL = string.IsNullOrEmpty(APIUpStreamActionURL) ? $"{id}": $"{APIUpStreamActionURL}/{id}";
            var response = aPIHttpClient.DeleteAsync($"{APIUpStreamURL}/{APIUpStreamActionURL}").Result;
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                var result = response.Content.ReadAsStringAsync().Result;
                throw new Exception(result);
            }
            return false;
        }
    }
}
