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
		public event Action<(int, int)> AvailableStructureQuantityChanged;

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
			_selectedStructureIndex = 0;
			_highlightedCell = null;
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
				if (_highlightedCell?.CanDestroy() == true)
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
			AvailableStructureQuantityChanged?.Invoke((data.Id, _structuresAvailable[data]));
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
			AvailableStructureQuantityChanged?.Invoke((data.Id, _structuresAvailable[data]));
		}

		private void LoadLevel(Level level)
		{
			{
				DestroyBoard();

				foreach (var cell in level.Board)
				{
					var position = cell.Key;
					Board.Add(position, SpawnCell(position, cell.Value));
				}
			}

			{
				// Make sure to clone or we will mutate the scriptable object !
				_structuresAvailable = level.Structures.ToDictionary(entry => entry.Key, entry => entry.Value); ;
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

		private CellComponent SpawnCell(Vector2Int position, Cell data)
		{
			var localPosition = new Vector3(position.y, position.x, 0f);

			var cell = Instantiate(GameSettings.Instance.CellPrefab, transform);
			cell.transform.localPosition = localPosition;
			cell.name = $"Cell [{position.x},{position.y}]";
			cell.Initialize(position, data);

			// Cells spawned by the level are indestructible
			if (data.Structure > -1)
			{
				cell.gameObject.AddComponent<IndestructibleFlag>();
			}

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
