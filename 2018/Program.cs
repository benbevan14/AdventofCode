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
            Console.WriteLine(EfficientReduction(@"data\5.txt"));
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

		private static int AlchemicalReduction(string input)
		{
			//var input = File.ReadAllText(path);

			var ptr = 0;

			while (ptr < input.Length - 1)
			{
				if (char.ToLower(input[ptr]) == char.ToLower(input[ptr + 1]) && input[ptr] != input[ptr + 1])
				{
					input = input.Remove(ptr, 2);
					ptr--;
					if (ptr < 0) ptr = 0;
					// Console.WriteLine(input);
				}
				else
				{
					ptr++;
				}
			}

			return input.Length;
		}

		private static int EfficientReduction(string path)
		{
			var input = File.ReadAllText(path);
			var dict = new Dictionary<char, int>();

			var alphabet = "abcdefghijklmnopqrstuvwxyz";

			foreach (var c in alphabet)
			{
				var removed = input.Replace(c.ToString(), "").Replace(char.ToUpper(c).ToString(), "");
				dict[c] = AlchemicalReduction(removed);
			}

			foreach (var p in dict)
			{
				Console.WriteLine(p.Key + ": " + p.Value);
			}

			return dict.OrderBy(p => p.Value).First().Value;
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
		
    }
}
