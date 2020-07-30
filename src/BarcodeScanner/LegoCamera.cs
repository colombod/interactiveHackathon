using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using PiTopMakerArchitecture.Foundation.Sensors;

namespace BarcodeScanner
{
    public class LegoCamera : IDisposable

    {
        private readonly Button _trigger;
        private readonly DirectoryInfo _storage;
        private readonly HashSet<string> _imageTypes;
        private int _currentImage;
        private FileInfo[] _images;

        public LegoCamera(Button trigger, DirectoryInfo storage)
        {
            _trigger = trigger;
            _storage = storage;
            _trigger.Pressed += triggerOnPressed;
            _imageTypes = new HashSet<string>
            {

                "bmp",
                "gif",
                "jpg",
                "jpeg",
                "png",
                "tiff"
            };
        }

        private void triggerOnPressed(object? o, EventArgs eventArgs)
        {

            if (_images.Length > 0)
            {
                var image = _images[_currentImage];
                File.SetLastWriteTimeUtc(image.FullName, DateTime.UtcNow);
                _currentImage = (_currentImage + 1) % _images.Length;
            }

        }

        public void Reload()
        {
            _currentImage = 0;
            _images = _storage.GetFiles().Where(f => _imageTypes.Contains(f.Extension.ToLower())).ToArray();
        }

        public void Dispose()
        {
            _trigger.Pressed -= triggerOnPressed;
        }
    }
}