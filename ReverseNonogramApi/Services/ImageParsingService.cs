using ReverseNonogramApi.Exceptions;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ReverseNonogramApi.Services;

public class ImageParsingService : IImageParsingService
{
    public int[,] ParseImage(Stream imageStream)
    {
        var image = Image.Load(imageStream).CloneAs<Rgb24>();

        if (image.Height > 20 || image.Width > 20)
        {
            throw new InvalidImageException("Image must be 20 x 20 pixels or smaller");
        }

        var grid = new int[image.Height, image.Width];

        for (var i = 0; i < image.Width; i++)
        {
            for (var j = 0; j < image.Height; j++)
            {
                var pixel = image[i, j];

                if (IsBlack(pixel))
                {
                    grid[j, i] = 1;
                }
                else if (IsWhite(pixel))
                {
                    grid[j, i] = 0;
                }
                else
                {
                    throw new InvalidImageException("Image must be black and white only");
                }
            }
        }

        return grid;
    }

    private bool IsBlack(Rgb24 pixel)
    {
        return pixel.R == 0 && pixel.G == 0 && pixel.B == 0;
    }

    private bool IsWhite(Rgb24 pixel)
    {
        return pixel.R == 255 && pixel.G == 255 && pixel.B == 255;
    }
}
