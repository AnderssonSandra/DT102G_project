using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Net;
using System.IO;

namespace DT102G_project_web.Services
{
    public class ApiHelper
    {
        //GET ALL
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


        //GET ONE
        public static async Task<T> GetOneObject<T>(string url, int? id)
        {
            //create client 
            var client = new HttpClient
            {
                //base adress
                BaseAddress = new Uri("https://localhost:44325/api/")
            };

            //define headers
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var returnobj = default(T);

            try
            {
                var response = await client.GetAsync(url + "/" + id);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    returnobj = JsonConvert.DeserializeObject<T>(content);
                }
            }
            catch
            {
                return default(T);
            }
            return returnobj;
        }


        //POST
        public static async Task<bool> PostObject<T>(string url, T reqData)
        {
            var jsonString = JsonConvert.SerializeObject(reqData);

            StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            //create client 
            var client = new HttpClient
            {
                //base adress
                BaseAddress = new Uri("https://localhost:44325/api/")
            };

            //send post  request
            HttpResponseMessage response = await client.PostAsync(url, content);

            if (response.StatusCode == HttpStatusCode.Created)
            {
                return true;
            } else
            {
                return false;
            }      
        }

        //DELETE
        public static async Task<bool> DeleteObject<T>(string url, int id)
        {
            //create client 
            var client = new HttpClient
            {
                //base adress
                BaseAddress = new Uri("https://localhost:44325/api/")
            };

            //define headers
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var returnobj = false;

            try
            {
                var response = await client.DeleteAsync(url + "/" + id);
                if (response.IsSuccessStatusCode)
                {
                    returnobj = true;
                }
            }
            catch
            {
                return false;
            }
            return returnobj;
        }




        //delete 

    }
}
