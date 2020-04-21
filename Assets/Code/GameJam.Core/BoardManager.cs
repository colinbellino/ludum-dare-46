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
		[SerializeField] [Required] private AudioSource _audioSource;
		[SerializeField] [Required] private AudioClip _cantPlaceClip;
		[SerializeField] [Required] private AudioClip _placeClip;
		[SerializeField] [Required] private AudioClip _removeClip;
		[SerializeField] [Required] private SpriteRenderer _marker;

		// Runtime
		private List<Level> _levels;
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
			_levels = FindObjectOfType<GameStateMachine>().GameSettings.AllLevels;
			_eventSystem = EventSystem.current;
			_camera = Camera.main;
		}

		private void Update()
		{
			if (_interactive)
			{
				_highlightedCell = GetCellUnderMouseCursor();

				HandleInputs();

				if (_highlightedCell)
				{
					_marker.transform.localPosition = new Vector3(
						_highlightedCell.Position.y,
						_highlightedCell.Position.x,
						0f
					);
					_marker.color = _highlightedCell?.HasComponent<IndestructibleFlag>() == true ? Color.red : Color.blue;
					_marker.sortingOrder = _highlightedCell.SortingOrder - 1;
				}
				else
				{
					_marker.transform.localPosition = new Vector3(99999, 0, 0);
				}
			}
		}

		public void Activate()
		{
			_currentLevel = _levels[_currentLevelIndex % _levels.Count];
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
			var data = FindObjectOfType<GameStateMachine>().GameSettings.AllStructures.Find(structure => structure.Id == id);
			_selectedStructureIndex = FindObjectOfType<GameStateMachine>().GameSettings.AllStructures.FindIndex(structure => structure.Id == id);
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
				else
				{
					_audioSource.PlayOneShot(_placeClip);
				}
			}

			if (Mouse.current.rightButton.wasPressedThisFrame)
			{
				var canDestroyCell = _highlightedCell?.CanDestroy() == true;
				if (canDestroyCell)
				{
					DestroyHightlightedStructure();
					_audioSource.PlayOneShot(_removeClip);
				}
				else
				{
					_audioSource.PlayOneShot(_cantPlaceClip);
				}
			}
		}

		private void DestroyHightlightedStructure()
		{
			var data = FindObjectOfType<GameStateMachine>().GameSettings.AllStructures.Find(structure => structure.Id == _highlightedCell.StructureId);
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

			var data = FindObjectOfType<GameStateMachine>().GameSettings.AllStructures[_selectedStructureIndex];
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

		public bool NextLevelIndex()
		{
			_currentLevelIndex++;

			return _currentLevelIndex == _levels.Count;
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

			var cell = Instantiate(FindObjectOfType<GameStateMachine>().GameSettings.CellPrefab, transform);
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
