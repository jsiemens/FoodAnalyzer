using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodAnalyzer
{
    public class ParseResponse
    {
        public string Text { get; set; }

        public FoodContainer[] Parsed { get; set; }
    }

    public class FoodContainer
    {
        public Food Food { get; set; }
    }

    public class Food
    {
        public string FoodId { get; set; }

        public string Uri { get; set; }

        public string Label { get; set; }
    }

    public class NutritionRequest
    {
        [JsonProperty("ingredients")]
        public Ingredient[] Ingredients { get; set; }
    }

    public class Ingredient
    {
        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("measureURI")]
        public string MeasureURI { get; set; }

        [JsonProperty("foodId")]
        public string FoodId { get; set; }
    }
}
