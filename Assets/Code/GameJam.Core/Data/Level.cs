using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameJam.Core
{
	[CreateAssetMenu(fileName = "Level", menuName = "GameJam/Level")]
	public class Level : SerializedScriptableObject
	{
		public Dictionary<Structure, int> Structures = new Dictionary<Structure, int>();

		// Start : 	{ Structure = 0, Fire = 1 }
		// Tree :	{ Structure = 0 }
		// Rock : 	{ Structure = 9 }
		// Exit : 	{ Structure = 99 }

		[DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.OneLine, IsReadOnly = true)]
		public Dictionary<Vector2Int, Cell> Board = new Dictionary<Vector2Int, Cell> {
			{ new Vector2Int(0, 0), new Cell { Structure = 0, Fire = 1 } },
			{ new Vector2Int(0, 1), new Cell { } },
			{ new Vector2Int(0, 2), new Cell { } },
			{ new Vector2Int(0, 3), new Cell { } },
			{ new Vector2Int(0, 4), new Cell { } },
			{ new Vector2Int(0, 5), new Cell { } },
			{ new Vector2Int(0, 6), new Cell { } },
			{ new Vector2Int(0, 7), new Cell { } },
			{ new Vector2Int(0, 8), new Cell { } },
			{ new Vector2Int(0, 9), new Cell { } },

			{ new Vector2Int(1, 0), new Cell { } },
			{ new Vector2Int(1, 1), new Cell { } },
			{ new Vector2Int(1, 2), new Cell { } },
			{ new Vector2Int(1, 3), new Cell { } },
			{ new Vector2Int(1, 4), new Cell { } },
			{ new Vector2Int(1, 5), new Cell { } },
			{ new Vector2Int(1, 6), new Cell { } },
			{ new Vector2Int(1, 7), new Cell { } },
			{ new Vector2Int(1, 8), new Cell { } },
			{ new Vector2Int(1, 9), new Cell { } },

			{ new Vector2Int(2, 0), new Cell { } },
			{ new Vector2Int(2, 1), new Cell { } },
			{ new Vector2Int(2, 2), new Cell { } },
			{ new Vector2Int(2, 3), new Cell { } },
			{ new Vector2Int(2, 4), new Cell { } },
			{ new Vector2Int(2, 5), new Cell { } },
			{ new Vector2Int(2, 6), new Cell { } },
			{ new Vector2Int(2, 7), new Cell { } },
			{ new Vector2Int(2, 8), new Cell { } },
			{ new Vector2Int(2, 9), new Cell { } },

			{ new Vector2Int(3, 0), new Cell { } },
			{ new Vector2Int(3, 1), new Cell { } },
			{ new Vector2Int(3, 2), new Cell { } },
			{ new Vector2Int(3, 3), new Cell { } },
			{ new Vector2Int(3, 4), new Cell { } },
			{ new Vector2Int(3, 5), new Cell { } },
			{ new Vector2Int(3, 6), new Cell { } },
			{ new Vector2Int(3, 7), new Cell { } },
			{ new Vector2Int(3, 8), new Cell { } },
			{ new Vector2Int(3, 9), new Cell { } },

			{ new Vector2Int(4, 0), new Cell { } },
			{ new Vector2Int(4, 1), new Cell { } },
			{ new Vector2Int(4, 2), new Cell { } },
			{ new Vector2Int(4, 3), new Cell { } },
			{ new Vector2Int(4, 4), new Cell { } },
			{ new Vector2Int(4, 5), new Cell { } },
			{ new Vector2Int(4, 6), new Cell { } },
			{ new Vector2Int(4, 7), new Cell { } },
			{ new Vector2Int(4, 8), new Cell { } },
			{ new Vector2Int(4, 9), new Cell { } },

			{ new Vector2Int(5, 0), new Cell { } },
			{ new Vector2Int(5, 1), new Cell { } },
			{ new Vector2Int(5, 2), new Cell { } },
			{ new Vector2Int(5, 3), new Cell { } },
			{ new Vector2Int(5, 4), new Cell { } },
			{ new Vector2Int(5, 5), new Cell { } },
			{ new Vector2Int(5, 6), new Cell { } },
			{ new Vector2Int(5, 7), new Cell { } },
			{ new Vector2Int(5, 8), new Cell { } },
			{ new Vector2Int(5, 9), new Cell { Structure = 99 } },
		};
	}
}
