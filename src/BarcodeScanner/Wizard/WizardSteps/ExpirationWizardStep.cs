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
            // assign field
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

            _display.Draw((context, cr) => {
                context.Clear(Color.Black);
                var rect = TextMeasurer.Measure(text, new RendererOptions(_font));
                var x = (cr.Width - rect.Width) / 2;
                var y = (cr.Height + rect.Height) / 2;
                context.DrawText(text, _font, Color.Aqua, new PointF(x, y));
            });
        }
    }
}
