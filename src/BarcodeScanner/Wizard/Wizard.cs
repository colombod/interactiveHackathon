using System;
using PiTop;
using PiTop.Abstractions;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;

namespace BarcodeScanner
{
    public class Ingredient {
        
    }
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
            var font =  SystemFonts.Collection.Find("FreeMono").CreateFont(16);

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
                    module.Display.Draw((context, cr) => {
                        context.Clear(Color.Black);
                        var rect = TextMeasurer.Measure("Diego was here", new RendererOptions(font));
                        var x = (cr.Width - rect.Width) / 2;
                        var y = (cr.Height + rect.Height) / 2;
                        context.DrawText("Diego was here", font, Color.Aqua, new PointF(x, y));
                    });
                    CurrentState = WizardState.Completed;
                }
            };

            module.CancelButton.PressedChanged += (sender, pressed) => {
                module.Display.Draw((context, cr) => {
                    context.Clear(Color.Black);
                    var rect = TextMeasurer.Measure("Diego was here", new RendererOptions(font));
                    var x = (cr.Width - rect.Width) / 2;
                    var y = (cr.Height + rect.Height) / 2;
                    context.DrawText("Diego was here", font, Color.Aqua, new PointF(x, y));
                });
                CurrentState = WizardState.Cancelled;
            };

            CurrentState = WizardState.InProgress;
            steps[_currentStep].Initialize(module.Display, font);
        }

        public static IWizardStep CreateStep(string initialPrompt, Action confirm, Func<string> up, Func<string> down)
        {
            return new AnonymousWizardStep(initialPrompt, confirm, up, down);
        }

        private class AnonymousWizardStep : IWizardStep
        {
            private Action _confirm;
            private Func<string> _up;
            private Func<string> _down;
            private Display _display;
            private Font _font;
            private string _initialPrompt;

            internal AnonymousWizardStep(string initialPrompt, Action confirm, Func<string> up, Func<string> down)
            {
                _confirm = confirm;
                _up = up;
                _down = down;
                _initialPrompt = initialPrompt;
            }
            public void Initialize(Display display, Font font)
            {
                _display = display;
                _font = font;
                DisplayNewPrompt(_initialPrompt);
            }

            public void Confirm() => _confirm();

            public void Up()
            {
                string newPrompt = _up();
                DisplayNewPrompt(newPrompt);
            }

            public void Down()
            {
                string newPrompt = _down();
                DisplayNewPrompt(newPrompt);
            }

            private void DisplayNewPrompt(string newPrompt)
            {
                _display.Draw((context, cr) => {
                    context.Clear(Color.Black);
                    var rect = TextMeasurer.Measure(newPrompt, new RendererOptions(_font));
                    var x = (cr.Width - rect.Width) / 2;
                    var y = (cr.Height + rect.Height) / 2;
                    context.DrawText(newPrompt, _font, Color.Aqua, new PointF(x, y));
                });
            }
        }
    }
}
