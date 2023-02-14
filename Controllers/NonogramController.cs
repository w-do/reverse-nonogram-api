using Microsoft.AspNetCore.Mvc;
using ReverseNonogramApi.Models;
using ReverseNonogramApi.Services;

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
        return Ok(new Nonogram(JaggedToMultiDimensional(array)));
    }

    [HttpPost("image")]
    public IActionResult CreateNonogramFromImage(IFormFile file)
    {
        return Ok(new Nonogram(_imageParser.ParseImageFile(file)));
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
                throw new Exception("Error: submitted array is malformed");
            }

            for (var j = 0; j < columns; j++)
            {
                grid[i, j] = jagged[i][j];
            }
        }

        return grid;
    }
}
