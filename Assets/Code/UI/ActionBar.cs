using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GameJam.Core
{
	public class ActionBar : MonoBehaviour
	{
		[SerializeField] private StructureButton _actionButtonPrefab;
		[SerializeField] private BoardManager _boardManager;

		private List<StructureButton> _buttons = new List<StructureButton>();

		private void OnEnable()
		{
			_boardManager.AvailableStructuresChanged += OnAvailableStructuresChanged;
		}

		private void OnDisable()
		{
			_boardManager.AvailableStructuresChanged -= OnAvailableStructuresChanged;

			foreach (var button in _buttons)
			{
				Destroy(button.gameObject);
			}

			_buttons.Clear();
		}

		private void OnAvailableStructuresChanged(Dictionary<Structure, int> structures)
		{
			foreach (var item in structures)
			{
				SpawnButton(item.Key, item.Value);
			}
		}

		private void SpawnButton(Structure data, int quantity)
		{
			var button = Instantiate(_actionButtonPrefab, transform);
			button.Initialize(data, quantity, () => { _boardManager.SelectStructure(data.Id); });
			_buttons.Add(button);
		}
	}
}
