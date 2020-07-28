
public static class Classify
{
    public static int GetPerishibilityInDays(string aisleName) =>
        aisleName switch
        {
            "Baking" => 365,
            "Health Foods" => 60,
            "Spices and Seasonings" => 365,
            "Pasta and Rice" => 365,
            "Bakery/Bread" => 7,
            "Refrigerated" => 21,
            "Canned and Jarred" => 365,
            "Frozen" => 180,
            "Nut butters, Jams, and Honey" => 180,
            "Oil, Vinegar, Salad Dressing" => 90,
            "Condiments" => 180,
            "Savory Snacks" => 90,
            "Milk, Eggs, Other Dairy" => 21,
            "Ethnic Foods" => 21,
            "Tea and Coffee" => 180,
            "Meat" => 7,
            "Gourmet" => 30,
            "Sweet Snacks" => 365,
            "Gluten Free" => 60,
            "Alcoholic Beverages" => 800,
            "Cereal" => 180,
            "Nuts" => 180,
            "Beverages" => 365,
            "Produce" => 7,
            "Not in Grocery Store/Homemade" => 7,
            "Seafood" => 5,
            "Cheese" => 21,
            "Dried Fruits" => 90,
            "Online" => 365,
            "Grilling Supplies" => 365,
            "Bread" => 7,
            _ => 2
        };



}