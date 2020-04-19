using System.Collections.Generic;
using UnityEngine;

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
		}

		private void OnDisable()
		{
			_boardManager.AvailableStructuresChanged -= OnAvailableStructuresChanged;

			DestroyButtons();
		}
		private void OnAvailableStructuresChanged(Dictionary<Structure, int> structures)
		{
			foreach (var item in structures)
			{
				_buttons.TryGetValue(item.Key.Id, out var button);
				if (button != null)
				{
					button.SetQuantity(item.Value);
				}
				else
				{
					SpawnButton(item.Key, item.Value);
				}
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
			button.Initialize(data, quantity, () => { _boardManager.SelectStructure(data.Id); });
			_buttons.Add(data.Id, button);
		}
	}
}
