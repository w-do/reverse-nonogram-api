using ReverseNonogramApi.Exceptions;
using ReverseNonogramApi.Services;
using SixLabors.ImageSharp;

namespace ReverseNonogramApi.Tests.Services;

public class ImageParsingServiceTests
{
    private readonly ImageParsingService _imageParsingService;

    public ImageParsingServiceTests()
    {
        _imageParsingService = new ImageParsingService();
    }

    [Fact]
    public void ParseImage_ThrowsUnknownImageFormatException_WhenPassedNonImageStream()
    {
        var nonImageBase64 = "dGVzdGRhdGEK";
        var stream = new MemoryStream(Convert.FromBase64String(nonImageBase64));

        var exception = Assert.Throws<UnknownImageFormatException>(() => _imageParsingService.ParseImage(stream));

        Assert.StartsWith("Image cannot be loaded.", exception.Message);
    }

    [Theory]
    [MemberData(nameof(LargeImageBase64))]
    public void ParseImage_ThrowsInvalidImageException_WhenPassedImageExceeding20By20(string base64)
    {
        var imageStream = new MemoryStream(Convert.FromBase64String(base64));

        var exception = Assert.Throws<InvalidImageException>(() => _imageParsingService.ParseImage(imageStream));

        Assert.Equal("Image must be 20 x 20 pixels or smaller", exception.Message);
    }

    [Fact]
    public void ParseImage_ThrowsInvalidImageException_WhenPassedColorImage()
    {
        var colorImageBase64 = "iVBORw0KGgoAAAANSUhEUgAAAAIAAAACAQMAAABIeJ9nAAAABlBMVEUAAAABAACkpdP4AAAADElEQVR4nGNgYHAAAABEAEGXj2jLAAAAAElFTkSuQmCC";
        var imageStream = new MemoryStream(Convert.FromBase64String(colorImageBase64));

        var exception = Assert.Throws<InvalidImageException>(() => _imageParsingService.ParseImage(imageStream));

        Assert.Equal("Image must be black and white only", exception.Message);
    }

    [Fact]
    public void ParseImage_Produces2dArray_GivenValidImage()
    {
        var validImageBase64 = "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABAQMAAAAl21bKAAAAA1BMVEUAAACnej3aAAAACklEQVR4nGNgAAAAAgABSK+kcQAAAABJRU5ErkJggg==";
        var imageStream = new MemoryStream(Convert.FromBase64String(validImageBase64));

        var result = _imageParsingService.ParseImage(imageStream);

        Assert.Equal(1, result.GetLength(0));
        Assert.Equal(1, result.GetLength(1));
        Assert.Equal(1, result[0,0]);
    }

    public static IEnumerable<object[]> LargeImageBase64 =>
        new List<object[]>
        {
            new object[] { "iVBORw0KGgoAAAANSUhEUgAAAAEAAAAVAQMAAAC9nBZHAAAAA1BMVEUAAACnej3aAAAADElEQVR4nGNgIBYAAAAqAAFi4XRDAAAAAElFTkSuQmCC" },
            new object[] { "iVBORw0KGgoAAAANSUhEUgAAABUAAAABAQMAAAALnndYAAAAA1BMVEUAAACnej3aAAAADElEQVR4nGNgYGAAAAAEAAH2FzhVAAAAAElFTkSuQmCC" },
            new object[] { "iVBORw0KGgoAAAANSUhEUgAAABUAAAAVAQMAAACT2TfVAAAAA1BMVEUAAACnej3aAAAADElEQVR4nGNgoD4AAABUAAGT58M4AAAAAElFTkSuQmCC" }
        };
}
