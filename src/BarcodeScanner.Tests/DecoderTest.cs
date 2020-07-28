using System;

using FluentAssertions;

using Xunit;

namespace BarcodeScanner.Tests
{
    public class DecoderTest
    {
        [Theory]
        [InlineData("code_000.jpg", "712345678911")]
        [InlineData("code_001.jpg", "753469010065")]
        [InlineData("apples.png", "011110181770")]
        [InlineData("bananas.png", "643126071631")]
        [InlineData("ghirardelli.png", "747599414190")]
        [InlineData("oranges.png", "840437100009")]
        public void can_decode_a_bitmap(string imageSample, string expectedCode)
        {
            var image = ImageHelper.LoadBitmap(imageSample);
            var result = image.Decode();
            result.Text.Should().Be(expectedCode);
            
        }

        [Theory]
        [InlineData("code_000.jpg", "712345678911")]
        [InlineData("code_001.jpg", "753469010065")]
        [InlineData("apples.png", "011110181770")]
        [InlineData("bananas.png", "643126071631")]
        [InlineData("ghirardelli.png", "747599414190")]
        [InlineData("oranges.png", "840437100009")]
        public void can_decode_an_image(string imageSample, string expectedCode)
        {
            var image = ImageHelper.LoadImage(imageSample);
            var result = image.Decode();
            result.Text.Should().Be(expectedCode);

        }
    }
}
