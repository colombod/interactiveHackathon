namespace BarcodeScanner.Spoonacular
{
    public class IngredientInfo
    {
        public string ImageSmallUrl => $"https://spoonacular.com/cdn/ingredients_100x100/{image}";
        public string ImageMediumUrl => $"https://spoonacular.com/cdn/ingredients_250x250/{image}";
        public string ImageLargeUrl => $"https://spoonacular.com/cdn/ingredients_500x500/{image}";

        public int id { get; set; }
        public string original { get; set; }
        public string originalName { get; set; }
        public string name { get; set; }
        public double amount { get; set; }
        public string unit { get; set; }
        public string unitShort { get; set; }
        public string unitLong { get; set; }
        public string[] possibleUnits { get; set; }
        public EstimatedCost estimatedCost { get; set; }
        public string consistency { get; set; }
        public string[] shoppingListUnits { get; set; }
        public string aisle { get; set; }
        public string image { get; set; }
        public string[] meta { get; set; }
        public Nutrition nutrition { get; set; }
    }

    public class EstimatedCost
    {
        public double value { get; set; }
        public string unit { get; set; }
    }

    public class Nutrient
    {
        public string title { get; set; }
        public double amount { get; set; }
        public string unit { get; set; }
        public double percentOfDailyNeeds { get; set; }
    }

    public class CaloricBreakdown
    {
        public double percentProtein { get; set; }
        public double percentFat { get; set; }
        public double percentCarbs { get; set; }
    }

    public class WeightPerServing
    {
        public int amount { get; set; }
        public string unit { get; set; }
    }

    public class Nutrition
    {
        public Nutrient[] nutrients { get; set; }
        public CaloricBreakdown caloricBreakdown { get; set; }
        public WeightPerServing weightPerServing { get; set; }
    }
}