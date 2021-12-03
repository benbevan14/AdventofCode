using System;
using System.IO;
using System.Linq;
using System.Text;

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
    }
}
