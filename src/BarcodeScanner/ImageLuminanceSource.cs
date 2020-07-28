using System;
using System.Runtime.InteropServices;
using SixLabors.ImageSharp.PixelFormats;
using ZXing;

namespace BarcodeScanner
{
    internal class ImageLuminanceSource : BaseLuminanceSource
    {
        protected ImageLuminanceSource(int width, int height)
            : base(width, height)
        {
        }

        public ImageLuminanceSource(SixLabors.ImageSharp.Image image)
            : base(image.Width, image.Height)
        {
            CalculateLuminanceValues(image, luminances);
        }

        private void CalculateLuminanceValues(SixLabors.ImageSharp.Image image, byte[] bytes)
        {
            var luminance = image.CloneAs<L8>();
            for (int i = 0;  i < luminance.Height; i++)
            {
                var row = luminance.GetPixelRowSpan(i);
                
                byte[] rawBytes = MemoryMarshal.AsBytes(row).ToArray();
                var offset = i * (rawBytes.Length);
                Buffer.BlockCopy(rawBytes,0,bytes,offset, rawBytes.Length);
            }
        }

        protected override LuminanceSource CreateLuminanceSource(byte[] newLuminances, int width, int height)
        {
            return new ImageLuminanceSource(width, height) { luminances = newLuminances };
        }
    }
}