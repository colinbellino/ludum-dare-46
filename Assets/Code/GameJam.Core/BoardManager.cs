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
			GenerateBoard(_currentLevel);

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
					var structureToPlace = 0;
					if (_highlightedCell.Structure)
					{
						_highlightedCell.DestroyStructure();
					}
					else
					{

						_highlightedCell.PlaceStructure(structureToPlace);
					}
				}
			}
		}

		private void GenerateBoard(Level level)
		{
			foreach (var cell in level.Board)
			{
				var position = cell.Key;
				Board.Add(position, GenerateCell(position, cell.Value));
			}
		}

		private CellComponent GenerateCell(Vector2Int position, Cell cellData)
		{
			var worldPosition = new Vector3(position.y, position.x, 0f);

			var instance = Instantiate(GameSettings.Instance.CellPrefab, worldPosition, Quaternion.identity);
			instance.name = $"Cell [{position.x},{position.y}]";

			var cell = instance.GetComponent<CellComponent>();
			cell.Initialize(position, cellData);

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
