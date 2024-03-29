﻿using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace _2017
{
    public class Program
    {
        public static void Main(string[] args)
        {
			var path = @"data/" + args[2] + ".txt";

			switch (args[0])
			{
				case "10":
					Console.WriteLine(args[1] == "1" ? KnotHash(path) : 0);
					break;
				case "11":
					Console.WriteLine(args[1] == "1" ? HexGrid(path) : HexGridFurthest(path));
					break;

			}
        }

		private static int AddDigits(string path)
		{
			string s = File.ReadAllText(path);
			int step = s.Length / 2;
			int sum = 0;
			for (int i = 0; i < s.Length; i++)
			{
				int j = (i + step) > s.Length - 1 ? i + step - s.Length : i + step;
				if (s[i] == s[j])
				{
					sum += int.Parse(s[i].ToString());
				}
			}
			return sum;
		}

		private static int Checksum(string path)
		{
			int sum = 0;
			foreach (string s in File.ReadAllLines(path))
			{
				var nums = s.Split(new char[] {' ', '\t'}, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x));
				sum += nums.Max() - nums.Min();
			}
			return sum;
		}

		private static int ChecksumDivisible(string path)
		{
			int sum = 0;
			foreach (string s in File.ReadAllLines(path))
			{
				var nums = s.Split(new char[] {' ', '\t'}, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToList();
				List<int> sorted = nums.OrderBy(n => n).ToList();
				for (int i = 0; i < sorted.Count; i++)
				{
					for (int j = 0; j < sorted.Count; j++)
					{
						if (j == i)
						{
							continue;
						}
						else
						{
							if (sorted[j] % sorted[i] == 0)
							{
								sum += sorted[j] / sorted[i];
							}
						}
					}
				}

			}
			return sum;
		}

		private static int ValidPassphrases(string path)
		{
			var count = 0;

			foreach (var words in File.ReadAllLines(path).Select(x => x.Split(' ')))
			{
				var map = new Dictionary<string, int>();

				foreach (var word in words)
				{
					var sorted = String.Concat(word.OrderBy(c => c));
					map.TryGetValue(sorted, out var num);
					map[sorted] = num + 1;
				}

				if (!map.Values.Any(p => p > 1)) count++;
			}

			return count;
		}

		private static int EscapeList(string path)
		{
			var nums = File.ReadAllLines(path).Select(int.Parse).ToArray();

			var steps = 0;
			var pos = 0;

			while (pos < nums.Length)
			{
				var change = nums[pos] > 2 ? -1 : 1;
				var temp = pos;
				pos += nums[pos];
				nums[temp] += change;
				steps++;
			}

			//Console.WriteLine(string.Join(" ", nums));
			return steps;
		}
    
		private static int AllocateBlocks(string path)
		{
			var input = Regex.Replace(File.ReadAllText(path).Trim(), @"\s+", " ").Split(" ").Select(int.Parse).ToArray();
			var distributions = new List<string>();
			distributions.Add(string.Join(",", input));
			// Console.WriteLine(distributions[0]);

			var iterations = 0;

			while (true)
			{
				iterations++;
				var max = input.Max();
				var ptr = Array.IndexOf(input, max);

				input[ptr] = 0;
				while (max > 0)
				{
					// Move to the next block
					ptr++;
					if (ptr > input.Length - 1) ptr = 0;
					input[ptr]++;
					max--;
				}

				var currentDist = string.Join(",", input);
				// Console.WriteLine(currentDist);
				if (distributions.Contains(currentDist)) return iterations - distributions.IndexOf(currentDist);
				else distributions.Add(currentDist);
			}
		}
	
		private static string RecursiveCircus(string path)
		{
			var input = File.ReadAllLines(path);
			var heldBy = new Dictionary<string, string>();

			foreach (var str in input)
			{
				if (str.Contains(">"))
				{
					var content = str.Split(">");
					var holding = content[1].Replace(" ", "").Split(",");
					foreach (var program in holding)
					{
						heldBy[program] = content[0].Trim().Split(" ")[0];
					}
				}
			}

			// foreach (var p in heldBy)
			// {
			// 	Console.WriteLine(p.Key + " is held by " + p.Value);
			// }

			foreach (var str in input)
			{
				var key = str.Split(" ")[0];
				if (!heldBy.ContainsKey(key))
				{
					return key;
				}
			}

			return "";
		}
	
		private static int Register(string path)
		{
			foreach (var instruction in File.ReadAllLines(path).Select(x => x.Split(" ")))
			{
				var register = instruction[0];
				var op = instruction[1];
				var amount = instruction[2];
			}
			return 0;
		}

		private static int KnotHash(string path)
		{
			var nums = Enumerable.Range(0, 256).ToArray();
			var inputs = File.ReadAllText(path).Split(",").Select(int.Parse).ToArray();
			var l = nums.Length;

			// Console.WriteLine(string.Join(",", nums));

			var ptr = 0;
			var skip = 0;
			foreach (var i in inputs)
			{
				if (ptr >= l) ptr -= l;

				for (var j = 0; j < ptr; j++) nums = LeftShiftArray(nums);
				Array.Reverse(nums, 0, i);
				for (var j = 0; j < ptr; j++) nums = RightShiftArray(nums);

				ptr += (i + skip);
				skip++;

				// Console.WriteLine(string.Join(",", nums));
			}

			Console.WriteLine(string.Join(",", nums));

			return nums[0] * nums[1];
		}

		private static int HexGrid(string path)
		{
			var q = 0;
			var r = 0;

			foreach (var step in File.ReadAllText(path).Split(","))
			{
				switch (step)
				{
					case "n":
						r--;
						break;
					case "ne":
						q++;
						r--;
						break;
					case "se":
						q++;
						break;
					case "s":
						r++;
						break;
					case "sw":
						q--;
						r++;
						break;
					case "nw":
						q--;
						break;
				}
			}

			var s = -q - r;
			q = Math.Abs(q);
			r = Math.Abs(r);
			s = Math.Abs(s);

			return Math.Max(Math.Max(q, r), s);
		}

		private static int HexGridFurthest(string path)
		{
			var q = 0;
			var r = 0;

			var furthest = 0;

			foreach (var step in File.ReadAllText(path).Split(","))
			{
				switch (step)
				{
					case "n":
						r--;
						break;
					case "ne":
						q++;
						r--;
						break;
					case "se":
						q++;
						break;
					case "s":
						r++;
						break;
					case "sw":
						q--;
						r++;
						break;
					case "nw":
						q--;
						break;
				}

				var rTemp = Math.Abs(r);
				var qTemp = Math.Abs(q);
				var s = Math.Abs(-q - r);

				var dist = Math.Max(Math.Max(rTemp, qTemp), s);

				if (dist > furthest) furthest = dist;
			}

			return furthest;
		}



		private static int[] LeftShiftArray(int[] arr)
		{
			return arr.Skip(1).Concat(arr.Take(1)).ToArray();
		}

		private static int[] RightShiftArray(int[] arr)
		{
			return arr.Skip(arr.Length - 1).Concat(arr.Take(arr.Length - 1)).ToArray();
		}
	}
}
