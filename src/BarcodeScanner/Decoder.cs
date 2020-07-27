using System.Drawing;
using OpenCvSharp;
using ZXing;

namespace BarcodeScanner
{
    public static class Decoder
    {
        public static Result Decode(this Bitmap source)
        {
            var reader = new BarcodeReader();
            var result = reader.Decode(new BitmapLuminanceSource(source));
            return result;
        }

        public static Result Decode(this Mat source)
        {
            var reader = new BarcodeReader();
            var result = reader.Decode(new MatLuminanceSource(source));
            return result;
        }
    }
}
