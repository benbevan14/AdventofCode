using System;
using System.IO;
using System.Linq;

namespace _2021
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(SlidingDepth(@"data/1.txt"));
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
    }
}
