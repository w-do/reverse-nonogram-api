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

    /// <summary>
    /// Takes a 2d array representing a completed nonogram and returns the clues for the nonogram.
    /// </summary>
    /// <param name="array">An even, 2d array containing only 1s and 0s, maximum size 20 rows and 20 columns.</param>
    /// <response code="200">Returns a nonogram object which includes the clues</response>
    /// <response code="400">If the array is jagged, too large, or contains invalid values</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

    /// <summary>
    /// Takes an image file representing a completed nonogram and returns the clues for the nonogram.
    /// </summary>
    /// <param name="file">An image file whose dimensions are 20x20 or smaller, containing only black and white pixels.</param>
    /// <response code="200">Returns a nonogram object which includes the clues</response>
    /// <response code="400">If the image exceeds maximum dimensions or contains any pixels that are neither black nor white.</response>
    [HttpPost("image")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult CreateNonogramFromImage(IFormFile file)
    {
        try
        {
            return Ok(new Nonogram(_imageParser.ParseImage(file.OpenReadStream())));
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
