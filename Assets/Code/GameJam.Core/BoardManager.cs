using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace GameJam.Core
{
	public class BoardManager : MonoBehaviour
	{
		// Authoring
		[SerializeField] [Required] private Level _currentLevel;

		// Runtime
		private EventSystem _eventSystem;
		private Camera _camera;
		public Dictionary<Vector2Int, CellComponent> Board { get; } = new Dictionary<Vector2Int, CellComponent>();
		private Dictionary<Structure, int> _structuresAvailable;
		private CellComponent _highlightedCell;
		private int _selectedStructureIndex;

		public event Action<Dictionary<Structure, int>> AvailableStructuresChanged;

		private void Awake()
		{
			_eventSystem = EventSystem.current;
			_camera = Camera.main;
		}

		public void Activate()
		{
			LoadLevel(_currentLevel);
		}

		public void Deactivate()
		{
			DestroyBoard();
		}

		public void SelectStructure(int id)
		{
			var data = GameSettings.Instance.AllStructures.Find(structure => structure.Id == id);
			if (_structuresAvailable[data] <= 1)
			{
				Debug.LogWarning($"You can't select \"{data.Name}\" DOOD.");
				return;
			}

			_selectedStructureIndex = GameSettings.Instance.AllStructures.FindIndex(structure => structure.Id == id);
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
				if (_highlightedCell?.CanConstruct() == true)
				{
					PlaceSelectedStructure();
				}
			}

			if (Mouse.current.rightButton.wasPressedThisFrame)
			{
				if (_highlightedCell?.HasStructure() == true)
				{
					DestroyHightlightedStructure();
				}
			}
		}

		private void DestroyHightlightedStructure()
		{
			var data = GameSettings.Instance.AllStructures.Find(structure => structure.Id == _highlightedCell.StructureId);
			_highlightedCell.DestroyStructure();

			_structuresAvailable[data] += 1;
			AvailableStructuresChanged?.Invoke(_structuresAvailable);
		}

		private void PlaceSelectedStructure()
		{
			var data = GameSettings.Instance.AllStructures[_selectedStructureIndex];
			if (_structuresAvailable[data] <= 0)
			{
				Debug.LogWarning($"You don't have any \"{data.Name}\" DOOD.");
				return;
			}

			_highlightedCell.PlaceStructure(data);

			_structuresAvailable[data] -= 1;
			AvailableStructuresChanged?.Invoke(_structuresAvailable);
		}

		private void LoadLevel(Level level)
		{
			{
				DestroyBoard();

				var tempBoard = new Dictionary<Vector2Int, Cell> {
					{ new Vector2Int(0, 0), new Cell { Terrain = 1, Structure = 0, Fire = 1 } },
					{ new Vector2Int(0, 1), new Cell { Terrain = 0, } },
					{ new Vector2Int(0, 2), new Cell { Terrain = 0, } },
					{ new Vector2Int(0, 3), new Cell { Terrain = 0, } },
					{ new Vector2Int(0, 4), new Cell { Terrain = 0, Structure = 0 } },
					{ new Vector2Int(0, 5), new Cell { Terrain = 0, Structure = 0 } },
					{ new Vector2Int(0, 6), new Cell { Terrain = 0, Structure = 0 } },
					{ new Vector2Int(0, 7), new Cell { Terrain = 0, Structure = 0 } },
					{ new Vector2Int(0, 8), new Cell { Terrain = 0, Structure = 0 } },
					{ new Vector2Int(0, 9), new Cell { Terrain = 0, Structure = 0 } },

					{ new Vector2Int(1, 0), new Cell { Terrain = 1, } },
					{ new Vector2Int(1, 1), new Cell { Terrain = 1, } },
					{ new Vector2Int(1, 2), new Cell { Terrain = 1, } },
					{ new Vector2Int(1, 3), new Cell { Terrain = 1, } },
					{ new Vector2Int(1, 4), new Cell { Terrain = 1, Structure = 0 } },
					{ new Vector2Int(1, 5), new Cell { Terrain = 1, Structure = 0 } },
					{ new Vector2Int(1, 6), new Cell { Terrain = 1, Structure = 0 } },
					{ new Vector2Int(1, 7), new Cell { Terrain = 1, Structure = 0 } },
					{ new Vector2Int(1, 8), new Cell { Terrain = 1, Structure = 0 } },
					{ new Vector2Int(1, 9), new Cell { Terrain = 1, Structure = 0 } },

					{ new Vector2Int(2, 0), new Cell { Terrain = 1, } },
					{ new Vector2Int(2, 1), new Cell { Terrain = 1, } },
					{ new Vector2Int(2, 2), new Cell { Terrain = 1, Structure = 0 } },
					{ new Vector2Int(2, 3), new Cell { Terrain = 1, } },
					{ new Vector2Int(2, 4), new Cell { Terrain = 1, } },
					{ new Vector2Int(2, 5), new Cell { Terrain = 1, } },
					{ new Vector2Int(2, 6), new Cell { Terrain = 1, } },
					{ new Vector2Int(2, 7), new Cell { Terrain = 1, } },
					{ new Vector2Int(2, 8), new Cell { Terrain = 1, } },
					{ new Vector2Int(2, 9), new Cell { Terrain = 1, } },

					{ new Vector2Int(3, 0), new Cell { Terrain = 1, } },
					{ new Vector2Int(3, 1), new Cell { Terrain = 1, } },
					{ new Vector2Int(3, 2), new Cell { Terrain = 1, Structure = 0 } },
					{ new Vector2Int(3, 3), new Cell { Terrain = 1, } },
					{ new Vector2Int(3, 4), new Cell { Terrain = 1, } },
					{ new Vector2Int(3, 5), new Cell { Terrain = 1, } },
					{ new Vector2Int(3, 6), new Cell { Terrain = 1, } },
					{ new Vector2Int(3, 7), new Cell { Terrain = 1, } },
					{ new Vector2Int(3, 8), new Cell { Terrain = 1, } },
					{ new Vector2Int(3, 9), new Cell { Terrain = 1, } },

					{ new Vector2Int(4, 0), new Cell { Terrain = 1, } },
					{ new Vector2Int(4, 1), new Cell { Terrain = 1, } },
					{ new Vector2Int(4, 2), new Cell { Terrain = 1, Structure = 0 } },
					{ new Vector2Int(4, 3), new Cell { Terrain = 1, Structure = 0 } },
					{ new Vector2Int(4, 4), new Cell { Terrain = 1 } },
					{ new Vector2Int(4, 5), new Cell { Terrain = 1 } },
					{ new Vector2Int(4, 6), new Cell { Terrain = 1 } },
					{ new Vector2Int(4, 7), new Cell { Terrain = 1 } },
					{ new Vector2Int(4, 8), new Cell { Terrain = 1 } },
					{ new Vector2Int(4, 9), new Cell { Terrain = 1 } },

					{ new Vector2Int(5, 0), new Cell { Terrain = 1, } },
					{ new Vector2Int(5, 1), new Cell { Terrain = 1, } },
					{ new Vector2Int(5, 2), new Cell { Terrain = 1, Structure = 0 } },
					{ new Vector2Int(5, 3), new Cell { Terrain = 1, Structure = 0 } },
					{ new Vector2Int(5, 4), new Cell { Terrain = 1 } },
					{ new Vector2Int(5, 5), new Cell { Terrain = 1 } },
					{ new Vector2Int(5, 6), new Cell { Terrain = 1 } },
					{ new Vector2Int(5, 7), new Cell { Terrain = 1 } },
					{ new Vector2Int(5, 8), new Cell { Terrain = 1 } },
					{ new Vector2Int(5, 9), new Cell { Terrain = 1, Structure = 1 } },
				};
				// foreach (var cell in level.Board)
				foreach (var cell in tempBoard)
				{
					var position = cell.Key;
					Board.Add(position, GenerateCell(position, cell.Value));
				}
			}

			{
				// Make sure to clone or we will mutate the scriptable object !
				_structuresAvailable = _currentLevel.Structures.ToDictionary(entry => entry.Key, entry => entry.Value); ;
				AvailableStructuresChanged?.Invoke(_structuresAvailable);
			}
		}

		private void DestroyBoard()
		{
			foreach (var item in Board)
			{
				Destroy(item.Value.gameObject);
			}

			Board.Clear();
		}

		private CellComponent GenerateCell(Vector2Int position, Cell data)
		{
			var localPosition = new Vector3(position.y, position.x, 0f);

			var cell = Instantiate(GameSettings.Instance.CellPrefab, transform);
			cell.transform.localPosition = localPosition;
			cell.name = $"Cell [{position.x},{position.y}]";
			cell.Initialize(position, data);

			return cell;
		}

		private CellComponent GetCellUnderMouseCursor()
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

			return hit.transform.GetComponent<CellComponent>();
		}
	}
}
