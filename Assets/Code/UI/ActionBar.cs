using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameJam.Core
{
	public class ActionBar : MonoBehaviour
	{
		[SerializeField] private StructureButton _actionButtonPrefab;
		[SerializeField] private BoardManager _boardManager;

		private Dictionary<int, StructureButton> _buttons = new Dictionary<int, StructureButton>();
		private StructureButton _activeButton;

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

			_activeButton = _buttons.First().Value;

			UpdateActiveButton();
		}

		private void UpdateActiveButton()
		{
			foreach (var button in _buttons.Values)
			{
				if (button == _activeButton)
				{
					button.SetActive();
				}
				else
				{
					button.SetInactive();
				}
			}
		}

		private void OnAvailableStructureQuantityChanged((int, int) tuple)
		{
			var structureId = tuple.Item1;
			var quantity = tuple.Item2;

			_buttons.TryGetValue(structureId, out var button);
			if (button != null)
			{
				button.SetQuantity(quantity);
				_activeButton = button;
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
				_activeButton = button;
				_boardManager.SelectStructure(data.Id);
				UpdateActiveButton();
			});

			_buttons.Add(data.Id, button);
		}
	}
}
