using System.Collections.Generic;
using System.Linq;
using System.Text;

public class SnailfishNumber
{
	public List<int> Items { get; set; }

	public SnailfishNumber(string input)
	{
		Items = ConvertToList(input);
	}

	public void AddSnailfishNumber(SnailfishNumber toAdd)
	{
		// add bracket at start
		Items.Insert(0, -3);
		// comma
		Items.Add(-1);
		// Number to add
		Items.AddRange(toAdd.Items);
		// closing bracket
		Items.Add(-2);
	}

	public int CheckExplode()
	{	
		var bracket = 0;
		for (var i = 0; i < Items.Count; i++)
		{
			if (Items[i] == -3) bracket++;
			else if (Items[i] == -2) bracket--;

			if (bracket > 4) return i + 1;
		}

		// not found
		return -1;
	}

	public void Explode(int start)
	{
		var left = Items[start];
		var right = Items[start + 2];
		// Console.WriteLine("left: " + left);
		// Console.WriteLine("right: " + right); 

		// add the left number to the next left
		for (var l = start - 1; l >= 0; l--)
		{
			if (Items[l] >= 0)
			{
				Items[l] += left;
				break;
			}
		}
		
		// add the right number to the next right
		for (var r = start + 3; r < Items.Count; r++)
		{
			if (Items[r] >= 0)
			{
				Items[r] += right;
				break;
			}
		}

		// remove the pair and replace it with a zero
		Items.RemoveRange(start - 1, 5);
		Items.Insert(start - 1, 0);
	}

	public int CheckSplit()
	{
		for (var i = 0; i < Items.Count; i++)
		{
			if (Items[i] > 9) return i;
		}
		
		// not found
		return -1;
	}

	public void Split(int start)
	{
		var left = Items[start] / 2;
		var right = (Items[start] + 1) / 2;

		// remove the number and insert the new pair
		Items.RemoveAt(start);
		var pair = new List<int> { -3, left, -1, right, -2 };
		Items.InsertRange(start, pair);
	}

	public void Reduce()
	{
		var explode = CheckExplode();
		var split = CheckSplit();

		while (explode > 0 || split > 0)
		{
			if (explode > 0)
			{
				Explode(explode);

				// exploding takes priority, so if we can still explode, skip splitting and do that again
				explode = CheckExplode();
				split = CheckSplit();
				continue;
			}

			if (split > 0)
			{
				Split(split);
			}

			explode = CheckExplode();
			split = CheckSplit();
		}
	}

	public int GetMagnitude()
	{
		var list = new List<int>(Items);
		// if there are any brackets left in the list
		while (list.Contains(-3))
		{	
			// iterate backwards through the list
			for (var i = list.Count - 1; i >= 0; i--)
			{
				// find a pair
				if (list[i] == -3 && list[i + 2] == -1 && list[i + 4] == -2)
				{
					var replace = list[i + 1] * 3 + list[i + 3] * 2;
					list.RemoveRange(i, 5);
					list.Insert(i, replace);
				}
			}
		}

		return list.First();
	}

	public List<int> ConvertToList(string input)
	{
		var res = new List<int>();

		for (var i = 0; i < input.Length; i++)
		{
			if (input[i] == '[') res.Add(-3);
			else if (input[i] == ']') res.Add(-2);
			else if (input[i] == ',') res.Add(-1);
			else 
			{
				if (char.IsDigit(input[i]) && char.IsDigit(input[i + 1]))
				{
					res.Add(int.Parse(input.Substring(i, 2)));
					i++;
				}
				else
				{
					res.Add(int.Parse(input.Substring(i, 1)));
				}
			}
		}

		return res;
	}

	public override string ToString()
	{
		var sb = new StringBuilder();

		foreach (var item in Items)
		{
			switch (item)
			{
				case -3:
					sb.Append("[");
					break;
				case -2:
					sb.Append("]");
					break;
				case -1:
					sb.Append(",");
					break;
				default:
					sb.Append(item);
					break;
			}
		}

		return sb.ToString();
	}
}