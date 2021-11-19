using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace _2017
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(EscapeList(@"data/5.txt"));
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
    }
}
