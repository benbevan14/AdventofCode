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
			Console.WriteLine(Medicine(@"Data/19.txt"));
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
        static int CrossedWires(string path)
        {
            var signals = new Dictionary<string, int>();
            var instructions = new Dictionary<string, string[]>();

            // parse instructions
            foreach (var line in File.ReadAllLines(path))
            {
                var content = line.Split(" -> ");
                instructions[content[1]] = content[0].Split(" ");
            }

            // keep removing instructions as they're evaulated
            while (instructions.Count > 0)
            {
                var remove = new List<string>();
                // for each instruction, see if we have all the necessary signals
                foreach (var inst in instructions)
                {
                    var key = inst.Key;
                    var value = inst.Value;

                    // if we can evaluate the instruction then do so
                    if (CanEvaluate(value, signals))
                    {
                        // convert all strings in instruction to numbers
                        var translated = new string[value.Length];
                        for (var i = 0; i < translated.Length; i++)
                        {
                            translated[i] = signals.TryGetValue(value[i], out var sig) ? sig.ToString() : value[i];
                        }

                        // evaluate this instruction
                        signals[key] = Evaluate(new KeyValuePair<string, string[]>(key, translated), signals);

                        // remove this instruction from the list left to evaluate
                        remove.Add(key);
                    }
                }

                // remove completed instructinos
                foreach (var key in remove)
                {
                    instructions.Remove(key);
                }
            }

            return signals["a"];
        }

        static bool CanEvaluate(string[] instruction, Dictionary<string, int> signals)
        {
            var needed = instruction.Where(item => !int.TryParse(item, out var i) && item.ToUpper() != item)
                                    .Where(item => !signals.Keys.Contains(item));

            return !needed.Any();
        }

        // Take in a single instruction and evaluate it
        static int Evaluate(KeyValuePair<string, string[]> instruction, Dictionary<string, int> signals)
        {
            var key = instruction.Key;
            var val = instruction.Value;

            // Direct copy of signal value
            if (val.Length == 1)
            {
                return int.Parse(val[0]);
            }

            // All cases where length is 2
            if (val[0] == "NOT")
            {
                var s = ushort.Parse(val[1]);
                return (ushort)~s;
            }

            // All cases where length is 3
            if (val[1] == "RSHIFT")
            {
                return (ushort)(ushort.Parse(val[0]) >> ushort.Parse(val[2]));
            }
            if (val[1] == "LSHIFT")
            {
                return (ushort)(ushort.Parse(val[0]) << ushort.Parse(val[2]));
            }
            if (val[1] == "AND")
            {
                return (ushort)(ushort.Parse(val[0]) & ushort.Parse(val[2]));
            }
            if (val[1] == "OR")
            {
                return (ushort)(ushort.Parse(val[0]) | ushort.Parse(val[2]));
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
        static int HowManyCharacters(string path)
        {
            var total = 0;

            foreach (var str in File.ReadAllLines(path))
            {
                // starts at 2 for the double quotes at each end
                var sum = 2;
                sum += str.Length;
                sum += str.Count(c => c == '\\' || c == '"');
                total += (sum - str.Length);
            }

            return total;
        }

        static int LookAndSay2(string input)
        {
            for (var i = 0; i < 3; i++)
            {
                var current = new List<string>();

                var ptr = 0;
                while (ptr < input.Length)
                {
                    var ind = ptr;
                    Console.WriteLine("Starting search at pointer: " + ptr);
                    while (input[ind] == input[ptr])
                    {
                        if (++ind == input.Length) break;
                    }
                    current.Add((ind - ptr) + "," + input[ptr]);
                    Console.WriteLine("Added: " + current.Last());
                    ptr += (ind - ptr);
                }
            }


            // Console.WriteLine(string.Join("__", current));

            return 0;
        }

        // Problem 12:1
        static int JsonNumbers(string input)
        {
            string data = File.ReadAllText(input);
            var sum = 0;

            foreach (var match in Regex.Matches(data, @"-?\d+"))
            {
                sum += int.Parse(match.ToString());
            }

            return sum;
        }

        // Problem 12:2
        static int NotRed(string input)
        {
            string data = File.ReadAllText(input);
            var ptr = 0;

            while (ptr < data.Length - 2)
            {
                var check = data.Substring(ptr, 3);
                if (check == "red")
                {
                    var start = FindBound(data, ptr, true);
                    var end = start != -1 ? FindBound(data, ptr, false) : -1;

                    // if it's an array
                    if (start == -1)
                    {
                        // replace red with BLU
                        data = data.Remove(ptr, 3).Insert(ptr, "BLU");
                    }
                    else
                    {
                        // remove this whole object and put the pointer back to the start
                        data = data.Remove(start, (end - start) + 1);
                        ptr = 0;
                        continue;
                    }
                }
                ptr++;
            }

            var sum = 0;
            foreach (var match in Regex.Matches(data, @"-?\d+"))
            {
                sum += int.Parse(match.ToString());
            }

            return sum;
        }

        static int FindBound(string data, int ptr, bool backwards)
        {
            char[] brackets = new char[4] {']', '}', '[', '{'};

            if (!backwards)
            {
                brackets[0] = '[';
                brackets[1] = '{';
                brackets[3] = '}';
                brackets[2] = ']';
            }

            var curly = 0;
            var square = 0;

            while (curly <= 0 && square <= 0)
            {
                if (backwards) ptr--;
                else ptr++;

                char c = data[ptr];
                if (c == brackets[0]) square--;
                if (c == brackets[1]) curly--;
                if (c == brackets[2]) square++;
                if (c == brackets[3]) curly++;
            }

            if (square == 1) return -1;
            else return ptr;
        }

        // Problem 19:1
        static int Medicine(string path)
        {
            var info = File.ReadAllText(path).Split("\r\n\r\n");
            var replacements = info[0].Split("\r\n").Select(x => x.Split(" => "));
            var molecule = info[1];
            var unique = new HashSet<string>();

            // for each set of replacements
            foreach (var pair in replacements)
            {
                var ptr = 0;
                var key = pair[0];
                var value = pair[1];
                // move through the molecule and replace each occurrence of the key
                while (ptr <= molecule.Length - key.Length)
                {
                    // if this section of the molecule matches the key, replace it
                    if (molecule.Substring(ptr, key.Length) == key)
                    {
                        var replaced = molecule.Remove(ptr, key.Length).Insert(ptr, value);
                        unique.Add(replaced);
                    }
                    // move the pointer
                    ptr++;
                }
            }

            foreach (var mol in unique)
            {
                Console.WriteLine(mol);
            }

            Console.WriteLine(unique.Count);

            return 0;
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

		static int FindSue(string path)
		{
			var input = File.ReadAllLines(path);
			var sues = new Dictionary<int, Dictionary<string, int>>();

			foreach (var line in input)
			{
				var words = line.Split(" ");
				var key = int.Parse(words[1].TrimEnd(':'));
				var items = new Dictionary<string, int>();

				for (var i = 0; i < 6; i += 2)
				{
					var item = words[i + 2].TrimEnd(':');
					var amount = int.Parse(words[i + 3].TrimEnd(','));
					items[item] = amount;
				}

				sues[key] = items;
			}

			var required = new Dictionary<string, int>
			{
				{ "children", 3 },
				{ "cats", 7 },
				{ "samoyeds", 2 },
				{ "pomeranians", 3 },
				{ "akitas", 0 },
				{ "vizslas", 0 },
				{ "goldfish", 5 },
				{ "trees", 3 },
				{ "cars", 2 },
				{ "perfumes", 1 }
			};

			var list = Enumerable.Range(1, 500);
			// foreach (var req in required)
			// {
			// 	list = list.Where(x => (sues[x].ContainsKey(req.Key) && sues[x][req.Key] == req.Value) || !sues[x].ContainsKey(req.Key));
			// }

			foreach (var item in required.Select((value, i) => new { i, value }))
			{
				var pair = item.value;
				if (item.i == 1 || item.i == 7)
				{
					list = list
						.Where(x => (sues[x].ContainsKey(pair.Key) && sues[x][pair.Key] > pair.Value) || !sues[x].ContainsKey(pair.Key));
				}
				else if (item.i == 3 || item.i == 6)
				{
					list = list
						.Where(x => (sues[x].ContainsKey(pair.Key) && sues[x][pair.Key] < pair.Value) || !sues[x].ContainsKey(pair.Key));
				}
				else
				{
					list = list
						.Where(x => (sues[x].ContainsKey(pair.Key) && sues[x][pair.Key] == pair.Value) || !sues[x].ContainsKey(pair.Key));
				}
			}

			Console.WriteLine(list.Count());
			Console.WriteLine(list.First());

			return 0;
		}

		static bool HasStraight(string input)
		{
			for (var i = 0; i < input.Length - 2; i++)
			{
				if (input[i] + 1 == input[i + 1] && input[i] + 2 == input[i + 2]) return true;
			}

			return false;
		}

		static bool HasLegalCharacters(string input)
		{
			return !input.Any(x => x == 'i' || x == 'o' || x == 'l');
		}
	}
}
