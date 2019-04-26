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
        [JsonProperty("foodId")]
        public string FoodId { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("categoryLabel")]
        public string CategoryLabel { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }
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
