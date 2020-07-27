using System;

using FluentAssertions;

using Xunit;

namespace BarcodeScanner.Tests
{
    public class DecoderTest
    {
        [Fact]
        public void can_decode_a_bitmap()
        {
            var image = ImageHelper.LoadImage("code_000.jpg");
            var result = image.Decode();
            result.Text.Should().Be("712345678911");
        }
    }
}
