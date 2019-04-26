using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
namespace FoodAnalyzer
{
    public class ResponseObject
    {
        [JsonProperty("shouldIEat")]
        public bool ShouldIEat { get; set; }

        [JsonProperty("nutritionInfo")]
        public JObject NutritionInfo { get; set; }

        public ResponseObject()
        {
        }
    }
}
