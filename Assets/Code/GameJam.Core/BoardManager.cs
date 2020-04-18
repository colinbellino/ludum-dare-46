using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace GameJam.Core
{
	public class BoardManager : MonoBehaviour
	{
		// Runtime
		public Dictionary<Vector2Int, Cell> Board { get; } = new Dictionary<Vector2Int, Cell>();
		private EventSystem _eventSystem;
		private Camera _camera;
		private Cell _highlightedCell;

		private void Awake()
		{
			// Load level data
			var level = new Dictionary<Vector2Int, CellData> {
				{ new Vector2Int(0, 0), new CellData { Content = 0, Type = 0, Fire = 1 } },
				{ new Vector2Int(0, 1), new CellData { Content = -1, Type = 0 } },
				{ new Vector2Int(0, 2), new CellData { Content = -1, Type = 0 } },
				{ new Vector2Int(0, 3), new CellData { Content = -1, Type = 0 } },
				{ new Vector2Int(0, 4), new CellData { Content = 0, Type = 0 } },
				{ new Vector2Int(1, 0), new CellData { Content = -1, Type = 0 } },
				{ new Vector2Int(1, 1), new CellData { Content = -1, Type = 1 } },
				{ new Vector2Int(1, 2), new CellData { Content = -1, Type = 1 } },
				{ new Vector2Int(1, 3), new CellData { Content = -1, Type = 1 } },
				{ new Vector2Int(1, 4), new CellData { Content = 0, Type = 0 } },
				{ new Vector2Int(2, 0), new CellData { Content = -1, Type = 0 } },
				{ new Vector2Int(2, 1), new CellData { Content = -1, Type = 1 } },
				{ new Vector2Int(2, 2), new CellData { Content = 0, Type = 1 } },
				{ new Vector2Int(2, 3), new CellData { Content = -1, Type = 1 } },
				{ new Vector2Int(2, 4), new CellData { Content = -1, Type = 0 } },
				{ new Vector2Int(3, 0), new CellData { Content = -1, Type = 0 } },
				{ new Vector2Int(3, 1), new CellData { Content = -1, Type = 1 } },
				{ new Vector2Int(3, 2), new CellData { Content = 0, Type = 1 } },
				{ new Vector2Int(3, 3), new CellData { Content = -1, Type = 1 } },
				{ new Vector2Int(3, 4), new CellData { Content = -1, Type = 0 } },
				{ new Vector2Int(4, 0), new CellData { Content = -1, Type = 0 } },
				{ new Vector2Int(4, 1), new CellData { Content = -1, Type = 0 } },
				{ new Vector2Int(4, 2), new CellData { Content = 0, Type = 0 } },
				{ new Vector2Int(4, 3), new CellData { Content = 0, Type = 0 } },
				{ new Vector2Int(4, 4), new CellData { Content = 1, Type = 0 } },
			};

			GenerateBoard(level);

			_eventSystem = EventSystem.current;
			_camera = Camera.main;
		}

		private void Update()
		{
			_highlightedCell = GetCellUnderMouseCursor();

			HandleInputs();
		}

		private void HandleInputs()
		{
			if (Mouse.current.leftButton.wasPressedThisFrame)
			{
				if (_highlightedCell)
				{
					var content = _highlightedCell.Content == null ? 0 : -1; // -1 to clear the content
					_highlightedCell.SetContent(content);
				}
			}
		}

		private void GenerateBoard(Dictionary<Vector2Int, CellData> level)
		{
			foreach (var cell in level)
			{
				var position = cell.Key;
				Board.Add(position, GenerateCell(position, cell.Value));
			}
		}

		private Cell GenerateCell(Vector2Int position, CellData authoringData)
		{
			var prefab = Resources.Load<GameObject>("Prefabs/Cell");
			var worldPosition = new Vector3(position.y, position.x, 0f);

			var instance = Instantiate(prefab, worldPosition, Quaternion.identity);
			instance.name = $"Cell [{position.x},{position.y}]";

			var cell = instance.GetComponent<Cell>();
			cell.Initialize(position, authoringData);

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
}
