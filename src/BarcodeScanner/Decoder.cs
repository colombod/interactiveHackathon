using System.Drawing;
using OpenCvSharp;
using ZXing;
using ZXing.Common;

namespace BarcodeScanner
{

    public static class Decoder
    {
        public static Result Decode(this Bitmap source)
        {
            
            var reader = new BarcodeReader
            {
                AutoRotate = true,
                TryInverted = true,
                Options = new DecodingOptions { TryHarder = true }
            };
            var result = reader.Decode(new BitmapLuminanceSource(source));
            return result;
        }

        public static Result Decode(this SixLabors.ImageSharp.Image source)
        {

            var reader = new BarcodeReader
            {
                AutoRotate = true,
                TryInverted = true,
                Options = new DecodingOptions { TryHarder = true }
            };
            var result = reader.Decode(new ImageLuminanceSource(source));
            return result;
        }

        public static Result Decode(this Mat source)
        {
            var reader = new BarcodeReader
            {
                AutoRotate = true,
                TryInverted = true,
                Options = new DecodingOptions { TryHarder = true }
            };
            var result = reader.Decode(new MatLuminanceSource(source));
            return result;
        }
    }
}
