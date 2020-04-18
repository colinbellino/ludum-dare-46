using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameJam.Core
{
	public class BoardManager : SerializedMonoBehaviour
	{
		// Authoring
		private Dictionary<Vector2Int, CellAuthoring> _level = new Dictionary<Vector2Int, CellAuthoring> {
			{ new Vector2Int(0, 0), new CellAuthoring { Content = -1, Type = 1 } },
			{ new Vector2Int(0, 1), new CellAuthoring { Content = -1, Type = 1 } },
			{ new Vector2Int(0, 2), new CellAuthoring { Content = -1, Type = 1 } },
			{ new Vector2Int(1, 0), new CellAuthoring { Content = -1, Type = 0 } },
			{ new Vector2Int(1, 1), new CellAuthoring { Content = 0, Type = 0 } },
			{ new Vector2Int(1, 2), new CellAuthoring { Content = -1, Type = 0 } },
			{ new Vector2Int(2, 0), new CellAuthoring { Content = -1, Type = 0 } },
			{ new Vector2Int(2, 1), new CellAuthoring { Content = -1, Type = 0 } },
			{ new Vector2Int(2, 2), new CellAuthoring { Content = -1, Type = 0 } },
		};

		// Runtime
		private Dictionary<Vector2Int, Cell> _board = new Dictionary<Vector2Int, Cell>();

		private void Awake()
		{
			PrepareBoard();
		}

		private void PrepareBoard()
		{
			foreach (var cell in _level)
			{
				var position = cell.Key;
				_board.Add(position, SpawnCell(position, cell.Value));
			}
		}

		private Cell SpawnCell(Vector2Int position, CellAuthoring authoringData)
		{
			var prefab = Resources.Load<GameObject>("Prefabs/Cell");
			var worldPosition = new Vector3(position.y, position.x, 0f);
			var instance = GameObject.Instantiate(prefab, worldPosition, Quaternion.identity);
			var cell = instance.GetComponent<Cell>();

			cell.Initialize(authoringData);

			return cell;
		}

		[Button]
		private void PlacePlant(Vector2Int position)
		{
			_board[position].SetContent(0);
		}
	}

	public class CellAuthoring
	{
		public int Content = -1;
		public int Type;
	}
}
