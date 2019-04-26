using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FoodAnalyzer
{
    class ShouldIEat
    {
        public static bool Decide(NutritionInfo nutrition, out string[] reasons)
        {
            double totalCalories = nutrition.Calories;
            double fatPercent = nutrition.Fat * 9 / totalCalories;
            double sugarPercent = nutrition.Sugar * 4 / totalCalories;
            double sodiumAdjusted = nutrition.Sodium * 2300 / nutrition.Calories;

            List<string> failures = new List<string>();
            if (fatPercent > 0.15)
            {
                failures.Add("High fat");
            }

            if (sugarPercent > 0.1)
            {
                failures.Add("High sugar");
            }

            if (sodiumAdjusted > 2300)
            {
                failures.Add("High sodium");
            }

            reasons = failures.ToArray();

            return !failures.Any();
        }
    }
}
