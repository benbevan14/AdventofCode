using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Scanner
{
	public int ID { get; set; }
	public List<List<int>> Beacons { get; set; }
	public int[][,] Distances { get; set; }

	public Scanner(int id, List<string> beacons)
	{
		ID = id;
		Beacons = new List<List<int>>();
		Distances = new int[Beacons.Count][,];

		// populate the beacon list
		foreach (var beacon in beacons)
		{
			Beacons.Add(beacon.Split(",").Select(int.Parse).ToList());
		}

		// populate the distances list
		foreach (var beacon in beacons)
		{

		}
	}

	public override string ToString()
	{
		var sb = new StringBuilder();
		sb.Append("Scanner: " + ID + "\n");
		foreach (var beacon in Beacons)
		{
			sb.Append(string.Join(",", beacon) + "\n");
		}

		return sb.ToString();
	}
}