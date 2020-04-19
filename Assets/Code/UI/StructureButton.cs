using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameJam.Core
{
	public class StructureButton : MonoBehaviour
	{
		[SerializeField] [Required] private Button _button;
		[SerializeField] [Required] private TextMeshProUGUI _name;
		[SerializeField] [Required] private TextMeshProUGUI _quantity;
		[SerializeField] [Required] private Image _image;

		public void Initialize(Structure data, int quantity, Action _onActionClick)
		{
			_name.text = data.Name;
			_image.sprite = data.Sprite;
			_quantity.text = quantity.ToString();
			_button.onClick.AddListener(() => _onActionClick?.Invoke());

			_button.interactable = quantity > 0;
		}

		public void SetQuantity(int quantity)
		{
			_quantity.text = quantity.ToString();
		}
	}
}
