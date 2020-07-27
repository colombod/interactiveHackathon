using System.Drawing;

namespace BarcodeScanner.Tests
{
    internal static class ImageHelper
    {
        public static Bitmap LoadImage(string imageFileName)
        {
            var assembly = typeof(ImageHelper).Assembly;
            using (var resourceStream = assembly.GetManifestResourceStream($"{typeof(ImageHelper).Namespace}.Images.{imageFileName}"))
            {
                if (resourceStream != null)
                {
                    return new Bitmap(resourceStream);

                }

                return null;

            }
        }
    }
}
