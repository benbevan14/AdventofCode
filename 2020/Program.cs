using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;

namespace _2020
{
	public class Program
	{
		public static void Main(string[] args)
		{
			Console.WriteLine(BusTime(@"data\13.txt"));
		}

		private static int FindSum(string path)
		{
			List<(int, int)> pairs = new List<(int, int)>();
			HashSet<int> hashSet = new HashSet<int>();
			foreach (int n in File.ReadAllLines(path).Select(x => int.Parse(x)))
			{
				int required = 2020 - n;
				if (hashSet.Contains(required))
				{
					pairs.Add((required, n));
				}

				if (!hashSet.Contains(n))
				{
					hashSet.Add(n);
				}
			}

			return pairs[0].Item1 * pairs[0].Item2;
		}

		private static void ThreeSum(string path)
		{
			int[] nums = File.ReadAllLines(path).Select(x => int.Parse(x)).ToArray();

			for (int i = 0; i < nums.Length - 2; i++)
			{
				HashSet<int> s = new HashSet<int>();
				int currentSum = 2020 - nums[i];
				for (int j = i + 1; j < nums.Length; j++)
				{
					if (s.Contains(currentSum - nums[j]))
					{
						Console.WriteLine(nums[i] + ", " + nums[j] + ", " + (currentSum - nums[j]));
						Console.WriteLine(nums[i] * nums[j] * (currentSum - nums[j]));
					}
					s.Add(nums[j]);
				}
			}
		}

		private static int CheckPasswords(string path)
		{
			int counter = 0;
			foreach (string line in File.ReadAllLines(path))
			{
				var info = line.Split(' ');
				int[] nums = info[0].Split('-').Select(x => int.Parse(x)).ToArray();
				string letter = info[1].Remove(1);
				string password = info[2];
				string removed = password.Replace(letter, "");
				int diff = password.Length - removed.Length;
				if (diff >= nums[0] && diff <= nums[1])
				{
					counter++;
				}

				//Console.WriteLine(nums[0] + "-" + nums[1] + " " + letter + ": " + password);
			}

			return counter;
		}

		private static int CheckMorePasswords(string path)
		{
			int counter = 0;
			foreach (string line in File.ReadAllLines(path))
			{
				Console.WriteLine(line);
				var info = line.Split(' ');
				int[] nums = info[0].Split('-').Select(x => int.Parse(x) - 1).ToArray();
				string letter = info[1].Remove(1);
				string password = info[2];

				Console.WriteLine(password[nums[0]] + ", " + password[nums[1]]);

				if (password[nums[0]].ToString() == letter ^ password[nums[1]].ToString() == letter)
				{
					counter++;
				}
			}
			return counter;
		}

		private static int TobogganRun(string path, int width)
		{
			int counter = 0;
			int xPos = 0;
			foreach (string line in File.ReadAllLines(path))
			{
				if (line[xPos] == '#')
				{
					counter++;
				}
				xPos += width;
				if (xPos >= line.Length)
				{
					xPos -= line.Length;
				}
			}
			return counter;
		}

		private static int SteeperRun(string path)
		{
			int counter = 0;
			int xPos = 0;
			bool check = true;
			foreach (string line in File.ReadAllLines(path))
			{
				if (!check)
				{
					check = !check;
					continue;
				}

				if (line[xPos] == '#')
				{
					counter++;
				}
				xPos++;
				if (xPos >= line.Length)
				{
					xPos -= line.Length;
				}
				check = !check;
			}
			return counter;
		}

		private static int PassportCheck(string path)
		{
			int numValid = 0;
			string text = File.ReadAllText(path);
			string[] passports = text.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);

			foreach (string s in passports)
			{
				int numFields = s.Split(' ', '\n').Length;
				if (numFields > 6)
				{
					if (!Regex.IsMatch(s, @"byr:\d{4}")) continue;
					if (!Regex.IsMatch(s, @"iyr:\d{4}")) continue;
					if (!Regex.IsMatch(s, @"eyr:\d{4}")) continue;
					if (!Regex.IsMatch(s, @"hgt:\d{2,3}[a-zA-Z]{2}")) continue;
					if (!Regex.IsMatch(s, @"hcl:#[0-9a-f]{6}")) continue;
					if (!Regex.IsMatch(s, @"ecl:(amb|blu|brn|gry|grn|hzl|oth)")) continue;
					if (!Regex.IsMatch(s, @"pid:\d{9}")) continue;

					// Check byr
					int byr = int.Parse(Regex.Match(s, @"byr:\d{4}").Value.Replace("byr:", ""));
					if (byr < 1920 || byr > 2002) continue;
					// Check iyr
					int iyr = int.Parse(Regex.Match(s, @"iyr:\d{4}").Value.Replace("iyr:", ""));
					if (iyr < 2010 || iyr > 2020) continue;
					// Check eyr
					int eyr = int.Parse(Regex.Match(s, @"eyr:\d{4}").Value.Replace("eyr:", ""));
					if (eyr < 2020 || eyr > 2030) continue;
					// Check hgt
					Regex r = new Regex(@"(?<hgt>hgt:\d{2,3}(cm|in))");
					var res = r.Match(s);
					string hgt = res.Groups["hgt"].Value.Substring(4);
					string type = Regex.Replace(hgt, @"\d", "");
					int number = int.Parse(Regex.Replace(hgt, @"[a-zA-Z]", ""));

					if (type == "cm")
					{
						if (number < 150 || number > 193) continue;
					}
					else if (type == "in")
					{
						if (number < 59 || number > 76) continue;
					}
					else continue;

					//Console.WriteLine(s);
					//Console.WriteLine();
					numValid++;
				}
			}
			return numValid;
		}

		private static int BinaryBoarding(string path)
		{
			var ids = new List<int>();

			foreach (var str in File.ReadAllLines(path))
			{
				var row = 0;
				var col = 0;

				var low = 0;
				var high = 127;
				var range = high - low + 1;

				for (var i = 0; i < 7; i++)
				{
					range /= 2;
					if (str[i] == 'F') high = low + range - 1;
					else if (str[i] == 'B') low += range;
				}

				row = low;

				low = 0;
				high = 7;
				range = high - low + 1;
				for (var i = 0; i < 3; i++)
				{
					range /= 2;
					if (str[i + 7] == 'L') high = low + range - 1;
					else if (str[i + 7] == 'R') low += range;
				}

				col = low;

				var id = row * 8 + col;

				ids.Add(id);
			}

			for (var i = 0; i <= 980; i++)
			{
				if (!ids.Contains(i)) Console.WriteLine(i);
			}

			return 0;
		}

		private static int UniqueLetters(string path)
		{
			var total = 0;
			foreach (string s in File.ReadAllText(path).Split(new string[] { "\r\n\r\n" }, System.StringSplitOptions.RemoveEmptyEntries))
			{
				var str = s.Replace("\r\n", "");
				var lines = (s.Length - str.Length + 2) / 2;
				var dict = new Dictionary<char, int>();
				foreach (char c in str)
				{
					dict.TryGetValue(c, out var count);
					dict[c] = count + 1;
				}
				total += dict.Values.Where(v => v == lines).Count();
			}
			return total;
		}

		private static int InfiniteLoop(string[] codes)
		{
			var acc = 0;
			var visited = new List<int>();
			var ptr = 0;

			while (true)
			{
				// If we've already been to this location the program will loop infinitely, so return 0
				if (visited.Contains(ptr)) return 0;

				visited.Add(ptr);
				var temp = codes[ptr].Split(" ");
				var inst = temp[0];
				var arg = int.Parse(temp[1]);

				switch (inst)
				{
					case "acc":
						acc += arg;
						ptr++;
						break;
					case "jmp":
						ptr += arg;
						break;
					case "nop":
						ptr++;
						break;
				}

				// If the pointer is past the end then we've terminated, so return the accumulator value
				if (ptr >= codes.Length) return acc;
			}
		}

		private static int CorruptedInstruction(string path)
		{
			var codes = File.ReadAllLines(path);
			for (var i = 0; i < codes.Length; i++)
			{
				var newInst = "";
				if (codes[i].Substring(0, 3) == "nop")
				{
					newInst = "jmp" + codes[i].Substring(3);
				}
				else if (codes[i].Substring(0, 3) == "jmp")
				{
					newInst = "nop" + codes[i].Substring(3);
				}

				if (newInst != "")
				{
					var newCodes = (string[])codes.Clone();
					newCodes[i] = newInst;

					var output = InfiniteLoop(newCodes);
					if (output != 0)
					{
						return output;
					}
				}
			}
			return 0;
		}

		private static long EncodingError(string path)
		{
			var nums = File.ReadAllLines(path).Select(long.Parse).ToArray();
			var len = 25;

			for (var i = len; i < nums.Length; i++)
			{
				var prev = nums.Skip(i - len).Take(len).ToArray();
				if (!TwoSum(prev, nums[i])) return nums[i];
			}
			return 0;
		}

		private static long SubArraySum(string path)
		{
			var input = File.ReadAllLines(path).Select(long.Parse).ToArray();
			var target = 731031916;
			var curr = input[0];
			var start = 0;

			for (var i = 1; i <= input.Length; i++)
			{
				while (curr > target && start < i - 1)
				{
					curr -= input[start];
					start++;
				}

				if (curr == target)
				{
					var range = input.Skip(start).Take(i - 1 - start);
					return range.Min() + range.Max();
				}

				if (i < input.Length) curr += input[i];
			}

			return 0;
		}

		private static int AdapterPath(string path)
		{
			var adapters = File.ReadAllLines(path).Select(int.Parse).OrderBy(x => x).ToArray();
			var dict = new Dictionary<int, int>();
			var jolt = 0;
			var device = adapters.Max() + 3;

			// Do stuff
			for (var i = 0; i < adapters.Length; i++)
			{
				var newJolt = adapters[i];
				dict.TryGetValue(newJolt - jolt, out var count);
				dict[newJolt - jolt] = count + 1;
				jolt = newJolt;
			}

			dict.TryGetValue(device - jolt, out var c);
			dict[device - jolt] = c + 1;

			return dict[1] * dict[3];
		}

		private static long AdapterCombinations(string path)
		{
			var adapters = File.ReadAllLines(path).Select(long.Parse).OrderBy(x => x).ToArray();
			// Keep track of number of ways we can approach this adapter
			var ways = new Dictionary<long, long>();
			ways[0] = 1;

			// Find the number of one step ways you can get to each adapter
			for (var i = 1; i < adapters.Length; i++)
			{
				var current = adapters[i];
				var num = 0;
				for (var j = 1; j <= 3; j++)
				{
					if (adapters.Contains(current - j)) num++;
				}
				ways[current] = num;

				// Account for the number of ways to get to previous adapters
				for (var j = 1; j <= num; j++)
				{
					ways[adapters[i]] += ways[adapters[i - j]] - 1;
				}
			}

			// Print out the number of one step ways to get to each value
			foreach (var p in ways)
			{
				Console.WriteLine(p.Key + ": " + p.Value);
			}

			return ways[adapters.Last()];
		}

		private static int SeatingSystem(string path)
		{
			char[,] grid = ReadGrid(path);

			//DisplayGrid(grid);
			//Console.WriteLine();

			var prev = "";

			// Iterate
			while (GridToString(grid) != prev)
			{
				prev = GridToString(grid);
				grid = IterateGrid2(grid);

				//DisplayGrid(grid);
				//Console.WriteLine();
			}

			return GridToString(grid).Count(c => c == '#');
		}

		private static int RainRisk(string path)
		{
			var dir = 1;
			var xPos = 0;
			var yPos = 0;

			var facings = new int[4][] { new int[] { 0, 1 }, new int[] { 1, 0 }, new int[] { 0, -1 }, new int[] { -1, 0 } };

			var d = facings[dir];

			Console.WriteLine($"Current position: {xPos}, {yPos}, facing {dir}");

			foreach (var step in File.ReadAllLines(path))
			{
				Console.WriteLine(step);
				var inst = step[0];
				var mag = int.Parse(step.Substring(1));

				switch (inst)
				{
					case 'N':
						d = facings[0];
						break;
					case 'E':
						d = facings[1];
						break;
					case 'S':
						d = facings[2];
						break;
					case 'W':
						d = facings[3];
						break;
					case 'L':
						dir -= mag / 90;
						if (dir < 0) dir += 4;
						// set dx and dy based on new heading
						d = facings[dir];
						break;
					case 'R':
						dir += mag / 90;
						if (dir > 3) dir -= 4;
						// set dx and dy based on new heading
						d = facings[dir];
						break;
					case 'F':
						d = facings[dir];
						break;
				}

				// Move
				if (inst != 'L' && inst != 'R')
				{
					xPos += mag * d[0];
					yPos += mag * d[1];
				}

				Console.WriteLine($"Current position: {xPos}, {yPos}, facing {dir}");
			}

			return Math.Abs(xPos) + Math.Abs(yPos);
		}

		private static int WaypointRainRisk(string path)
		{
			var pos = new int[] { 0, 0 };
			var waypoint = new int[] { 10, 1 };

			Console.WriteLine($"Current position: {pos[0]}, {pos[1]}");
			Console.WriteLine($"Current waypoint position: {waypoint[0]}, {waypoint[1]}");

			foreach (var step in File.ReadAllLines(path))
			{
				Console.WriteLine(step);
				var inst = step[0];
				var mag = int.Parse(step.Substring(1));

				switch (inst)
				{
					case 'N':
						waypoint[1] += mag;
						break;
					case 'E':
						waypoint[0] += mag;
						break;
					case 'S':
						waypoint[1] -= mag;
						break;
					case 'W':
						waypoint[0] -= mag;
						break;
					case 'R':
						for (var i = 0; i < mag / 90; i++)
						{
							var temp = -waypoint[0];
							waypoint[0] = waypoint[1];
							waypoint[1] = temp;
						}
						break;
					case 'L':
						mag = 360 - mag;
						for (var i = 0; i < mag / 90; i++)
						{
							var temp = -waypoint[0];
							waypoint[0] = waypoint[1];
							waypoint[1] = temp;
						}
						break;
					case 'F':
						pos[0] += mag * waypoint[0];
						pos[1] += mag * waypoint[1];
						break;
				}

				Console.WriteLine($"Current position: {pos[0]}, {pos[1]}");
				Console.WriteLine($"Current waypoint position: {waypoint[0]}, {waypoint[1]}\n");
			}

			return Math.Abs(pos[0]) + Math.Abs(pos[1]);
		}

		private static int BusTime(string path)
		{
			var input = File.ReadAllLines(path);
			var early = int.Parse(input[0]);
			var ids = input[1].Split(',').Where(x => x != "x").Select(int.Parse).ToArray();

			var earliest = new Dictionary<int, int>();

			foreach (var id in ids)
			{
				var curr = id;
				while (curr < early) curr += id;
				earliest[id] = curr;
			}
			
			var bus = earliest.OrderBy(x => x.Value).First();

			return (bus.Value - early) * bus.Key;
		}
		
		// Tools
		private static bool TwoSum(long[] arr, long target)
		{
			var hs = new HashSet<long>();

			for (var i = 0; i < arr.Length; i++)
			{
				var diff = target - arr[i];
				if (hs.Contains(diff)) return true;
				hs.Add(arr[i]);
			}

			return false;
		}

		private static char[,] ReadGrid(string path)
		{
			var input = File.ReadAllLines(path);
			var size = input[0].Length;
			var grid = new char[size, size];

			for (var row = 0; row < size; row++)
			{
				for (var col = 0; col < size; col++)
				{
					grid[row, col] = input[row][col];
				}
			}

			return grid;
		}

		private static void DisplayGrid(char[,] grid)
		{
			for (var row = 0; row < grid.GetLength(0); row++)
			{
				for (var col = 0; col < grid.GetLength(1); col++)
				{
					Console.Write(grid[row, col] + " ");
				}
				Console.WriteLine();
			}
		}

		private static string GridToString(char[,] grid)
		{
			var sb = new StringBuilder();
			for (var row = 0; row < grid.GetLength(0); row++)
			{
				for (var col = 0; col < grid.GetLength(1); col++)
				{
					sb.Append(grid[row, col]);
				}
			}

			return sb.ToString();
		}

		private static char[,] IterateGrid(char[,] grid)
		{
			var newGrid = new char[grid.GetLength(0), grid.GetLength(1)];

			for (var row = 0; row < grid.GetLength(0); row++)
			{
				for (var col = 0; col < grid.GetLength(1); col++)
				{
					// Only check seats
					if (grid[row, col] != '.')
					{
						var occupied = 0;
						// check surrounding squares
						for (var dx = -1; dx <= 1; dx++)
						{
							for (var dy = -1; dy <= 1; dy++)
							{
								// Check the neighbouring coordinate is in the grid
								if (col + dx >= 0 && col + dx < grid.GetLength(1) && row + dy >= 0 && row + dy < grid.GetLength(0))
								{
									// Increment if occupied
									if (grid[row + dy, col + dx] == '#') occupied++;
								}
							}
						}

						// If the seat is empty, check how many surrounding
						if (grid[row, col] == 'L' && occupied == 0) newGrid[row, col] = '#';
						else if (grid[row, col] == '#' && occupied > 4) newGrid[row, col] = 'L';
						else newGrid[row, col] = grid[row, col];
					}
					else
					{
						newGrid[row, col] = grid[row, col];
					}
				}
			}

			return newGrid;
		}

		private static char[,] IterateGrid2(char[,] grid)
		{
			var newGrid = new char[grid.GetLength(0), grid.GetLength(1)];

			for (var row = 0; row < grid.GetLength(0); row++)
			{
				for (var col = 0; col < grid.GetLength(1); col++)
				{
					// Only check seats
					if (grid[row, col] != '.')
					{
						var occupied = 0;
						// check surrounding squares
						for (var dx = -1; dx <= 1; dx++)
						{
							for (var dy = -1; dy <= 1; dy++)
							{
								// Skip when not going in any direction
								if (dx == 0 && dy == 0) continue;

								// Check if there is an occupied seat in each direction
								if (SearchInDirection(grid, col, row, dx, dy)) occupied++;
							}
						}

						// If the seat is empty, check how many surrounding
						if (grid[row, col] == 'L' && occupied == 0) newGrid[row, col] = '#';
						else if (grid[row, col] == '#' && occupied > 4) newGrid[row, col] = 'L';
						else newGrid[row, col] = grid[row, col];
					}
					else
					{
						newGrid[row, col] = grid[row, col];
					}
				}
			}

			return newGrid;
		}

		private static bool SearchInDirection(char[,] grid, int row, int col, int dx, int dy)
		{
			var xPos = col + dx;
			var yPos = row + dy;

			while (xPos >= 0 && xPos < grid.GetLength(1) && yPos >= 0 && yPos < grid.GetLength(0))
			{
				if (grid[xPos, yPos] == '#') return true;
				else if (grid[xPos, yPos] == 'L') return false;

				xPos += dx;
				yPos += dy;
			}

			return false;
		}
	}
}
