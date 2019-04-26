using System;
namespace FoodAnalyzer
{
    public class ResponseObject
    {
        public bool ShouldIEat { get; set; }

        public NutritionInfo NutritionInfo { get; set; }

        public ResponseObject()
        {
        }
    }
}
