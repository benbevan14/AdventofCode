using System;
using System.IO;

namespace _2019
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(BestAsteroid(@"data/test.txt"));
        }

		private static int BestAsteroid(string path)
		{
			var field = ReadGrid(path);

			DisplayGrid(field);

			

			return 0;
		}

		private static char[,] ReadGrid(string path)
		{
			var input = File.ReadAllLines(path);
			var size = input[0].Length;
			var grid = new char[size, size];

			for (var col = 0; col < size; col++)
			{
				for (var row = 0; row < size; row++)
				{
					grid[col, row] = input[row][col];
				}
			}

			return grid;
		}

		private static void DisplayGrid(char[,] grid)
		{
			for (var col = 0; col < grid.GetLength(0); col++)
			{
				for (var row = 0; row < grid.GetLength(1); row++)
				{
					Console.Write(grid[row, col] + " ");
				}
				Console.WriteLine();
			}
		}
    }
}
