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
				case "6":
					Console.WriteLine(args[1] == "1" ? SimulateFish(path) : ExponentionalFish(path));
					break;
				case "7":
					Console.WriteLine(args[1] == "1" ? AlignCrabs(path) : AlignCrabs2(path));
					break;
				case "8":
					Console.WriteLine(args[1] == "1" ? CommonDigits(path) : AllDigits(path));
					break;
				case "9":
					Console.WriteLine(args[1] == "1" ? LowPoints(path) : Basins(path));
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

		private static int SimulateFish(string path)
		{
			var fish = File.ReadAllText(path).Split(",").Select(int.Parse);

			for (var day = 1; day <= 80; day++)
			{
				var newFish = new List<int>();
				foreach (var f in fish)
				{
					if (f == 0)
					{
						newFish.Add(6);
						newFish.Add(8);
					}
					else
					{
						newFish.Add(f - 1);
					}
				}

				fish = newFish;
			}

			return fish.Count();
		}

		private static long ExponentionalFish(string path)
		{
			var input = File.ReadAllText(path).Split(",").Select(int.Parse);
			var fish = new long[9];

			foreach (var f in input)
			{
				fish[f]++;
			}

			for (var day = 1; day <= 256; day++)
			{
				var spawning = fish[0];
				for (var i = 0; i < 8; i++)
				{
					fish[i] = fish[i + 1];
				}
				fish[6] += spawning;
				fish[8] = spawning;
			}

			return fish.Sum();
		}

		private static int AlignCrabs(string path)
		{
			var crabs = File.ReadAllText(path).Split(",").Select(int.Parse).ToArray();

			var minFuel = int.MaxValue;

			for (var f = crabs.Min(); f <= crabs.Max(); f++)
			{
				var sum = crabs.Select(x => Math.Abs(x - f)).Sum();
				// Console.WriteLine("Target fuel: " + f + " : total fuel required: " + sum);
				if (sum < minFuel) minFuel = sum;
			}

			return minFuel;
		}

		private static int AlignCrabs2(string path)
		{
			var crabs = File.ReadAllText(path).Split(",").Select(int.Parse).ToArray();

			var minFuel = int.MaxValue;

			for (var f = crabs.Min(); f <= crabs.Max(); f++)
			{
				var sum = crabs.Select(x => 
				{
					var n = Math.Abs(x - f);
					return n * (n + 1) / 2;
				}).Sum();
				// Console.WriteLine("Target fuel: " + f + " : total fuel required: " + sum);
				if (sum < minFuel) minFuel = sum;
			}

			return minFuel;
		}

		private static int CommonDigits(string path)
		{
			var count = 0;
			var toFind = new int[] { 2, 3, 4, 7 };
			
			foreach (var line in File.ReadAllLines(path))
			{
				var content = line.Split(" | ");
				var output = content[1].Split(" ");
				count += output.Where(x => toFind.Contains(x.Length)).Count();
			}

			return count;
		}

		private static int AllDigits(string path)
		{
			var total = 0;

			foreach (var line in File.ReadAllLines(path))
			{
				var dict = new Dictionary<string, int>();
				var mapping = new Dictionary<char, char>();

				var content = line.Split(" | ");
				var input = content[0].Split(" ");
				var output = content[1].Split(" ");

				var one = input.Where(x => x.Length == 2).First();
				var four = input.Where(x => x.Length == 4).First();
				var seven = input.Where(x => x.Length == 3).First();
				var eight = input.Where(x => x.Length == 7).First();

				dict.Add(SortString(one), 1);
				dict.Add(SortString(four), 4);
				dict.Add(SortString(seven), 7);
				dict.Add(SortString(eight), 8);

				// get f and c
				// six contains only f, zero and nine contain c too
				var six = input.Where(x => x.Length == 6 && !(x.Contains(one[0]) && x.Contains(one[1]))).First();

				// differentiate f and c
				dict.Add(SortString(six), 6);
				mapping.Add('c', UniqueCharacters(one, six)[0]);
				mapping.Add('f', char.Parse(one.Replace(mapping['c'].ToString(), String.Empty)));

				// differentiate 9 and 0
				var nine = input.Where(x => x.Length == 6 && x != six && x.Contains(four[0]) && x.Contains(four[1]) && x.Contains(four[2]) && x.Contains(four[3])).First();
				var zero = input.Where(x => x.Length == 6 && x != six && x != nine).First();
				dict.Add(SortString(zero), 0);
				dict.Add(SortString(nine), 9);

				// differentiate 2, 3, and 5
				var two = input.Where(x => x.Length == 5 && x.Contains(mapping['c']) && !x.Contains(mapping['f'])).First();
				var three = input.Where(x => x.Length == 5 && x.Contains(mapping['c']) && x.Contains(mapping['f'])).First();
				var five = input.Where(x => x.Length == 5 && !x.Contains(mapping['c']) && x.Contains(mapping['f'])).First();
				dict.Add(SortString(two), 2);
				dict.Add(SortString(three), 3);
				dict.Add(SortString(five), 5);

				// Get the output
				var res = new StringBuilder();
				foreach (var num in output)
				{
					res.Append(dict[SortString(num)]);
				}
				
				total += int.Parse(res.ToString());
			}

			return total;
		}

		private static int LowPoints(string path)
		{
			var input = Regex.Replace(File.ReadAllText(path), @"\s+", "");

			var grid = new int[100, 100];

			// fill the grid
			for (var row = 0; row < 100; row++)
			{
				for (var col = 0; col < 100; col++)
				{
					grid[row, col] = int.Parse(input[row * 100 + col].ToString());
					// Console.Write(grid[row, col]);
				}
				// Console.WriteLine();
			}

			var riskSum = 0;

			// iterate through the grid
			for (var row = 0; row < 100; row++)
			{
				for (var col = 0; col < 100; col++)
				{
					var current = grid[row, col];
					var low = true;
					// check the four directions

					var dirs = new int[4][] 
					{
						new int[] { 1,  0}, 
						new int[] { 0, -1}, 
						new int[] {-1,  0}, 
						new int[] { 0,  1}
					};

					foreach (var dir in dirs)
					{
						var dx = dir[0];
						var dy = dir[1];

						// check the point is inside the grid
						if (row + dx >= 0 && row + dx < 100 && col + dy >= 0 && col + dy < 100)
						{
							if (grid[row + dx, col + dy] <= current)
							{
								low = false;
								break;
							}
						}
					}
					
					if (low) 
					{
						Console.WriteLine(current + " | " + row + ", " + col);
						riskSum += (current + 1);
					}
				}
			}

			return riskSum;
		}

		private static int Basins(string path)
		{
			var input = Regex.Replace(File.ReadAllText(path), @"\s+", "");

			var grid = new int[100, 100];

			// fill the grid
			for (var row = 0; row < 100; row++)
			{
				for (var col = 0; col < 100; col++)
				{
					grid[row, col] = int.Parse(input[row * 100 + col].ToString());
					// Console.Write(grid[row, col]);
				}
				// Console.WriteLine();
			}

			var lowPoints = new List<string>();
			var basins = new Dictionary<string, List<string>>();

			// iterate through the grid
			// if it's a low point, add it to the list of low points
			for (var row = 0; row < 100; row++)
			{
				for (var col = 0; col < 100; col++)
				{
					var current = grid[row, col];
					var low = true;
					// check the four directions

					var dirs = new int[4][] 
					{
						new int[] { 1,  0}, 
						new int[] { 0, -1}, 
						new int[] {-1,  0}, 
						new int[] { 0,  1}
					};

					foreach (var dir in dirs)
					{
						var dx = dir[0];
						var dy = dir[1];

						// check the point is inside the grid
						if (row + dx >= 0 && row + dx < 100 && col + dy >= 0 && col + dy < 100)
						{
							if (grid[row + dx, col + dy] <= current)
							{
								low = false;
								break;
							}
						}
					}
					
					if (low) lowPoints.Add(row + "," + col);
				}
			}

			// add each low point as a dictionary entry
			foreach (var p in lowPoints)
			{
				basins[p] = new List<string>();
			}

			// iterate through the grid again and add each point to a basin
			for (var row = 0; row < 100; row++)
			{
				for (var col = 0; col < 100; col++)
				{
					// ignore high points
					if (grid[row, col] != 9)
					{
						var lowest = GetBasin(grid, row, col, lowPoints);
						basins[lowest].Add(row + "," + col);
					}
				}
			}

			foreach (var basin in basins)
			{
				Console.WriteLine(basin.Key + ": " + basin.Value.Count);
			}

			return basins.Select(x => x.Value.Count).OrderByDescending(x => x).Take(3).Aggregate((x, y) => x * y);
		}



		private static string SortString(string s)
		{
			var chars = s.ToCharArray();
			Array.Sort(chars);
			return new string(chars);
		}

		private static char[] UniqueCharacters(string l, string s)
		{
			var unique = new List<char>();
			var sChars = s.ToCharArray();
			foreach (var c in l)
			{
				if (!sChars.Contains(c))
				{
					unique.Add(c);
				}
			}

			return unique.ToArray();
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

		private static string GetBasin(int[,] grid, int row, int col, List<string> low)
		{
			// if it's a low point, return this point
			if (low.Contains(row + "," + col)) return (row + "," + col);

			var current = grid[row, col];

			// while current is not in the list of low points, find the smallest point adjacent to it
			while (!low.Contains(row + "," + col))
			{
				var dirs = new int[4][]
				{
					new int[] {1, 0},
					new int[] {0, -1},
					new int[] {-1, 0},
					new int[] {0, 1}
				};

				var lowest = current;
				var direction = new int[] {0, 0};

				foreach (var dir in dirs)
				{
					var dx = dir[0];
					var dy = dir[1];

					// check it's in the grid
					if (row + dx >= 0 && row + dx < 100 && col + dy >= 0 && col + dy < 100)
					{
						// check if it's smaller
						if (grid[row + dx, col + dy] < lowest)
						{
							lowest = grid[row + dx, col + dy];
							direction = dir;
						}
					}
				}

				// move to the next pos
				current = lowest;
				row += direction[0];
				col += direction[1];
			}

			return (row + "," + col);
		}
    }
}
