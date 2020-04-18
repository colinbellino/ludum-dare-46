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
		public Dictionary<Vector2Int, Cell> Board { get; } = new Dictionary<Vector2Int, Cell>();
		private EventSystem _eventSystem;
		private Camera _camera;
		private Cell _highlightedCell;


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
					var content = _highlightedCell.Content == null ? 0 : -1; // -1 to clear the content
					_highlightedCell.SetContent(content);
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
