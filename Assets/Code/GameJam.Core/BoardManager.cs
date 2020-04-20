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
		[SerializeField] [Required] private List<Level> _levelsList;
		[SerializeField] [Required] private AudioSource _audioSource;
		[SerializeField] [Required] private AudioClip _cantPlaceClip;

		// Runtime
		private EventSystem _eventSystem;
		private Camera _camera;
		public Dictionary<Vector2Int, CellComponent> Board { get; } = new Dictionary<Vector2Int, CellComponent>();
		private Dictionary<Structure, int> _structuresAvailable;
		private CellComponent _highlightedCell;
		private int _selectedStructureIndex;
		private Level _currentLevel;
		private int _currentLevelIndex = 0;
		private bool _interactive;

		public event Action<Dictionary<Structure, int>> AvailableStructuresChanged;
		public event Action<(int, int)> AvailableStructureQuantityChanged;

		private void Awake()
		{
			_eventSystem = EventSystem.current;
			_camera = Camera.main;
		}

		private void Update()
		{
			if (_interactive)
			{
				_highlightedCell = GetCellUnderMouseCursor();
				HandleInputs();
			}
		}

		public void Activate()
		{
			_currentLevel = _levelsList[_currentLevelIndex % _levelsList.Count];
			LoadLevel(_currentLevel);
			_interactive = true;
		}

		public void DisableInteractions()
		{
			_interactive = false;
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
			_selectedStructureIndex = GameSettings.Instance.AllStructures.FindIndex(structure => structure.Id == id);
		}

		private void HandleInputs()
		{
			if (_highlightedCell == null)
			{
				return;
			}

			if (Mouse.current.leftButton.wasPressedThisFrame)
			{
				var cantPlaceCell = PlaceSelectedStructure() == false;
				if (cantPlaceCell)
				{
					_audioSource.PlayOneShot(_cantPlaceClip);
				}
			}

			if (Mouse.current.rightButton.wasPressedThisFrame)
			{
				var canDestroyCell = _highlightedCell?.CanDestroy() == true;
				if (canDestroyCell)
				{
					DestroyHightlightedStructure();
				}
				else
				{
					_audioSource.PlayOneShot(_cantPlaceClip);
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

		private bool PlaceSelectedStructure()
		{
			if (_highlightedCell?.CanConstruct() == false)
			{
				return false;
			}

			var data = GameSettings.Instance.AllStructures[_selectedStructureIndex];
			if (_structuresAvailable[data] <= 0)
			{
				Debug.LogWarning($"You don't have any \"{data.Name}\" DOOD.");
				return false;
			}

			_highlightedCell.PlaceStructure(data);

			_structuresAvailable[data] -= 1;
			AvailableStructureQuantityChanged?.Invoke((data.Id, _structuresAvailable[data]));

			return true;
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

		public void NextLevelIndex()
		{
			_currentLevelIndex++;
		}

		public void ResetLevelIndex()
		{
			_currentLevelIndex = 0;
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
