using System;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FoodAnalyzer
{
    public class NutritionClient
    {
        public NutritionClient()
        {
        }

        public static async Task<JObject> GetNutrition(string term)
        {
            string baseUrl = "https://api.edamam.com/api/food-database/parser";

            term = term ?? "banana";

            var builder = new UriBuilder(baseUrl);
            NameValueCollection parameters = new NameValueCollection();
            parameters["app_id"] = "07d50733";
            parameters["app_key"] = "80fcb49b500737827a9a23f7049653b9";
            parameters["ingr"] = term;
            parameters["nutrition-type"] = "logging";
            builder.Query = ToQueryString(parameters);
            var uri = builder.Uri;

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                var stringResponse = await response.Content.ReadAsStringAsync();
                var jobj = JObject.Parse(stringResponse);
                var parsed = jobj.ToObject<ParseResponse>();

                if (parsed.Parsed?.Any() ?? false)
                {
                    var foodId = parsed.Parsed[0].Food.FoodId;
                    var details = await GetDetails(foodId);
                    return details;
                }
            }

            return null;
        }

        private static async Task<JObject> GetDetails(string foodId)
        {
            using (var client = new HttpClient())
            {
                var ingredientsJsonObj = new NutritionRequest()
                {
                    Ingredients = new Ingredient[]
                    {
                        new Ingredient()
                        {
                            FoodId = foodId,
                            MeasureURI = "http://www.edamam.com/ontologies/edamam.owl#Measure_unit",
                            Quantity = 1
                        }
                    }
                };
                string json = JsonConvert.SerializeObject(ingredientsJsonObj);

                string baseUrl = "https://api.edamam.com/api/food-database/nutrients?app_id=b8de1742&app_key=fcadd94bbd1f2b0ec13433c053daf5ba";
                var uri = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsync(baseUrl, new StringContent(json, Encoding.UTF8, "application/json"));
                string jsonResponse = await response.Content.ReadAsStringAsync();
                var jobj = JObject.Parse(jsonResponse);
                return jobj;
            }
        }


        private static string ToQueryString(NameValueCollection nvc)
        {
            var array = (from key in nvc.AllKeys
                         from value in nvc.GetValues(key)
                         select string.Format("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(value)))
                .ToArray();
            return "?" + string.Join("&", array);
        }
    }
}
