using Sirenix.OdinInspector;
using UnityEngine;

namespace GameJam.Core
{
	public class CellContent : MonoBehaviour
	{
		[SerializeField] [Required] private SpriteRenderer _renderer;

		public void Initialize(int id)
		{
			_renderer.sprite = Resources.Load<Sprite>($"Art/Sprites/CellContent{id}");
		}
	}
}
