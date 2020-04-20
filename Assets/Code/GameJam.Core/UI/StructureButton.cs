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
		[SerializeField] [Required] private GameObject _highlight;

		public void Initialize(Structure data, int quantity, Action _onActionClick)
		{
			_name.text = data.Name;
			_image.sprite = data.Sprite;
			_image.SetNativeSize();
			_quantity.text = quantity.ToString();
			_button.onClick.AddListener(() => _onActionClick?.Invoke());
		}

		public void SetQuantity(int quantity) => _quantity.text = quantity.ToString();

		public void SetActive() => _highlight.SetActive(true);

		public void SetInactive() => _highlight.SetActive(false);
	}
}
