using ReverseNonogramApi.Models;

namespace ReverseNonogramApi.Tests.Models;

public class NonogramTests
{
    [Fact]
    public void Constructor_ThrowsException_WhenPassedNullArray()
    {
        var exception = Assert.Throws<Exception>(() => new Nonogram(null));

        Assert.Equal("Invalid array given - must be 20 x 20 or smaller", exception.Message);
    }

    [Theory]
    [InlineData(21, 1)]
    [InlineData(1, 21)]
    [InlineData(21, 21)]
    public void Constructor_ThrowsException_WhenPassedArrayExceeding20By20(int width, int height)
    {
        var grid = new int[height, width];

        var exception = Assert.Throws<Exception>(() => new Nonogram(grid));

        Assert.Equal("Invalid array given - must be 20 x 20 or smaller", exception.Message);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(2)]
    [InlineData(100)]
    public void Constructor_ThrowsException_WhenPassedArrayWithAnyValueOtherThanZeroOrOne(int value)
    {
        var grid = new int[20, 20];

        for (var i = 0; i < 20; i++)
        {
            for (var j = 0; j < 20; j++)
            {
                grid[i, j] = 0;
            }
        }

        grid[19, 19] = value;

        var exception = Assert.Throws<Exception>(() => new Nonogram(grid));

        Assert.Equal("Invalid array given - values must be 0 and 1 only", exception.Message);
    }

    [Theory]
    [InlineData(1, 1)]
    [InlineData(5, 10)]
    [InlineData(10, 5)]
    [InlineData(20, 1)]
    [InlineData(1, 20)]
    [InlineData(20, 20)]
    public void Constructor_AssignsDimensionsCorrectly_GivenValidData(int width, int height)
    {
        var grid = new int[height, width];

        for (var i = 0; i < width; i++)
        {
            for (var j = 0; j < height; j++)
            {
                grid[j, i] = 0;
            }
        }

        var nonogram = new Nonogram(grid);

        Assert.Equal(width, nonogram.Width);
        Assert.Equal(height, nonogram.Height);
    }

    [Fact]
    public void Constructor_AssignsCluesCorrectly_GivenValidData()
    {
        var grid = new int[3, 5]
        {
            { 1, 0, 1, 0, 1 },
            { 1, 1, 1, 0, 0 },
            { 1, 0, 0, 0, 1 }
        };

        var nonogram = new Nonogram(grid);

        Assert.Equal(5, nonogram.TopClues.Length);
        Assert.Equal(3, nonogram.LeftClues.Length);

        Assert.Single(nonogram.TopClues[0]);
        Assert.Single(nonogram.TopClues[1]);
        Assert.Single(nonogram.TopClues[2]);
        Assert.Empty(nonogram.TopClues[3]);
        Assert.Equal(2, nonogram.TopClues[4].Length);
        Assert.Equal(3, nonogram.LeftClues[0].Length);
        Assert.Single(nonogram.LeftClues[1]);
        Assert.Equal(2, nonogram.LeftClues[2].Length);

        Assert.Equal(3, nonogram.TopClues[0][0]);
        Assert.Equal(1, nonogram.TopClues[1][0]);
        Assert.Equal(2, nonogram.TopClues[2][0]);
        Assert.Equal(1, nonogram.TopClues[4][0]);
        Assert.Equal(1, nonogram.TopClues[4][1]);
        Assert.Equal(1, nonogram.LeftClues[0][0]);
        Assert.Equal(1, nonogram.LeftClues[0][1]);
        Assert.Equal(1, nonogram.LeftClues[0][2]);
        Assert.Equal(3, nonogram.LeftClues[1][0]);
        Assert.Equal(1, nonogram.LeftClues[2][0]);
        Assert.Equal(1, nonogram.LeftClues[2][1]);
    }
}

