using System.Collections.Generic;
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
		public Dictionary<Vector2Int, CellComponent> Board { get; } = new Dictionary<Vector2Int, CellComponent>();
		private EventSystem _eventSystem;
		private Camera _camera;
		private CellComponent _highlightedCell;

		private void Awake()
		{
			_eventSystem = EventSystem.current;
			_camera = Camera.main;
		}

		public void Activate()
		{
			GenerateBoard(_currentLevel);
		}

		public void Deactivate()
		{
			DestroyBoard();
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
					if (_highlightedCell.HasStructure())
					{
						_highlightedCell.DestroyStructure();
					}
					else
					{
						var data = GameSettings.Instance.AllStructures.Find(structure => structure.Id == 0);
						_highlightedCell.PlaceStructure(data);
					}
				}
			}
		}

		private void GenerateBoard(Level level)
		{
			DestroyBoard();
			foreach (var cell in level.Board)
			{
				var position = cell.Key;
				Board.Add(position, GenerateCell(position, cell.Value));
			}
		}

		private void DestroyBoard()
		{
			foreach (var item in Board)
			{
				GameObject.DestroyImmediate(item.Value.gameObject);
			}

			Board.Clear();
		}

		private CellComponent GenerateCell(Vector2Int position, Cell data)
		{
			var worldPosition = new Vector3(position.y, position.x, 0f);

			var cell = Instantiate(GameSettings.Instance.CellPrefab, worldPosition, Quaternion.identity);
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
