using System;
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
            Console.WriteLine(RecursiveCircus(@"data/7.txt"));
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
	}
}
