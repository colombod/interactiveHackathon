namespace BarcodeScanner.Spoonacular
{
    public class ClassifyResponse
    {
        public string Status { get; set; }
        public string Category { get; set; }
        public double Probability { get; set; }
    }
}
