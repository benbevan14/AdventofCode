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
						Grid[row, col] = new Wall(new int[] { col, row });
						break;
					case 'E':
						Grid[row, col] = new Unit('E', new int[] { col, row });
						break;
					case 'G':
						Grid[row, col] = new Unit('G', new int[] { col, row });
						break;
					default:
						Grid[row, col] = null;
						break;
				}
			}
		}
	}

	public List<Unit> GetUnits(char type)
	{
		var units = new List<Unit>();
		
		for (var row = 0; row < Grid.GetLength(0); row++)
		{
			for (var col = 0; col < Grid.GetLength(1); col++)
			{
				if (Grid[row, col]?.Type == type) units.Add((Unit) Grid[row, col]);
			}
		}

		return units;
	}
}