using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace GameJam.Core
{
	public class BoardManager : SerializedMonoBehaviour
	{
		// Authoring
		[SerializeField]
		[Required]
		private Camera _camera;

		private Dictionary<Vector2Int, CellAuthoring> _level = new Dictionary<Vector2Int, CellAuthoring> {
			{ new Vector2Int(0, 0), new CellAuthoring { Content = 0, Type = 0 } },
			{ new Vector2Int(0, 1), new CellAuthoring { Content = 0, Type = 0 } },
			{ new Vector2Int(0, 2), new CellAuthoring { Content = 0, Type = 0 } },
			{ new Vector2Int(0, 3), new CellAuthoring { Content = 0, Type = 0 } },
			{ new Vector2Int(0, 4), new CellAuthoring { Content = 0, Type = 0 } },
			{ new Vector2Int(1, 0), new CellAuthoring { Content = 0, Type = 0 } },
			{ new Vector2Int(1, 1), new CellAuthoring { Content = 0, Type = 1 } },
			{ new Vector2Int(1, 2), new CellAuthoring { Content = 0, Type = 1 } },
			{ new Vector2Int(1, 3), new CellAuthoring { Content = 0, Type = 1 } },
			{ new Vector2Int(1, 4), new CellAuthoring { Content = 0, Type = 0 } },
			{ new Vector2Int(2, 0), new CellAuthoring { Content = 0, Type = 0 } },
			{ new Vector2Int(2, 1), new CellAuthoring { Content = 0, Type = 1 } },
			{ new Vector2Int(2, 2), new CellAuthoring { Content = 0, Type = 1 } },
			{ new Vector2Int(2, 3), new CellAuthoring { Content = 0, Type = 1 } },
			{ new Vector2Int(2, 4), new CellAuthoring { Content = 0, Type = 0 } },
			{ new Vector2Int(3, 0), new CellAuthoring { Content = 0, Type = 0 } },
			{ new Vector2Int(3, 1), new CellAuthoring { Content = 0, Type = 1 } },
			{ new Vector2Int(3, 2), new CellAuthoring { Content = 0, Type = 1 } },
			{ new Vector2Int(3, 3), new CellAuthoring { Content = 0, Type = 1 } },
			{ new Vector2Int(3, 4), new CellAuthoring { Content = 0, Type = 0 } },
			{ new Vector2Int(4, 0), new CellAuthoring { Content = 0, Type = 0 } },
			{ new Vector2Int(4, 1), new CellAuthoring { Content = 0, Type = 0 } },
			{ new Vector2Int(4, 2), new CellAuthoring { Content = 0, Type = 0 } },
			{ new Vector2Int(4, 3), new CellAuthoring { Content = 0, Type = 0 } },
			{ new Vector2Int(4, 4), new CellAuthoring { Content = 0, Type = 0 } },
		};

		// Runtime
		private Dictionary<Vector2Int, Cell> _board = new Dictionary<Vector2Int, Cell>();
		private EventSystem _eventSystem;
		private Cell _highlightedCell;

		private void Awake()
		{
			PrepareBoard();

			_eventSystem = EventSystem.current;
		}

		private void Update()
		{
			_highlightedCell = GetCellUnderMouseCursor();

			if (Mouse.current.leftButton.wasPressedThisFrame)
			{
				_highlightedCell?.SetContent(1);
			}
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

			var instance = Instantiate(prefab, worldPosition, Quaternion.identity);
			instance.name = $"Cell [{position.x},{position.y}]";

			var cell = instance.GetComponent<Cell>();
			cell.Initialize(authoringData);

			return cell;
		}

		private Cell GetCellUnderMouseCursor()
		{
			var mousePosition = Mouse.current.position.ReadValue();

			var ray = _camera.ScreenPointToRay(mousePosition);
			if (!Physics.Raycast(ray, out var hit, maxDistance: 100f))
			{
				return null;
			}

			if (_eventSystem.IsPointerOverGameObject())
			{
				return null;
			}

			return hit.transform.GetComponent<Cell>();
		}
	}

	public class CellAuthoring
	{
		public int Content;
		public int Type;
	}
}
