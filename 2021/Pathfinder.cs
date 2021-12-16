using System.Collections.Generic;
using MapStuff;

public class Pathfinder
{
	public List<Square> OpenSet { get; set; }
	public List<Square> ClosedSet { get; set; }
	public Square Start { get; set; }
	public Square End { get; set; }
	public List<Square> Path { get; set; }
	public bool Flag { get; set; }

	public Pathfinder(Map map)
	{
		Start = map.Matrix[0, 0];
		End = map.Matrix[map.Rows - 1, map.Cols - 1];
		OpenSet = new List<Square>();
		OpenSet.Add(Start);
		ClosedSet = new List<Square>();
		Path = new List<Square>();
		Flag = false;
	}

	public void FindPath(int[,] grid)
	{
		var winner = 0;

		for (var i = 0; i < OpenSet.Count; i++)
		{
			if (OpenSet[i].F < OpenSet[winner].F) winner = i;
		}

		var current = OpenSet[winner];

		if (current == End) Flag = true;

		OpenSet.RemoveAt(winner);
		ClosedSet.Add(current);

		var border = current.Neighbours;
		for (var i = 0; i < border.Count; i++)
		{
			var item = border[i];

			if (!ClosedSet.Contains(item))
			{
				
			}
		}
	}
}