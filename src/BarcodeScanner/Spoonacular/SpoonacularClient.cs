using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
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

        public async Task<ClassifyResponse> Classify(string filePath)
        {
            var fileName = Path.GetFileName(filePath);
            using var stream = new FileStream(filePath, FileMode.Open);
            HttpContent content = new StreamContent(stream);

            content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = "file",
                FileName = fileName
            };

            MultipartFormDataContent form = new MultipartFormDataContent
            {
                { content, fileName }
            };

            var response = await _client.PostAsync($"food/images/classify?apiKey={_apiKey}", form);
            var contentJson = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ClassifyResponse>(contentJson);
        }
    }
}
