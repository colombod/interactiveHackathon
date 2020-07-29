namespace BarcodeScanner.Spoonacular
{
    public class FoodData
    {
        public Classification Classification { get; set; }
        public string CreationDate { get; set; }
        public string ExpirationDate { get; set; }
    }

    public class Classification
    {
        public bool Succeeded { get; set; }
        public string Category { get; set; }
        public double Probability { get; set; }
    }
}
