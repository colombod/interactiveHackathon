namespace BarcodeScanner.Spoonacular
{
    public class FoodData
    {
        public Classification classification { get; set; }
        public string creationDate { get; set; }
        public string expirationDate { get; set; }
    }

    public class Classification
    {
        public string category { get; set; }
        public double probability { get; set; }
    }
}
