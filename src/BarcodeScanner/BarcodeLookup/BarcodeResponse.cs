namespace BarcodeScanner.BarcodeLookup
{
    public class BarcodeResponse
    {
        public Product[] products { get; set; }
    }

    public class Store
    {
        public string store_name { get; set; }
        public string store_price { get; set; }
        public string product_url { get; set; }
        public string currency_code { get; set; }
        public string currency_symbol { get; set; }
    }

    public class Review
    {
        public string name { get; set; }
        public string rating { get; set; }
        public string title { get; set; }
        public string review { get; set; }
        public string datetime { get; set; }
    }

    public class Product
    {
        public string barcode_number { get; set; }
        public string barcode_type { get; set; }
        public string barcode_formats { get; set; }
        public string mpn { get; set; }
        public string model { get; set; }
        public string asin { get; set; }
        public string product_name { get; set; }
        public string title { get; set; }
        public string category { get; set; }
        public string manufacturer { get; set; }
        public string brand { get; set; }
        public string label { get; set; }
        public string author { get; set; }
        public string publisher { get; set; }
        public string artist { get; set; }
        public string actor { get; set; }
        public string director { get; set; }
        public string studio { get; set; }
        public string genre { get; set; }
        public string audience_rating { get; set; }
        public string ingredients { get; set; }
        public string nutrition_facts { get; set; }
        public string color { get; set; }
        public string format { get; set; }
        public string package_quantity { get; set; }
        public string size { get; set; }
        public string length { get; set; }
        public string width { get; set; }
        public string height { get; set; }
        public string weight { get; set; }
        public string release_date { get; set; }
        public string description { get; set; }
        public string[] features { get; set; }
        public string[] images { get; set; }
        public Store[] stores { get; set; }
        public Review[] reviews { get; set; }
    }
}
