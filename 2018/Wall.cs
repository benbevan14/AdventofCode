public class Wall : ITile
{
	public char Type { get; set; }
	public int[] Position { get; set; }

	public Wall(int[] position)
	{
		Type = '#';
		Position = position;
	}
}