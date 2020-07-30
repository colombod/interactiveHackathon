using System;
using System.Collections.Generic;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using BarcodeScanner.Spoonacular;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Linq;

namespace BarcodeScanner.CognitiveComputerVision
{
    public class ComputerVisionFoodClient
    {
        private ComputerVisionClient client;
        public ComputerVisionFoodClient(string key, string endpoint)
        {
            client = new ComputerVisionClient(new ApiKeyServiceClientCredentials(key))
            { Endpoint = endpoint };
        }

        public async Task<ImageAnalysis> Classify(string fileName, string writePath = null)
        {
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("ANALYZE IMAGE - URL");
            Console.WriteLine();
            try
            {
                List<VisualFeatureTypes> features = new List<VisualFeatureTypes>()
                {
                VisualFeatureTypes.Categories, VisualFeatureTypes.Description,
                VisualFeatureTypes.Faces, VisualFeatureTypes.ImageType,
                VisualFeatureTypes.Tags, VisualFeatureTypes.Adult,
                VisualFeatureTypes.Color, VisualFeatureTypes.Brands,
                VisualFeatureTypes.Objects
                };
                Console.WriteLine($"Analyzing the image {Path.GetFileName(fileName)}...");
                Console.WriteLine();
                // Analyze the URL image 
                using (Stream analyzeImageStream = File.OpenRead(fileName))
                {
                    // Analyze the local image.
                    ImageAnalysis results = await client.AnalyzeImageInStreamAsync(analyzeImageStream, features);
                    var foodResult = results.Tags.Where(t => t.Hint == "food").FirstOrDefault() ?? results.Tags.FirstOrDefault();
                    if (!string.IsNullOrWhiteSpace(writePath))
                    {
                        Directory.CreateDirectory(writePath);
                        var jsonFilePath =
                            Path.Combine(writePath, $"{Path.GetFileNameWithoutExtension(fileName)}.json");
                        var foodData = new FoodData
                        {
                            Classification = new Classification
                            {
                                Succeeded = true,
                                Category = foodResult.Name,
                                Probability = foodResult.Confidence
                            },
                            CreationDate = DateTime.UtcNow,
                            ExpirationDate = null
                        };

                        File.WriteAllText(jsonFilePath, JsonConvert.SerializeObject(foodData));
                    }

                    return results;
                }


            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw e;
            }
        }
    }
}