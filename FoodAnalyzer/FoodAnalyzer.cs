using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FoodAnalyzer
{
    public static class FoodAnalyzer
    {
        [FunctionName("FoodAnalyzer")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            var result = await NutritionClient.GetNutrition(name);
            var jsonObj = new ResponseObject()
            {
                ShouldIEat = new Random().Next() % 2 == 0,
                NutritionInfo = result.NutritionInfo,
                Food = result.Food
            };

            string json = JsonConvert.SerializeObject(jsonObj, Formatting.Indented);

            return new OkObjectResult(json);
        }
    }
}
