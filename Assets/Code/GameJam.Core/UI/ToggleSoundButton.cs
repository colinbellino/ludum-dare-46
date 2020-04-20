using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameJam.Core
{
	public class ToggleSoundButton : MonoBehaviour
	{
		[SerializeField] private Image _image;
		[SerializeField] private Sprite _muteSprite;
		[SerializeField] private Sprite _unmuteSprite;

		private bool _isMuted;


		public void Toggle()
		{
			_image.sprite = _isMuted ? _unmuteSprite : _muteSprite;
			_isMuted = !_isMuted;
		}
	}
}
