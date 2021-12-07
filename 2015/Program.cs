using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace _2015
{
    class Program
    {
        static void Main(string[] args)
        {
			Console.WriteLine(AssembleCircuit(@"Data/7.txt"));
        }

		// Problem 3:1
		static int HowManyPresents(string path)
		{
			int x = 0;
			int y = 0;
			Dictionary<string, int> map = new Dictionary<string, int>();

			map.Add("0,0", 1);

			foreach (char c in File.ReadAllText(path))
			{
				switch (c)
				{
					case '^':
						y++;
						break;
					case 'v':
						y--;
						break;
					case '>':
						x++;
						break;
					case '<':
						x--;
						break;
				}

				string loc = x.ToString() + "," + y.ToString();
				int count = 0;
				map.TryGetValue(loc, out count);
				map[loc] = count + 1; 
			}

			return map.Count;
		}

		// Problem 3:2
		static int HowManyPresentsRobo(string path)
		{
			int sx = 0;
			int sy = 0;
			int rx = 0;
			int ry = 0;
			Dictionary<string, int> map = new Dictionary<string, int>();

			map.Add("0,0", 2);

			bool robo = false;

			foreach (char c in File.ReadAllText(path))
			{
				if (robo)
				{
					switch (c)
					{
						case '^':
							ry++;
							break;
						case 'v':
							ry--;
							break;
						case '>':
							rx++;
							break;
						case '<':
							rx--;
							break;
					}

					string loc = rx.ToString() + "," + ry.ToString();
					map.TryGetValue(loc, out var count);
					map[loc] = count + 1;
				}
				else
				{
					switch (c)
					{
						case '^':
							sy++;
							break;
						case 'v':
							sy--;
							break;
						case '>':
							sx++;
							break;
						case '<':
							sx--;
							break;
					}
					string loc = sx.ToString() + "," + sy.ToString();
					map.TryGetValue(loc, out var count);
					map[loc] = count + 1;
				}

				robo = !robo;
			}

			// foreach (var loc in map)
			// {
			// 	Console.WriteLine(loc.Key + ": " + loc.Value);
			// }

			return map.Count;
		}

		// Problem 4
		static int MD5Hash(string input, int length)
		{
			StringBuilder sb = new StringBuilder();
			string guess = input;
			int number = 0;
			using (MD5 md5 = MD5.Create())
			{
				do 
				{
					number++;
					guess = input + number.ToString();
					byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(guess);
					byte[] hashBytes = md5.ComputeHash(inputBytes);

					sb = new StringBuilder();
					for (int i = 0; i < hashBytes.Length; i++)
					{
						sb.Append(hashBytes[i].ToString("X2"));
					}
				} while (!sb.ToString().Substring(0, length).Equals(new String('0', length)));
				
				return number;
			}
		}

		// Problem 5:1
		static int NiceStrings(string path)
		{
			string vowelPattern = @"(\w*[aeiou]\w*){3,}";
			string doublePattern = @"(.)\1{1}";
			string forbiddenPattern = @"(ab)|(cd)|(pq)|(xy)";
			int counter = 0;
			foreach (string s in File.ReadAllLines(path))
			{
				if (Regex.IsMatch(s, vowelPattern) && Regex.IsMatch(s, doublePattern) && !Regex.IsMatch(s, forbiddenPattern))
				{
					counter++;
				}
			}

			return counter;
		}
		
		// Problem 5:2
		static int NicerStrings(string path)
		{
			string pairPattern = @"(\w{2}).*\1";
			string skipPattern = @"(.).{1}\1";
			int counter = 0;
			foreach (string s in File.ReadAllLines(path))
			{
				if (Regex.IsMatch(s, pairPattern) && Regex.IsMatch(s, skipPattern))
				{
					counter++;
				}
			}

			return counter;
		}
	
		// Problem 6:1
		static int ToggleLights(string path)
		{
			bool[,] lights = new bool[1000, 1000];

			foreach (string line in File.ReadAllLines(path))
			{
				Match match = Regex.Match(line, @"\d+,\d+");
				string[] start = match.Value.Split(',');
				match = match.NextMatch();
				string[] end = match.Value.Split(',');
				int startx = int.Parse(start[0]);
				int starty = int.Parse(start[1]);
				int endx = int.Parse(end[0]);
				int endy = int.Parse(end[1]);

				if (line.Substring(6, 1) == " ")
				{
					System.Console.WriteLine("Got to a toggle");
					for (int x = startx; x <= endx; x++)
					{
						for (int y = starty; y <= endy; y++)
						{
							lights[x, y] = !lights[x, y];
						}
					}
				}
				else if (line.Substring(6, 1) == "f") 
				{
					System.Console.WriteLine("Got to a turn off");
					for (int x = startx; x <= endx; x++)
					{
						for (int y = starty; y <= endy; y++)
						{
							lights[x, y] = false;
						}
					}
				}
				else if (line.Substring(6, 1) == "n")
				{
					System.Console.WriteLine("Got to a turn on");
					for (int x = startx; x <= endx; x++)
					{
						for (int y = starty; y <= endy; y++)
						{
							lights[x, y] = true;
						}
					}
				}
			}

			int counter = 0;
			for (int y = 0; y < lights.GetLength(0); y++)
			{
				for (int x = 0; x < lights.GetLength(1); x++)
				{
					if (lights[x, y])
					{
						counter++;
					}
				}
			}
			return counter;
		}

		// Problem 6:2
		static int ToggleLightsBright(string path)
		{
			int[,] lights = new int[1000, 1000];

			foreach (string line in File.ReadAllLines(path))
			{
				Match match = Regex.Match(line, @"\d+,\d+");
				string[] start = match.Value.Split(',');
				match = match.NextMatch();
				string[] end = match.Value.Split(',');
				int startx = int.Parse(start[0]);
				int starty = int.Parse(start[1]);
				int endx = int.Parse(end[0]);
				int endy = int.Parse(end[1]);

				if (line.Substring(6, 1) == " ")
				{
					//System.Console.WriteLine("Got to a toggle");
					for (int x = startx; x <= endx; x++)
					{
						for (int y = starty; y <= endy; y++)
						{
							lights[x, y] += 2;
						}
					}
				}
				else if (line.Substring(6, 1) == "f") 
				{
					//System.Console.WriteLine("Got to a turn off");
					for (int x = startx; x <= endx; x++)
					{
						for (int y = starty; y <= endy; y++)
						{
							if (lights[x, y] != 0)
							{
								lights[x, y]--;
							}
						}
					}
				}
				else if (line.Substring(6, 1) == "n")
				{
					//System.Console.WriteLine("Got to a turn on");
					for (int x = startx; x <= endx; x++)
					{
						for (int y = starty; y <= endy; y++)
						{
							lights[x, y]++;
						}
					}
				}
			}

			int sum = 0;
			for (int y = 0; y < lights.GetLength(0); y++)
			{
				for (int x = 0; x < lights.GetLength(1); x++)
				{
					sum += lights[x, y];
				}
			}
			return sum;
		}
	
		// Problem 7:1
		static int AssembleCircuit(string path)
		{
			Dictionary<string, int> signals = new Dictionary<string, int>();

			var instructions = File.ReadLines(path);

			// Get a starting point
			foreach (string line in File.ReadAllLines(path))
			{
				var content = line.Split(" -> ");

				// if the instruction is just to provide a signal to a wire
				if (!line.Any(char.IsUpper) && !content[0].Any(char.IsLetter))
				{
					Console.WriteLine(line);
					signals[content[1]] = int.Parse(content[0]);
				}
			}

			// process instructions, add them to dictionary and remove
			while (instructions.Count() > 0)
			{
				var newInstructions = new List<string>();
				
				foreach (var inst in instructions)
				{
					var args = inst.Split(" ");
					var needed = args.Where(x => x.ToLower() == x).ToArray();
					// If the dictionary contains all the needed values
					if (needed.All(value => signals.Keys.Contains(value)))
					{
						
					}
					else
					{
						newInstructions.Add(inst);
					}
				}

				instructions = newInstructions;
			}



			foreach (var p in signals)
			{
				Console.WriteLine(p.Key + ": " + p.Value);
			}

			return 0;
		}
	
		// Problem 8:1
		static int Matchsticks(string path)
		{
			int codeTotal = 0;
			int charTotal = 0;
			string[] test = new string[] {};
			foreach (string line in File.ReadAllLines(path))
			{
				int length = line.Length;
				string replaced = Regex.Replace(line, @"\\{2}", "A");
				replaced = Regex.Replace(replaced, @"\\""", "B");
				replaced = Regex.Replace(replaced, @"\\x[0-9a-f]{2}", "C");
				Console.WriteLine(line + " : " +  line.Length);
				Console.WriteLine(replaced + " : " + (replaced.Length - 2));
				codeTotal += line.Length;
				charTotal += replaced.Length - 2;
			}
			return codeTotal - charTotal;
		}

		// Problem 8:2
		static int MatchsticksReverse(string path)
		{
			// int codeTotal = 0;
			int charsAdded = 0;
			string[] test = new string[] {};
			foreach (string line in File.ReadAllLines(path))
			{
				charsAdded = 0;
				int length = line.Length;
				//string replaced = line;
				string replaced = Regex.Replace(line, @"""", @"\""");
				replaced = Regex.Replace(line, "\\[^\"x\\]", @"\\");
				//replaced = Regex.Replace(replaced, @"\", "\\\\");
				//replaced = Regex.Replace(replaced, @"\\x[0-9a-f]{2}", "C");

				foreach (Match m in Regex.Matches(line, @""""))
				{
					charsAdded++;
				}

				foreach (Match m in Regex.Matches(line, "\\[^\"x\\]"))
				{
					Console.WriteLine("adding a charcter for a backslash");
					charsAdded++;
				}
				replaced = "\"" + replaced + "\"";
				charsAdded += 4;
				Console.WriteLine(line);
				Console.WriteLine(replaced + " : " + replaced.Length);
				Console.WriteLine("chars added: " + charsAdded);
			}
			return charsAdded;
		}
	
		static string LookAndSay(string input)
		{
			//Console.WriteLine(input);
			// var result = input
			// 			.Aggregate(" ", (seed, next) => seed + (seed.Last() == next ? "" : " ") + next)
			// 			.Trim()
			// 			.Split(' ');

			// StringBuilder sb = new StringBuilder();

			// foreach (string s in result)
			// {
			// 	sb.Append(s.Length + "" + s[0]);
			// }
				
			// return sb.ToString();

			string result = "";
			int numberCount = 0;
			char number = input[0];

			for (int i = 0; i < input.Length; i++)
			{
				if (number == input[i])
				{
					numberCount++;
				}
				else
				{
					result += numberCount.ToString() + number;
					number = input[i];
					numberCount = 1;
				}
			}

			result += numberCount.ToString() + number;

			return result;
		}
	}
}
