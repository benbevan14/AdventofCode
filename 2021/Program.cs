using System;
using System.IO;
using System.Linq;

namespace _2021
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(PilotSub2(@"data/2.txt"));
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
    }
}
