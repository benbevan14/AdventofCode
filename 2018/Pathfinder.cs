public class Pathfinder
{
    public bool[,] Walls { get; set; }
    public int[,] Distances { get; set; }

    public Pathfinder()
    {

    }

    public int[,] GetDistanceMatrix(int[] start)
    {

    }
}

public class Vertex
{
    public int Distance { get; set; }
    public int[] Previous { get; set; }
}