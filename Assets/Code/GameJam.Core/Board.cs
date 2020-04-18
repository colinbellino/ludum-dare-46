using UnityEngine;

namespace GameJam.Core
{
	public class Board
	{
		public Cell[,] Cells;

		public Board(Vector2Int size)
		{
			Cells = new Cell[size.x, size.y];
		}
	}

	public class Cell
	{
		public CellTypes Type; // Rock => can't plant | Dirt => can plant
		public Plant Content; // Resource placed in this cell
	}

	public enum CellTypes
	{
		Rock,
		Grass,
	}

	public class Plant
	{
		public int Burning;
	}
}
