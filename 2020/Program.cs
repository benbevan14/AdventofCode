using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace _2020
{
    public class Program
    {
        public static void Main(string[] args)
        {
			Console.WriteLine(BinaryBoarding(@"data\5.txt"));
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
			string[] passports = text.Split(new string[] {"\r\n\r\n"}, StringSplitOptions.RemoveEmptyEntries);

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
			var highest = 0;

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

				if (id > highest) highest = id;
			}

			return highest;
		}

		private static int UniqueLetters(string path)
		{
			List<string> lines = new List<string>();
			foreach (string s in File.ReadAllText(path).Split(System.Environment.NewLine, System.StringSplitOptions.RemoveEmptyEntries))
			{
				Console.WriteLine(s + ":");
			}
			return 0;
		}
	}
}
