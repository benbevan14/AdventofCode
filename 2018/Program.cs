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
            Console.WriteLine(RepeatFrequency(@"data\1.txt"));
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

		
    }
}
