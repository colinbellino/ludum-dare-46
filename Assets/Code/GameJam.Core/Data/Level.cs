using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameJam.Core
{
	[CreateAssetMenu(fileName = "Level", menuName = "GameJam/Level")]
	public class Level : SerializedScriptableObject
	{
		public Dictionary<Vector2Int, Cell> Board = new Dictionary<Vector2Int, Cell> {
			{ new Vector2Int(0, 0), new Cell { Terrain = 1, Structure = 0, Fire = 1 } },
			{ new Vector2Int(0, 1), new Cell { Terrain = 1, Structure = null } },
			{ new Vector2Int(0, 2), new Cell { Terrain = 1, Structure = null } },
			{ new Vector2Int(0, 3), new Cell { Terrain = 1, Structure = null } },
			{ new Vector2Int(0, 4), new Cell { Terrain = 1, Structure = 0 } },
			{ new Vector2Int(1, 0), new Cell { Terrain = 1, Structure = null } },
			{ new Vector2Int(1, 1), new Cell { Terrain = 1, Structure = null } },
			{ new Vector2Int(1, 2), new Cell { Terrain = 1, Structure = null } },
			{ new Vector2Int(1, 3), new Cell { Terrain = 1, Structure = null } },
			{ new Vector2Int(1, 4), new Cell { Terrain = 1, Structure = 0 } },
			{ new Vector2Int(2, 0), new Cell { Terrain = 1, Structure = null } },
			{ new Vector2Int(2, 1), new Cell { Terrain = 1, Structure = null } },
			{ new Vector2Int(2, 2), new Cell { Terrain = 1, Structure = 0 } },
			{ new Vector2Int(2, 3), new Cell { Terrain = 1, Structure = null } },
			{ new Vector2Int(2, 4), new Cell { Terrain = 1, Structure = null } },
			{ new Vector2Int(3, 0), new Cell { Terrain = 1, Structure = null } },
			{ new Vector2Int(3, 1), new Cell { Terrain = 1, Structure = null } },
			{ new Vector2Int(3, 2), new Cell { Terrain = 1, Structure = 0 } },
			{ new Vector2Int(3, 3), new Cell { Terrain = 1, Structure = null } },
			{ new Vector2Int(3, 4), new Cell { Terrain = 1, Structure = null } },
			{ new Vector2Int(4, 0), new Cell { Terrain = 1, Structure = null } },
			{ new Vector2Int(4, 1), new Cell { Terrain = 1, Structure = null } },
			{ new Vector2Int(4, 2), new Cell { Terrain = 1, Structure = 0 } },
			{ new Vector2Int(4, 3), new Cell { Terrain = 1, Structure = 0 } },
			{ new Vector2Int(4, 4), new Cell { Terrain = 1, Structure = 1 } },
		};
	}
}
