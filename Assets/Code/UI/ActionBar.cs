using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameJam.Core
{
	public class ActionBar : MonoBehaviour
	{
		[SerializeField] private StructureButton _actionButtonPrefab;
		[SerializeField] private BoardManager _boardManager;

		private Dictionary<int, StructureButton> _buttons = new Dictionary<int, StructureButton>();

		private void OnEnable()
		{
			_boardManager.AvailableStructuresChanged += OnAvailableStructuresChanged;
			_boardManager.AvailableStructureQuantityChanged += OnAvailableStructureQuantityChanged;
		}

		private void OnDisable()
		{
			_boardManager.AvailableStructuresChanged -= OnAvailableStructuresChanged;
			_boardManager.AvailableStructureQuantityChanged -= OnAvailableStructureQuantityChanged;

			DestroyButtons();
		}

		private void OnAvailableStructuresChanged(Dictionary<Structure, int> structures)
		{
			foreach (var item in structures)
			{
				SpawnButton(item.Key, item.Value);
			}

			EventSystem.current.SetSelectedGameObject(_buttons.First().Value.gameObject);
		}

		private void OnAvailableStructureQuantityChanged((int, int) tuple)
		{
			var structureId = tuple.Item1;
			var quantity = tuple.Item2;

			_buttons.TryGetValue(structureId, out var button);
			if (button != null)
			{
				button.UpdateState(quantity);
				EventSystem.current.SetSelectedGameObject(button.gameObject);
			}
		}

		private void DestroyButtons()
		{
			foreach (var item in _buttons)
			{
				Destroy(item.Value.gameObject);
			}

			_buttons.Clear();
		}

		private void SpawnButton(Structure data, int quantity)
		{
			var button = Instantiate(_actionButtonPrefab, transform);
			button.Initialize(data, quantity, () =>
			{
				_boardManager.SelectStructure(data.Id);
			});
			_buttons.Add(data.Id, button);

			button.UpdateState(quantity);
		}
	}
}
