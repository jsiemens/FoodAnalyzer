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

        public FoodContainer[] Hints { get; set; }
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

    public class NutritionInfo
    {
        public double Calories { get; set; }

        public string Uri { get; set; }

        public double TotalWeight { get; set; }

        public string[] HealthLabels { get; set; }

        [JsonProperty("totalNutrients")]
        public Dictionary<string, NutrientDescriptor> Nutrients { get; set; }

        public double Fat => TryGet("FAT");

        public double SaturedFat => TryGet("FASAT");

        public double MonounsaturatedFat => TryGet("FAMS");

        public double PolyunsaturatedFat => TryGet("FAPU");

        public double Carbs => TryGet("CHOCDF");

        public double Sugar => TryGet("SUGAR");

        public double Protein => TryGet("PROCNT");

        public double Sodium => TryGet("NA");

        public double Cholesterol => TryGet("CHOLE");

        public NutritionInfo()
        {
        }

        private double TryGet(string name)
        {
            if (Nutrients == null)
            {
                return 0;
            }

            if (Nutrients.TryGetValue(name, out var value))
            {
                return value.Quantity;
            }

            return 0;
        }
    }

    public class NutrientDescriptor
    {
        public string Label { get; set; }

        public double Quantity { get; set; }

        public string unit { get; set; }
    }
}
