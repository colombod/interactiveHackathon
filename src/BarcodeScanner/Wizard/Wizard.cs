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
        NotStarted,
        InProgress,
        Completed,
        Cancelled
    }

    public class Wizard<T>
    {
        private readonly Font _font = SystemFonts.Collection.Find("FreeMono").CreateFont(16);
        private readonly IWizardStep<T>[] _steps;
        private int _currentStep;
        public WizardState CurrentState { get; private set; } = WizardState.NotStarted;
        private readonly Display _display;
        private T _data;

        public Wizard(PiTopModule module, IWizardStep<T>[] steps)
        {
            _steps = steps;
            _display = module.Display;

            module.UpButton.PressedChanged += (sender, pressed) => {
                if (pressed)
                {
                    steps[_currentStep].Up();
                }
            };

            module.DownButton.PressedChanged += (sender, pressed) => {
                if (pressed)
                {
                    steps[_currentStep].Down();
                }
            };

            module.SelectButton.PressedChanged += (sender, pressed) => {
                steps[_currentStep].Confirm(_data);
                var nextStep = _currentStep + 1;
                if (nextStep < steps.Length)
                {
                    _currentStep = nextStep;
                    steps[_currentStep].Initialize(_display, _font);
                }
                else
                {
                    _display.Draw((context, cr) => {
                        context.Clear(Color.Black);
                        var rect = TextMeasurer.Measure("Done!", new RendererOptions(_font));
                        var x = 1;
                        var y = 1;
                        context.DrawText("Done!", _font, Color.Aqua, new PointF(x, y));
                    });
                    CurrentState = WizardState.Completed;
                }
            };

            module.CancelButton.PressedChanged += (sender, pressed) => {
                _display.Draw((context, cr) => {
                    context.Clear(Color.Black);
                    var rect = TextMeasurer.Measure("Cancelled.", new RendererOptions(_font));
                    var x = 1;
                    var y = 1;
                    context.DrawText("Cancelled.", _font, Color.Aqua, new PointF(x, y));
                });
                CurrentState = WizardState.Cancelled;
            };
        }

        public void Start(T data)
        {
            _data = data;
            CurrentState = WizardState.InProgress;
            _currentStep = 0;
            _display.Clear();
            _steps[_currentStep].Initialize(_display, _font);
        }

        public void Reset()
        {
            CurrentState = WizardState.NotStarted;
            _currentStep = 0;
            _display.Clear();
        }

        public static IWizardStep<T> CreateStep(string initialPrompt, Action<T> confirm, Func<string> up, Func<string> down)
        {
            return new AnonymousWizardStep(initialPrompt, confirm, up, down);
        }

        private class AnonymousWizardStep : IWizardStep<T>
        {
            private readonly Action<T> _confirm;
            private readonly Func<string> _up;
            private readonly Func<string> _down;
            private Display _display;
            private Font _font;
            private readonly string _initialPrompt;

            internal AnonymousWizardStep(string initialPrompt, Action<T> confirm, Func<string> up, Func<string> down)
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

            public void Confirm(T data) => _confirm(data);

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
                    var x = 1;
                    var y = 1;
                    context.DrawText(newPrompt, _font, Color.Aqua, new PointF(x, y));
                });
            }
        }
    }
}
