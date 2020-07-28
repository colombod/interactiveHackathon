using System.Drawing;

namespace BarcodeScanner.Tests
{
    internal static class ImageHelper
    {
        public static Bitmap LoadBitmap(string imageFileName)
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

        public static SixLabors.ImageSharp.Image LoadImage(string imageFileName)
        {
            var assembly = typeof(ImageHelper).Assembly;
            using (var resourceStream = assembly.GetManifestResourceStream($"{typeof(ImageHelper).Namespace}.Images.{imageFileName}"))
            {
                if (resourceStream != null)
                {
                    return SixLabors.ImageSharp.Image.Load(resourceStream);
                }

                return null;

            }
        }
    }
}
