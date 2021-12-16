using System.Collections.Generic;

namespace MapStuff
{
	public class Map
	{
		public int Rows { get; set; }
		public int Cols { get; set; }
		public Square[,] Matrix { get; set; }

		public Map(int rows, int cols)
		{
			Rows = rows;
			Cols = cols;
			Matrix = new Square[rows, cols];
			
			// fill the grid with squares
			for (var row = 0; row < Rows; row++)
			{
				for (var col = 0; col < cols; col++)
				{
					Matrix[row, col] = new Square(col, row);
				}
			}

			// set the neighbours of each square
			for (var row = 0; row < Rows; row++)
			{
				for (var col = 0; col < Cols; col++)
				{
					Matrix[row, col].Neighbours = GetNeighbours(row, col);
				}
			}
		}

		public List<Square> GetNeighbours(int row, int col)
		{
			var list = new List<Square>();

			if (row < Rows - 1) list.Add(Matrix[row + 1, col]);
			if (row > 0) list.Add(Matrix[row - 1, col]);
			if (col < Cols - 1) list.Add(Matrix[row, col + 1]);
			if (col > 0) list.Add(Matrix[row, col - 1]);

			return list;
		}
	}

	public class Square
	{
		public int X { get; set; }
		public int Y { get; set; }
		public int F { get; set; }
		public int G { get; set; }
		public int H { get; set; }
		public List<Square> Neighbours { get; set; }
		public Square Previous { get; set; }

		public Square(int x, int y)
		{
			X = x;
			Y = y;
			F = 0;
			G = 0;
			H = 0;
			Neighbours = new List<Square>();
			Previous = null;
		}
	}
}