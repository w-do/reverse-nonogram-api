using Microsoft.AspNetCore.Mvc;
using ReverseNonogramApi.Models;

namespace ReverseNonogramApi.Controllers;

[ApiController]
[Route("[controller]")]
public class NonogramController : ControllerBase
{
    private readonly ILogger<NonogramController> _logger;

    public NonogramController(ILogger<NonogramController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Test()
    {
        return Ok($"current time: {DateTime.UtcNow}");
    }

    [HttpPost]
    public IActionResult CreateNonogram(int[][] array)
    {
        return Ok(new Nonogram(JaggedToMultiDimensional(array)));
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
                throw new Exception("ahhhhhhh");
            }

            for (var j = 0; j < columns; j++)
            {
                grid[i, j] = jagged[i][j];
            }
        }

        return grid;
    }
}
