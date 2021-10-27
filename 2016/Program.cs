using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;

namespace _2016
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(SSLProtocol(@"Data\7.txt"));
        }

		private static int StreetGrid(string path)
		{
			int rotation = 0;
			int xPos = 0;
			int yPos = 0;
			List<string> visited = new List<string>();
			foreach (string s in File.ReadAllText(path).Split(',').Select(x => x.Trim(' ')).ToArray())
			{
				if (s[0] == 'R')
				{
					rotation++;
					if (rotation == 4) rotation = 0;
				}
				else if (s[0] == 'L')
				{
					rotation--;
					if (rotation == -1) rotation = 3;
				}

				int steps = int.Parse(s[1..]);
				switch (rotation)
				{
					case 0:
						yPos += steps;
						break;
					case 1:
						xPos += steps;
						break;
					case 2:
						yPos -= steps;
						break;
					case 3:
						xPos -= steps;
						break;
				}
				if (!visited.Contains(xPos + "," + yPos))
				{
					Console.WriteLine("Im in here");
					visited.Add(xPos + "," + yPos);
				}
				else
				{
					Console.WriteLine("found it");
					return Math.Abs(xPos) + Math.Abs(yPos);
				}
			}

			foreach (string s in visited)
			{
				Console.WriteLine(s);
			}
			return Math.Abs(xPos) + Math.Abs(yPos);
		}

		private static int ValidTriangles(string path)
		{
			var count = 0;
			foreach (string s in File.ReadAllLines(path))
			{
				var trimmed = Regex.Replace(s, @"\s+", " ").Trim();
				int[] sides = trimmed.Split(' ').Select(int.Parse).ToArray();
				if (sides[0] + sides[1] <= sides[2] || sides[0] + sides[2] <= sides[1] || sides[1] + sides[2] <= sides[0])
				{
					continue;
				}
				count++;
			}
			
			return count;
		}

		private static int VerticalTriangles(string path)
		{
			var count = 0;
			var lines = File.ReadAllLines(path).Select(x => Regex.Replace(x, @"\s+", " ").Trim()).ToList();

			for (var i = 0; i < lines.Count - 2; i += 3)
			{
				var first = lines[i].Split(' ').Select(int.Parse).ToArray();
				var second = lines[i + 1].Split(' ').Select(int.Parse).ToArray();
				var third = lines[i + 2].Split(' ').Select(int.Parse).ToArray();

				for (var j = 0; j < 3; j++)
				{
					if (first[j] + second[j] <= third[j] || first[j] + third[j] <= second[j] || second[j] + third[j] <= first[j]) 
					{
						continue;
					}
					count++;
				}
			}
			return count;
		}

		private static int DecoyRooms(string path)
		{
			var total = 0;
			foreach (var s in File.ReadAllLines(path))
			{
				//Console.WriteLine("Room is: " + s);
				var check = Regex.Match(s, @"\[\w{5}\]").Value.Substring(1, 5);
				//Console.WriteLine("Checksum is: " + check);
				var code = int.Parse(Regex.Match(s, @"\d{3}").Value);
				//Console.WriteLine("Code is: " + code);
				var map = new Dictionary<char, int>();
				foreach (var c in s)
				{
					if (c == '-') continue;
					if (char.IsDigit(c)) break;

					map.TryGetValue(c, out var count);
					map[c] = count + 1;
				}

				var top = string.Join("", map.OrderByDescending(x => x.Value).ThenBy(x => x.Key).Take(5).Select(x => x.Key));
				//Console.WriteLine("Top characters are: " + top);

				if (top == check)
				{
					//Console.WriteLine("This is a valid room");

					var text = s.Remove(s.Length - 11).Replace("-", " ");
					var shift = code % 26;
					var newText = string.Join("", text.Where(x => x != ' ').Select(x => x + shift > 122 ? (char)(x + shift - 26) : (char)(x + shift)));
					if (newText.Contains("north"))
					{
						Console.WriteLine(s);
						Console.WriteLine(newText);
					}
					total += code;
				}
				//Console.WriteLine();
			}

			return total;
		}

		private static string MD5Hash(string doorId)
		{
			var pass = new StringBuilder();
			using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
			{
				var i = 0;
				while (pass.Length < 8)
				{
					byte[] input = System.Text.Encoding.ASCII.GetBytes(doorId + "" + i);
					byte[] hashed = md5.ComputeHash(input);

					var output = string.Join("", hashed.Select(x => x.ToString("x2")));

					if (output.Substring(0, 5) == "00000")
					{
						pass.Append(output.Substring(5, 1));
					}
					i++;
				}
			}
			return pass.ToString();
		}

		private static string ComplexHash(string doorId)
		{
			var pass = new string[8] {"-", "-", "-", "-", "-", "-", "-" ,"-"};
			var counter = 0;
			using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
			{
				var i = 0;
				while (counter < 8)
				{
					byte[] input = System.Text.Encoding.ASCII.GetBytes(doorId + "" + i);
					byte[] hashed = md5.ComputeHash(input);

					var output = string.Join("", hashed.Select(x => x.ToString("x2")));

					if (output.Substring(0, 5) == "00000" && char.IsDigit(output[5]))
					{
						Console.WriteLine(output);
						var index = int.Parse(output.Substring(5, 1));
						if (index < 8 && pass[index] == "-")
						{
							pass[index] = output.Substring(6, 1);
							Console.WriteLine(string.Join("", pass));
							counter++;
						}
					}
					i++;
				}
			}
			return string.Join("", pass);
		}

		private static string Signals(string path)
		{
			var words = File.ReadAllText(path).Split('\n');
			var pass = "";
			
			for (var i = 0; i < words[0].Length - 1; i++)
			{
				pass += words.Select(x => x[i]).GroupBy(x => x).OrderBy(x => x.Count()).First().Key;
			}
			
			return pass;
		}

		private static int TLSProtocol(string path)
		{
			var count = 0;

			foreach (string input in File.ReadAllLines(path)) 
			{
				var regex = new Regex(@"\[\w+?\]");

				// strings not in square brackets
				var non = regex.Replace(input, ".").Split('.');

				var sqAbba = false;

				// Search in sq brackets for abba
				foreach (Match match in regex.Matches(input))
				{
					if (FindAbba(match.Value.Trim('[', ']')) && !sqAbba) sqAbba = true;
				}

				// If we found one it's invalid
				if (sqAbba) continue;

				// Search in the rest for abba
				foreach (var str in non)
				{
					if (FindAbba(str))
					{
						count++;
						break;
					} 
				}
			}

			return count;
		}

		private static int SSLProtocol(string path)
		{
			var count = 0;
			
			foreach (var str in File.ReadAllLines(path))
			{
				var regex = new Regex(@"\[\w+?\]");

				// strings not in square brackets
				var non = regex.Replace(str, ".").Split('.');

				var bab = new List<string>();

				// Search in sq brackets for bab
				foreach (Match match in regex.Matches(str))
				{
					var arr = match.Value.ToCharArray();
					for (var i = 0; i < arr.Length - 2; i++)
					{
						if (arr[i] == arr[i + 2] && arr[i] != arr[i + 1])
						{
							bab.Add(arr[i + 1].ToString() + arr[i].ToString() + arr[i + 1].ToString());
						}
					}
				}

				foreach (var s in non)
				{
					foreach (var b in bab)
					{
						if (s.Contains(b))
						{
							count++;
							break;
						}
					}
				}
			}
			return count;
		}

		private static bool FindAbba(string s)
		{
			var arr = s.ToCharArray();
			for (var i = 0; i <= arr.Length - 4; i++)
			{
				if (arr[i] == arr[i + 3] && arr[i + 1] == arr[i + 2])
				{
					if (arr[i] != arr[i + 1]) return true;
				}
			}
			return false;
		}
	}
}
