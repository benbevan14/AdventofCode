using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace _2021
{
    public class Program
    {
        public static void Main(string[] args)
        {
			var path = @"data/" + args[2] + ".txt";

			switch (args[0])
			{
				case "1":
					Console.WriteLine(args[1] == "1" ? IncreasingDepth(path) : SlidingDepth(path));
					break;
				case "2":
					Console.WriteLine(args[1] == "1" ? PilotSub(path) : PilotSub2(path));
					break;
				case "3":
					Console.WriteLine(args[1] == "1" ? Diagnostics(path) : LifeSupportDiagnostics(path));
					break;
				case "4":
					Console.WriteLine(args[1] == "1" ? WinBingo(path) : LoseBingo(path));
					break;
				case "5":
					Console.WriteLine(args[1] == "1" ? StraightVents(path) : DiagonalVents(path));
					break;
			}
        }

		private static int IncreasingDepth(string path)
		{
			var count = 0;
			var depths = File.ReadAllLines(path).Select(int.Parse).ToArray();
			for (var i = 0; i < depths.Length - 1; i++)
			{
				if (depths[i + 1] > depths[i]) count++;
			}

			return count;
		}

		private static int SlidingDepth(string path)
		{
			var count = 0;
			var depths = File.ReadAllLines(path).Select(int.Parse).ToArray();
			for (var i = 0; i < depths.Length - 3; i++)
			{
				if (depths[i + 3] > depths[i]) count++;
			}

			return count;
		}

		private static int PilotSub(string path)
		{
			var pos = 0;
			var depth = 0;

			foreach (var inst in File.ReadAllLines(path))
			{
				var content = inst.Split(" ");
				var mag = int.Parse(content[1]);

				switch (content[0])
				{
					case "forward":
						pos += mag;
						break;
					case "down":
						depth += mag;
						break;
					case "up":
						depth -= mag;
						break;
				}
			}

			return pos * depth;
		}

		private static int PilotSub2(string path)
		{
			var pos = 0;
			var depth = 0;
			var aim = 0;

			foreach (var inst in File.ReadAllLines(path))
			{
				var content = inst.Split(" ");
				var mag = int.Parse(content[1]);

				switch (content[0])
				{
					case "forward":
						pos += mag;
						depth += mag * aim;
						break;
					case "down":
						aim += mag;
						break;
					case "up":
						aim -= mag;
						break;
				}
			}

			return pos * depth;
		}

		private static int Diagnostics(string path)
		{
			var input = File.ReadAllLines(path);
			var len = input.Length;
			var gamma = new StringBuilder();
			var epsilon = new StringBuilder();

			for (var i = 0; i < input[0].Length; i++)
			{
				var sum = input.Select(x => int.Parse(x.Substring(i, 1))).Sum();
				if (sum > len / 2) 
				{
					gamma.Append("1");
					epsilon.Append("0");
				}
				else
				{
					gamma.Append("0");
					epsilon.Append("1");
				}
			}

			return Convert.ToInt32(gamma.ToString(), 2) * Convert.ToInt32(epsilon.ToString(), 2);
		}

		private static int LifeSupportDiagnostics(string path)
		{
			var input = File.ReadAllLines(path);
			var len = input.Length;

			var oxygen = input.ToList();
			var co2 = input.ToList();

			for (var i = 0; i < oxygen[0].Length; i++)
			{
				var common = oxygen.Select(x => int.Parse(x.Substring(i, 1))).Sum() >= oxygen.Count / 2.0 ? "1" : "0";
				var uncommon = common == "1" ? "0" : "1";
				if (oxygen.Count > 1) 
					oxygen = oxygen.Where(x => x.Substring(i, 1) == common).ToList();
				else
					break;
			}

			for (var i = 0; i < co2[0].Length; i++)
			{
				var common = co2.Select(x => int.Parse(x.Substring(i, 1))).Sum() >= co2.Count / 2.0 ? "1" : "0";
				var uncommon = common == "1" ? "0" : "1";
				if (co2.Count > 1)
					co2 = co2.Where(x => x.Substring(i, 1) == uncommon).ToList();
				else
					break;
			}

			return Convert.ToInt32(oxygen[0], 2) * Convert.ToInt32(co2[0], 2);
		}

		private static int WinBingo(string path)
		{
			var input = File.ReadAllText(path).Split("\r\n\r\n", StringSplitOptions.RemoveEmptyEntries);

			var numbers = input[0].Split(",").Select(int.Parse).ToArray();
			var grids = new List<int[]>();

			// read in each grid into an array of nums
			for (var i = 1; i < input.Length; i++)
			{
				var nums = Regex.Replace(input[i], @"\s+", " ").Trim().Split(" ").Select(int.Parse).ToArray();
				grids.Add(nums);
			}

			// add each number from the list to each grid
			for (var n = 0; n < numbers.Length; n++)
			{
				// change the number in each grid
				for (var i = 0; i < grids.Count; i++)
				{
					var index = Array.IndexOf(grids[i], numbers[n]);
					if (index != -1)
						grids[i][index] = 100;
				}

				// check each grid to see if any won
				foreach (var grid in grids)
				{
					if (CheckGrid(grid))
					{
						return grid.Where(x => x != 100).Sum() * numbers[n];
					}
				}
			}

			return 0;
		}

		private static int LoseBingo(string path)
		{
			var input = File.ReadAllText(path).Split("\r\n\r\n", StringSplitOptions.RemoveEmptyEntries);

			var numbers = input[0].Split(",").Select(int.Parse).ToArray();
			var grids = new List<int[]>();

			// read in each grid into an array of nums
			for (var i = 1; i < input.Length; i++)
			{
				var nums = Regex.Replace(input[i], @"\s+", " ").Trim().Split(" ").Select(int.Parse).ToArray();
				grids.Add(nums);
			}

			// add each number from the list to each grid
			for (var n = 0; n < numbers.Length; n++)
			{
				// change the number in each grid
				for (var i = 0; i < grids.Count; i++)
				{
					var index = Array.IndexOf(grids[i], numbers[n]);
					if (index != -1)
						grids[i][index] = 100;
				}

				// if there's only one grid left and it just finished, return
				if (grids.Count == 1)
				{
					if (CheckGrid(grids[0]))
						return grids[0].Where(x => x != 100).Sum() * numbers[n];
				}

				// otherwise remove any grids that are finished
				grids = grids.Where(x => CheckGrid(x) == false).ToList();
			}

			return 0;
		}

		private static int StraightVents(string path)
		{
			var lines = File.ReadAllLines(path);
			var grid = new int[1000, 1000];
			foreach (var line in lines)
			{	
				var content = line.Split(" -> ");
				var from = content[0].Split(",").Select(int.Parse).ToArray();
				var to = content[1].Split(",").Select(int.Parse).ToArray();

				var xMin = Math.Min(from[0], to[0]);
				var xMax = Math.Max(from[0], to[0]);
				var yMin = Math.Min(from[1], to[1]);
				var yMax = Math.Max(from[1], to[1]);

				if (xMin == xMax)
				{
					for (var i = yMin; i <= yMax; i++) grid[xMin, i]++;
				}
				else if (yMin == yMax)
				{
					for (var i = xMin; i <= xMax; i++) grid[i, yMin]++;
				}
			}

			var count = 0;
			for (var row = 0; row < 1000; row++)
			{
				for (var col = 0; col < 1000; col++)
				{
					if (grid[row, col] > 1) count++;
				}
			}

			return count;
		}

		private static int DiagonalVents(string path)
		{
			var lines = File.ReadAllLines(path);
			var grid = new int[1000, 1000];
			foreach (var line in lines)
			{	
				var sorted = line.Split(" -> ").OrderBy(x => int.Parse(x.Split(",")[0])).ThenBy(x => int.Parse(x.Split(",")[1])).ToArray();

				var from = sorted[0].Split(",").Select(int.Parse).ToArray();
				var to = sorted[1].Split(",").Select(int.Parse).ToArray();

				if (from[0] == to[0])
				{
					for (var i = from[1]; i <= to[1]; i++) grid[from[0], i]++;
				}
				else if (from[1] == to[1])
				{
					for (var i = from[0]; i <= to[0]; i++) grid[i, from[1]]++;
				}
				else if (Math.Abs(from[1] - to[1]) == Math.Abs(from[0] - to[0])) // 45 degree diagonal
				{
					if ((to[1] - from[1]) * (to[0] - from[0]) > 0)
					{
						// positive gradient
						for (var x = 0; x <= to[0] - from[0]; x++)
						{
							grid[from[0] + x, from[1] + x]++;
						}
					}
					else // negative gradient
					{
						for (var x = 0; x <= to[0] - from[0]; x++)
						{
							grid[from[0] + x, from[1] - x]++;
						}
					}
				}
			}

			var count = 0;
			for (var row = 0; row < 1000; row++)
			{
				for (var col = 0; col < 1000; col++)
				{
					if (grid[row, col] > 1) count++;
				}
			}

			return count;
		}


		private static bool CheckGrid(int[] grid)
		{
			// Check all the rows
			for (var i = 0; i < 5; i++)
			{
				var sum = grid.Skip(i * 5).Take(5).Sum();
				if (sum == 500) return true;
			}

			// Check all the columns
			for (var i = 0; i < 5; i++)
			{
				var sum = 0;
				for (var j = 0; j < 5; j++)
				{
					sum += grid[j * 5 + i];
				}
				if (sum == 500) return true;
			}
			
			return false;
		}
    }
}
