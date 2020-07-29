using System;
using PiTop.Abstractions;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;

namespace BarcodeScanner
{
    public class ExpirationWizardStep : IWizardStep
    {
        private DateTime _expirationDate = DateTime.Now;
        private Display _display;
        private Font _font;

        public void Confirm()
        {
            // idk
        }

        public void Down()
        {
            _expirationDate = _expirationDate.AddDays(-1);
            RenderDateTime();
        }

        public void Initialize(Display display, Font font)
        {
            _display = display;
            _font = font;
            display.Clear();
            RenderDateTime();
        }

        public void Up()
        {
            _expirationDate = _expirationDate.AddDays(1);
            RenderDateTime();
        }

        private void RenderDateTime()
        {
            var text = $"Expiration:\n{_expirationDate.Date.ToShortDateString()}";

            _display.Draw((context) => {
                context.Clear(Color.Black);
                var rect = TextMeasurer.Measure(text, new RendererOptions(_font));
                var x = (_display.Width - rect.Width) / 2;
                var y = (_display.Height + rect.Height) / 2;
                context.DrawText(text, _font, Color.Aqua, new PointF(x, y));
            });
        }
    }
}
