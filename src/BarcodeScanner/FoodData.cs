using System;

namespace BarcodeScanner
{
    public class FoodData
    {
        public Classification Classification { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }

    public class Classification
    {
        public bool Succeeded { get; set; }
        public string Category { get; set; }
        public double Probability { get; set; }
    }
}
