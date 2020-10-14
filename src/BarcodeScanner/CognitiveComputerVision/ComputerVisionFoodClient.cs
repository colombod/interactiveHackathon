using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;

using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BarcodeScanner.CognitiveComputerVision
{
    public class ComputerVisionFoodClient
    {
        private readonly ComputerVisionClient _client;
        public ComputerVisionFoodClient(string key, string endpoint)
        {
            _client = new ComputerVisionClient(new ApiKeyServiceClientCredentials(key))
            {
                Endpoint = endpoint
            };
        }

        public async Task<ImageAnalysis> Classify(string fileName)
        {
            var features = new List<VisualFeatureTypes?>()
                {
                    VisualFeatureTypes.Categories,
                    VisualFeatureTypes.Description,
                    VisualFeatureTypes.Faces,
                    VisualFeatureTypes.ImageType,
                    VisualFeatureTypes.Tags,
                    VisualFeatureTypes.Adult,
                    VisualFeatureTypes.Color,
                    VisualFeatureTypes.Brands,
                    VisualFeatureTypes.Objects
                };

            // Analyze the URL image
            using Stream analyzeImageStream = File.OpenRead(fileName);
            // Analyze the local image
            ImageAnalysis result = await _client.AnalyzeImageInStreamAsync(analyzeImageStream, features);
            return result;
        }
    }
}