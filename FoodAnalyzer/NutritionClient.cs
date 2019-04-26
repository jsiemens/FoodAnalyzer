using System;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json.Linq;

namespace FoodAnalyzer
{
    public class NutritionClient
    {
        public NutritionClient()
        {
        }

        public static async Task<NutritionInfo> GetNutrition(string term)
        {
            string baseUrl = "https://api.edamam.com/api/food-database/parser";

            term = term ?? "banana";

            var builder = new UriBuilder(baseUrl);
            NameValueCollection parameters = new NameValueCollection();
            parameters["app_id"] = "07d50733"; 
            parameters["app_key"]="80fcb49b500737827a9a23f7049653b9";
            parameters["ingr"] = term;
            parameters["nutrition-type"] = "logging";
            builder.Query = ToQueryString(parameters);
            var uri = builder.Uri;

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                var stringResponse = await response.Content.ReadAsStringAsync();
                dynamic jobj = JObject.Parse(stringResponse);
                var label = jobj.parsed.food.label;
            }

            var info = new NutritionInfo()
            {
                Calories = 10,
                Protein = 20,
                Name = term
            };

            return info;
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
