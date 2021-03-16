using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace DT102G_project_web.Services
{
    public class ApiHelper
    {
        //Get all
        public static async Task<List<T>> GetAllObjects<T>(string url)
        {
            //create client 
            var client = new HttpClient
            {
                //base adress
                BaseAddress = new Uri("https://localhost:44325/api/")
            };

            //define headers
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var returnobj = new List<T>();
            try
            {
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    returnobj = JsonConvert.DeserializeObject<List<T>>(content);
                }
            }
            catch
            {
                return new List<T>();
            }
            return returnobj;
        }



        /*post 
        private async Task<T> PostRequest<T>(string url, object model)
        {
            
            //create client 
            var client = new HttpClient
            {
                //base adress
                BaseAddress = new Uri("https://localhost:44325/api/")
            };

            //define headers
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var serializedObject = JsonConvert.SerializeObject(model);

            HttpResponseMessage response = await client.PostAsJsonAsync(
                url, model);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return T;
        }*/



        //get one



        /*post
        private async Task<TOut> PostRequest<TIn, TOut>(string uri, TIn content)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                    var serialized = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");

                    using (HttpResponseMessage response = await client.PostAsync(uri, serialized))
                    {
                        response.EnsureSuccessStatusCode();
                        string responseBody = await response.Content.ReadAsStringAsync();

                        return JsonConvert.DeserializeObject<TOut>(responseBody);
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }*/


        //update


        //delete 

    }
}
