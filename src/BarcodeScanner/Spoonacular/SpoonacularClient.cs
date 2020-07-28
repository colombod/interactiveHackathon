using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BarcodeScanner.Spoonacular
{
    public class SpoonacularClient
    {
        private readonly string _apiKey;
        private readonly HttpClient _client;

        public SpoonacularClient(string apiKey)
        {
            _apiKey = apiKey;
            _client = new HttpClient
            {
                BaseAddress = new Uri("https://api.spoonacular.com/")
            };
        }

        public async Task<IngredientInfo> GetIngredientInfoAsync(Ingredients ingredient)
        {
            var ingredientInformationJson = await _client.GetStringAsync($"food/ingredients/{(int)ingredient}/information?amount=1&apiKey={_apiKey}");
            return JsonConvert.DeserializeObject<IngredientInfo>(ingredientInformationJson);
        }
    }
}
