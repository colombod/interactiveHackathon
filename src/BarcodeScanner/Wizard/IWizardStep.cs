using PiTop.Abstractions;
using SixLabors.Fonts;

namespace BarcodeScanner
{
    public interface IWizardStep
    {
        void Initialize(Display display, Font font);
        void Confirm();
        void Up();
        void Down();
    }
}
