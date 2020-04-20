using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameJam.Core
{
	public class MainMenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
	{
		[SerializeField] [Required] private GameObject _highlight;

		public void OnPointerEnter(PointerEventData eventData)
		{
			_highlight.SetActive(true);

			var position = _highlight.transform.position;
			position.y = transform.position.y;
			_highlight.transform.position = position;
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			// _highlight.SetActive(false);
		}
	}
}
