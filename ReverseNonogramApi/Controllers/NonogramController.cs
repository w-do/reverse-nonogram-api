using Microsoft.AspNetCore.Mvc;
using ReverseNonogramApi.Exceptions;
using ReverseNonogramApi.Models;
using ReverseNonogramApi.Services;
using SixLabors.ImageSharp;

namespace ReverseNonogramApi.Controllers;

[ApiController]
[Route("[controller]")]
public class NonogramController : ControllerBase
{
    private readonly ILogger<NonogramController> _logger;
    private readonly IImageParsingService _imageParser;

    public NonogramController(ILogger<NonogramController> logger, IImageParsingService imageParser)
    {
        _logger = logger;
        _imageParser = imageParser;
    }

    [HttpPost]
    public IActionResult CreateNonogram(int[][] array)
    {
        try
        {
            return Ok(new Nonogram(JaggedToMultiDimensional(array)));
        }
        catch (InvalidArrayException exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpPost("image")]
    public IActionResult CreateNonogramFromImage(IFormFile file)
    {
        try
        {
            return Ok(new Nonogram(_imageParser.ParseImageFile(file.OpenReadStream())));
        }
        catch (InvalidImageException exception)
        {
            return BadRequest(exception.Message);
        }
        catch (UnknownImageFormatException)
        {
            return new UnsupportedMediaTypeResult();
        }
    }

    private int[,] JaggedToMultiDimensional(int[][] jagged)
    {
        var rows = jagged.Length;
        var columns = jagged[0].Length;
        var grid = new int[rows, columns];

        for (var i = 0; i < rows; i++)
        {
            if (jagged[i].Length != columns)
            {
                throw new InvalidArrayException("Invalid array: must be a uniform 2-dimensional array");
            }

            for (var j = 0; j < columns; j++)
            {
                grid[i, j] = jagged[i][j];
            }
        }

        return grid;
    }
}
