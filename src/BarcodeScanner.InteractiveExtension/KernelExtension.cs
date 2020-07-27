using System;
using System.Threading.Tasks;
using Microsoft.DotNet.Interactive;
using Microsoft.DotNet.Interactive.Formatting;
using ZXing;
using ZXing.Common;

namespace BarcodeScanner.InteractiveExtension
{
    public class KernelExtension : IKernelExtension
    {
        public Task OnLoadAsync(Kernel kernel)
        {
            Formatter<Result>.Register((result, writer) =>
            {
                var barcodeWriter = new BarcodeWriterSvg
                {
                    Format = result.BarcodeFormat,
                    Options = new EncodingOptions
                    {
                        Height = 300,
                        Width = 600
                    }
                };

                writer.Write(barcodeWriter.Write(result.Text).Content);

            }, HtmlFormatter.MimeType);

            return Task.CompletedTask;
        }
    }
}