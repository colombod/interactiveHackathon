namespace BarcodeScanner.Spoonacular
{
    public class ClassifyResponse
    {
        public string status { get; set; }
        public string category { get; set; }
        public double probability { get; set; }
    }
}
