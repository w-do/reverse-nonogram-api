namespace ReverseNonogramApi.Models;

public class Nonogram
{
    private int[,] Grid;

    public int Width { get; set; }
    public int Height { get; set; }
    public int[][] TopClues { get; set; }
    public int[][] LeftClues { get; set; }

    public Nonogram(int[,] grid)
    {
        if (grid is null || grid.GetLength(0) > 20 || grid.GetLength(1) > 20)
        {
            throw new Exception("Grid must be 20 x 20 or smaller");
        }

        Grid = grid;
        Width = grid.GetLength(1);
        Height = grid.GetLength(0);

        var topClues = new List<int[]>();
        var leftClues = new List<int[]>();

        for (var i = 0; i < Width; i++)
        {
            var consecutive = 0;
            var line = new List<int>();

            for (var j = 0; j < Height; j++)
            {
                if (Grid[j, i] == 1)
                {
                    consecutive++;
                }
                else if (consecutive != 0)
                {
                    line.Add(consecutive);
                    consecutive = 0;
                }
            }

            if (consecutive != 0)
            {
                line.Add(consecutive);
            }

            topClues.Add(line.ToArray());
        }

        for (var j = 0; j < Height; j++)
        {
            var consecutive = 0;
            var line = new List<int>();

            for (var i = 0; i < Width; i++)
            {
                if (Grid[j, i] == 1)
                {
                    consecutive++;
                }
                else if (consecutive != 0)
                {
                    line.Add(consecutive);
                    consecutive = 0;
                }
            }

            if (consecutive != 0)
            {
                line.Add(consecutive);
            }

            leftClues.Add(line.ToArray());
        }

        TopClues = topClues.ToArray();
        LeftClues = leftClues.ToArray();
    }
}
