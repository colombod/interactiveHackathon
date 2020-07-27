using System.Drawing;
using System.Drawing.Imaging;
using ZXing;

namespace BarcodeScanner
{
    internal class BitmapLuminanceSource : BaseLuminanceSource
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BitmapLuminanceSource"/> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        protected BitmapLuminanceSource(int width, int height)
            : base(width, height)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BitmapLuminanceSource"/> class.
        /// </summary>
        /// <param name="bitmap">The bitmap.</param>
        public BitmapLuminanceSource(Bitmap bitmap)
            : base(bitmap.Width, bitmap.Height)
        {
            switch (bitmap.PixelFormat)
            {
                case PixelFormat.Format24bppRgb:
                    CalculateLuminanceRgb(bitmap, 3);
                    break;
                case PixelFormat.Format32bppRgb:
                    CalculateLuminanceRgb(bitmap, 4);
                    break;
              
                case PixelFormat.Format32bppArgb:
                    CalculateLuminanceArgb(bitmap);
                    break;
                case PixelFormat.Format64bppArgb:
                    break;
                default:
                    // there is no special conversion routine to luminance values
                    // we have to convert the image to a supported format
                    bitmap = ConvertImage(bitmap, PixelFormat.Format32bppRgb);
                    CalculateLuminanceRgb(bitmap, 3);
                    break;
            }
         
        }

        private Bitmap ConvertImage(Bitmap bitmap, PixelFormat targetFormat)
        {
            return bitmap.Clone(new Rectangle(0, 0, bitmap.Width, bitmap.Height), targetFormat);
        }

        private void CalculateLuminanceRgb(Bitmap bitmap, int bytesPerPixel)
        {
            var width = bitmap.Width;
            var height = bitmap.Height;

            var data = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly,
                bitmap.PixelFormat);

            var currentRow = data.Scan0;
            var bufferSize = data.Stride;
            var buffer = new byte[bufferSize];
            var luminanceIndex = 0;

            for (var curY = 0; curY < height; curY++)
            {
                System.Runtime.InteropServices.Marshal.Copy(currentRow, buffer, 0, bufferSize);
                
                for (var curX = 0; curX < width; curX += bytesPerPixel)
                {
                    var r = buffer[curX];
                    var g = buffer[curX + 1];
                    var b = buffer[curX + 2];
                    luminances[luminanceIndex] = (byte)((RChannelWeight * r + GChannelWeight * g + BChannelWeight * b) >> ChannelWeight);
                    luminanceIndex++;
                }
                currentRow += data.Stride;
            }

            bitmap.UnlockBits(data);
        }

        private void CalculateLuminanceArgb(Bitmap bitmap)
        {

            var width = bitmap.Width;
            var height = bitmap.Height;

            var data = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly,
                bitmap.PixelFormat);

            var currentRow = data.Scan0;
            var bufferSize = data.Stride;
            var buffer = new byte[bufferSize];
            var luminanceIndex = 0;

            for (var curY = 0; curY < height; curY++)
            {
                System.Runtime.InteropServices.Marshal.Copy(currentRow, buffer, 0, bufferSize);

                for (var curX = 0; curX < width; curX += 4)
                {
                    var alpha = buffer[curX];
                    var r = buffer[curX + 1];
                    var g = buffer[curX + 2];
                    var b = buffer[curX + 3];
                    var luminance = (byte)((RChannelWeight * r + GChannelWeight * g + BChannelWeight * b) >> ChannelWeight);
                    luminance = (byte)(((luminance * alpha) >> 8) + (255 * (255 - alpha) >> 8));
                    luminances[luminanceIndex] = luminance;
                    luminanceIndex++;
                }
                currentRow += data.Stride;
            }

            bitmap.UnlockBits(data);
        }

        /// <summary>
        /// Should create a new luminance source with the right class type.
        /// The method is used in methods crop and rotate.
        /// </summary>
        /// <param name="newLuminances">The new luminances.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns></returns>
        protected override LuminanceSource CreateLuminanceSource(byte[] newLuminances, int width, int height)
        {
            return new BitmapLuminanceSource(width, height) { luminances = newLuminances };
        }
    }
}