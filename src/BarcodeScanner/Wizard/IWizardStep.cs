using PiTop.Abstractions;
using SixLabors.Fonts;

namespace BarcodeScanner
{
    public interface IWizardStep<T>
    {
        void Initialize(Display display, Font font);
        void Confirm(T data);
        void Up();
        void Down();
    }
}
