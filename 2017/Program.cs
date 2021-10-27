using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace _2017
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(SpiralSteps());
        }

		static int AddDigits(string path)
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

		static int Checksum(string path)
		{
			int sum = 0;
			foreach (string s in File.ReadAllLines(path))
			{
				var nums = s.Split(new char[] {' ', '\t'}, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x));
				sum += nums.Max() - nums.Min();
			}
			return sum;
		}

		static int ChecksumDivisible(string path)
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

		static int SpiralSteps()
		{
			
		}
    }
}
