using System.Linq;
using PiTop;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;

namespace BarcodeScanner
{
    public enum WizardState
    {
        InProgress,
        Completed,
        Cancelled
    }

    public class Wizard
    {
        private readonly IWizardStep[] _steps;
        private int _currentStep;
        public WizardState CurrentState { get; private set; }

        public Wizard(PiTopModule module, IWizardStep[] steps)
        {
            _steps = steps;
            // var font = new Font("Roboto", 12));
            var family = SystemFonts.Families.First();
            var font = family.CreateFont(12);

            module.UpButton.PressedChanged += (sender, pressed) => steps[_currentStep].Up();

            module.DownButton.PressedChanged += (sender, pressed) => steps[_currentStep].Down();

            module.SelectButton.PressedChanged += (sender, pressed) => {
                steps[_currentStep].Confirm();
                var nextStep = _currentStep + 1;
                if (nextStep < steps.Length)
                {
                    _currentStep = nextStep;
                    steps[_currentStep].Initialize(module.Display, font);
                }
                else
                {
                    module.Display.Draw((context) => {
                        context.Clear(Color.Black);
                        var rect = TextMeasurer.Measure("Diego was here", new RendererOptions(font));
                        var x = (module.Display.Width - rect.Width) / 2;
                        var y = (module.Display.Height + rect.Height) / 2;
                        context.DrawText("Diego was here", font, Color.Aqua, new PointF(x, y));
                    });
                    CurrentState = WizardState.Completed;
                }
            };

            module.CancelButton.PressedChanged += (sender, pressed) => {
                module.Display.Draw((context) => {
                    context.Clear(Color.Black);
                    var rect = TextMeasurer.Measure("Diego was here", new RendererOptions(font));
                    var x = (module.Display.Width - rect.Width) / 2;
                    var y = (module.Display.Height + rect.Height) / 2;
                    context.DrawText("Diego was here", font, Color.Aqua, new PointF(x, y));
                });
                CurrentState = WizardState.Cancelled;
            };

            CurrentState = WizardState.InProgress;
            steps[_currentStep].Initialize(module.Display, font);
        }
    }
}
