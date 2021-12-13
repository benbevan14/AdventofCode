using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace _2018
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(PlantPots(@"data/12.txt"));
        }

		private static int RepeatFrequency(string path)
		{
			var freq = 0;
			var lines = File.ReadAllLines(path).Select(int.Parse).ToArray();
			var f = new List<int>(new[] {0});

			for (var i = 0; i <= lines.Length; i++)
			{
				if (i == lines.Length) 
				{
					i = -1;
					continue;
				}

				freq += lines[i];
				if (!f.Contains(freq))
				{
					f.Add(freq);
				}
				else
				{
					return freq;
				}
			}
			return 0;
		}

		private static int BoxIdChecksum(string path)
		{
			var twoCount = 0;
			var threeCount = 0;

			foreach (var str in File.ReadAllLines(path))
			{
				var map = new Dictionary<char, int>();

				foreach (var c in str)
				{
					map.TryGetValue(c, out var count);
					map[c] = count + 1;
				}

				if (map.ContainsValue(2)) twoCount++;
				if (map.ContainsValue(3)) threeCount++;
			}

			return twoCount * threeCount;
		}

		private static string SimilarBoxIds(string path)
		{
			var ids = File.ReadAllLines(path);

			// for each id, compare it to each other and check how many different characters there are
			for (var i = 0; i < ids.Length - 1; i++)
			{
				for (var j = i + 1; j < ids.Length; j++)
				{
					//Console.WriteLine("Comparing " + ids[i] + " with " + ids[j]);
					if (CheckSimilar(ids[i], ids[j]))
					{
						Console.WriteLine(ids[i] + " and " + ids[j] + " are similar");
						return "found";
					}
				}
			}
			return "not found";
		}

		private static int FabricSize(string path)
		{
			var fabric = new string[8, 8];

			foreach (var str in File.ReadAllLines(path).Select(x => x.Split(' ')))
			{
				var pos = str[2].Trim(':').Split(',').Select(int.Parse).ToArray();
				var dim = str[3].Split('x').Select(int.Parse).ToArray();

				for (var j = pos[1]; j < pos[1] + dim[1]; j++)
				{
					for (var i = pos[0]; i < pos[0] + dim[0]; i++)
					{
						fabric[j, i] += str[0];
					}
				}
			}

			var count = 0;

			for (var j = 0; j < fabric.GetLength(0); j++)
			{
				for (var i = 0; i < fabric.GetLength(1); i++)
				{
					Console.Write(fabric[j, i] + " ");
					//if (fabric[j, i] > 1) count++;
				}
				Console.WriteLine();
			}

			return count;
		}

		private static int MoveStars(string path)
		{
			var input = File.ReadAllLines(path);

			var pos = new List<int[]>();
			var vel = new List<int[]>();

			foreach (var p in input.Select(x => x.Substring(10, 14)).Select(x => x.Split(",")))
			{
				pos.Add(new int[] { int.Parse(p[0].Trim()), int.Parse(p[1].Trim()) });
			}

			foreach (var v in input.Select(x => x.Substring(36, 6)).Select(x => x.Split(",")))
			{
				vel.Add(new int[] { int.Parse(v[0].Trim()), int.Parse(v[1].Trim()) });
			}

			var width = pos.Select(x => x[0]).Max() - pos.Select(x => x[0]).Min() + 1;
			var height = pos.Select(x => x[1]).Max() - pos.Select(x => x[1]).Min() + 1;

			var time = 0;

			for (var i = 1; i < 100000; i++)
			{
				for (var p = 0; p < pos.Count; p++)
				{
					pos[p][0] += vel[p][0];
					pos[p][1] += vel[p][1];
				}

				width = pos.Select(x => x[0]).Max() - pos.Select(x => x[0]).Min() + 1;
				height = pos.Select(x => x[1]).Max() - pos.Select(x => x[1]).Min() + 1;

				if (height < 20)
				{
					var minY = pos.Select(x => x[1]).Min();
					var maxY = pos.Select(x => x[1]).Max();
					var minX = pos.Select(x => x[0]).Min();
					var maxX = pos.Select(x => x[0]).Max();

					for (var y = minY - 1; y <= maxY + 1; y++)
					{
						for (var x = minX - 1; x <= maxX + 1; x++)
						{
							if (pos.Select(p => p[0] + "," + p[1]).Contains(x + "," + y))
							{
								Console.Write("#");
							}
							else
							{
								Console.Write(".");
							}
						}
						Console.WriteLine();
					}
					time = i;
				}
			}

			return time;
		}

		private static string FuelGrid()
		{
			var grid = new int[300, 300];
			var serial = 4172;

			for (var row = 0; row < 300; row++)
			{
				for (var col = 0; col < 300; col++)
				{
					var x = col + 1;
					var y = row + 1;

					var rack = x + 10;
					var power = y * rack;
					power += serial;
					power *= rack;
					power = (int) Math.Abs(power / 100 % 10) - 5;

					grid[row, col] = power;
				}
			}

			var highest = 0;
			var topLeft = new int[] { 0, 0 };
			var bestSize = 0;

			for (var size = 1; size <= 100; size++)
			{
				for (var row = 0; row < 301 - size; row++)
				{
					var total = 0;
					for (var r = row; r < row + size; r++)
					{
						for (var c = 0; c < size; c++)
						{
							total += grid[r, c];
						}
					}

					// for each column, add the column and remove the one 3 columns back
					for (var col = size; col < 300; col++)
					{
						for (var r = row; r < row + size; r++) 
						{
							total -= grid[r, col - size];
							total += grid[r, col];
						}

						if (total > highest)
						{
							highest = total;
							topLeft = new int[] { col - size + 2, row + 1 };
							bestSize = size;
						}
					}
				}
			}

			return topLeft[0] + "," + topLeft[1] + "," + bestSize;
		}

		private static int PlantPots(string path)
		{
			var input = File.ReadAllLines(path).Where(x => x != string.Empty);
			var state = input.First().Split(" ")[2];
			var conditions = input.Skip(1).ToArray();

			var pots = new char[200];
			for (var i = 0; i < pots.Length; i++) pots[i] = '.';

			// initialise the starting pots in the middle of the array
			for (var i = 0; i < state.Length; i++)
			{
				pots[i + 60] = state[i];
			}

			Console.Write(" 0:");
			Console.WriteLine(string.Join("", pots));

			// for each generation
			for (var gen = 1; gen < 21; gen++)
			{
				// initialise new array for next generation
				var newPots = new char[pots.Length]; 

				newPots[0] = '.';
				newPots[1] = '.';
				newPots[newPots.Length - 1] = '.';
				newPots[newPots.Length - 2] = '.';

				// for each pot, excluding those at the very ends
				for (var pot = 2; pot < pots.Length - 2; pot++)
				{
					// check against each condition
					foreach (var c in conditions.Select(x => x.Split(" => ")))
					{
						if (CheckSurroundingPots(pots, pot, c[0]))
						{
							newPots[pot] = char.Parse(c[1]);
							break;
						}
						else
						{
							newPots[pot] = '.';
						}
					}
				}

				pots = newPots;
				Console.Write(gen.ToString().PadLeft(2) + ":");
				Console.WriteLine(string.Join("", pots));
			}

			var total = 0;
			for (var i = 0; i < pots.Length; i++)
			{
				if (pots[i] == '#') total += (i - 60);
			}

			return total;
		}

		

		// Tools ================================================================

		// Check if two strings with the same length differ by only one character
		private static bool CheckSimilar(string a, string b)
		{
			var diff = 0;
			for (var i = 0; i < a.Length; i++)
			{
				if (a[i] != b[i]) diff++;
				if (diff > 1) return false;
			}
			return true;
		}

		private static bool CheckSurroundingPots(char[] pots, int current, string conditions)
		{
			for (var i = 0; i < 5; i++)
			{
				if (pots[current + i - 2] != conditions[i]) return false;
			}

			return true;
		}
    }
}
