using System;
using System.Collections.Generic;

public class Map
{
    public ITile[,] Grid { get; set; }

    public Map(int height, int width)
    {
        Grid = new ITile[height, width];
    }

    public ITile GetTile(int x, int y)
    {
        return Grid[y, x];
    }

    public List<int[]> GetAdjacent(int row, int col)
    {
        var directions = new int[4][]
        {
      new int[] { 1, 0 },
      new int[] { 0, 1 },
      new int[] { -1, 0 },
      new int[] { 0, -1 }
        };

        var adjacent = new List<int[]>();
        foreach (var dir in directions)
        {
            var dx = col + dir[0];
            var dy = row + dir[1];
            // check if the point is in the grid
            if (dx >= 0 && dx < Grid.GetLength(1) && dy >= 0 && dy < Grid.GetLength(0))
            {
                // if the spot is empty, add it to the list
                if (GetTile(dx, dy) == null) adjacent.Add(new int[] { dy, dx });
            }
        }

        return adjacent;
    }

    public List<int[]> GetReachablePositions(int row, int col)
    {
        return null;
    }

    public void Show()
    {
        for (var row = 0; row < Grid.GetLength(0); row++)
        {
            for (var col = 0; col < Grid.GetLength(1); col++)
            {
                if (Grid[row, col] == null) Console.Write(".");
                else Console.Write(Grid[row, col].Type);
            }
            Console.WriteLine();
        }
    }

    public void FillGrid(string input)
    {
        for (var row = 0; row < Grid.GetLength(0); row++)
        {
            for (var col = 0; col < Grid.GetLength(1); col++)
            {
                char c = input[row * Grid.GetLength(1) + col];

                switch (c)
                {
                    case '#':
                        Grid[row, col] = new Wall(new int[] { row, col });
                        break;
                    case 'E':
                        Grid[row, col] = new Unit('E', new int[] { row, col });
                        break;
                    case 'G':
                        Grid[row, col] = new Unit('G', new int[] { row, col });
                        break;
                    default:
                        Grid[row, col] = null;
                        break;
                }
            }
        }
    }

    public List<Unit> GetUnits()
    {
        var units = new List<Unit>();

        for (var row = 0; row < Grid.GetLength(0); row++)
        {
            for (var col = 0; col < Grid.GetLength(1); col++)
            {
                if (Grid[row, col]?.Type == 'E' || Grid[row, col]?.Type == 'G') units.Add((Unit)Grid[row, col]);
            }
        }

        return units;
    }

    public List<Unit> GetUnits(char type)
    {
        var units = new List<Unit>();

        for (var row = 0; row < Grid.GetLength(0); row++)
        {
            for (var col = 0; col < Grid.GetLength(1); col++)
            {
                if (Grid[row, col]?.Type == type) units.Add((Unit)Grid[row, col]);
            }
        }

        return units;
    }
}