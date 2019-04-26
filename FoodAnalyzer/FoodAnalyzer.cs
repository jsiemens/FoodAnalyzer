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
            try
            {
                log.LogInformation("C# HTTP trigger function processed a request.");

                string name = req.Query["name"];

                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                dynamic data = JsonConvert.DeserializeObject(requestBody);
                name = name ?? data?.name;

                var result = await NutritionClient.GetNutrition(name);

                if (result == null)
                {
                    string json = JsonConvert.SerializeObject(new
                    {
                        error = "Food not found"
                    }, Formatting.Indented);

                    return new OkObjectResult(json);
                }
                else
                {
                    var nutritionInfo = result.NutritionInfo.ToObject<NutritionInfo>();
                    bool shouldIEat = ShouldIEat.Decide(nutritionInfo, out var reasons);

                    var jsonObj = new ResponseObject()
                    {
                        ShouldIEat = shouldIEat,
                        NutritionInfo = result.NutritionInfo,
                        Food = result.Food,
                        Warnings = reasons
                    };


                    string json = JsonConvert.SerializeObject(jsonObj, Formatting.Indented);

                    return new OkObjectResult(json);
                }
            }
            catch (Exception e)
            {
                string json = JsonConvert.SerializeObject(new
                {
                    error = $"{e.GetType()}: {e.Message}"
                }, Formatting.Indented);

                return new OkObjectResult(json);
            }
        }
    }
}
