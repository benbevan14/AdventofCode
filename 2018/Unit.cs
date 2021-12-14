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

	public int[] SelectMove(char[,] grid)
	{
		// var enemies = 
		return new int[] {};
	}
}