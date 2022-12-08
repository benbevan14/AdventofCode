using System;
using System.Collections.Generic;

public class Unit : ITile
{
    public char Type { get; set; }
    public int Hp { get; set; }
    public int Attack { get; set; }
    public int[] Position { get; set; }
    public bool HasMoved { get; set; }
    public bool HasAttacked { get; set; }
    public bool IsDead { get; set; }

    public Unit(char type, int[] position)
    {
        Type = type;
        Hp = 200;
        Attack = 3;
        Position = position;
        HasMoved = false;
        HasAttacked = false;
        IsDead = false;
    }

    public List<int[]> SelectMove(Map m, List<Unit> enemies)
    {
        // Get enemies in range
        var adjacent = new List<int[]>();

        foreach (var enemy in enemies)
        {
            Console.WriteLine("Looking at enemy at: " + enemy.Position[0] + "," + enemy.Position[1]);
            adjacent.AddRange(m.GetAdjacent(enemy.Position[0], enemy.Position[1]));
        }

        // Get reachable enemies

        return adjacent;
    }
}