using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BarcodeScanner.BarcodeLookup
{
    public class BarcodeLookupClient
    {
        private readonly string _apiKey;
        private readonly HttpClient _client;

        public BarcodeLookupClient(string apiKey)
        {
            _apiKey = apiKey;
            _client = new HttpClient
            {
                BaseAddress = new Uri("https://api.barcodelookup.com/v2/")
            };
        }

        public async Task<BarcodeResponse> GetProductAsync(string barcode)
        {
            var response = await _client.GetAsync($"products?barcode={barcode}&formatted=y&key={_apiKey}");
            string jsonResponseBody = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<BarcodeResponse>(jsonResponseBody);
        }
    }
}
