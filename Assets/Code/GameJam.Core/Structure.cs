using Sirenix.OdinInspector;
using UnityEngine;

namespace GameJam.Core
{
	public class Structure : MonoBehaviour
	{
		[SerializeField] [Required] private SpriteRenderer _renderer;
		[SerializeField] [Required] private SpriteRenderer _fireRenderer;

		public int Fire { get; private set; }

		public void Initialize(int id)
		{
			_renderer.sprite = Resources.Load<Sprite>($"Art/Sprites/CellContent{id}");
		}

		public void SetFire(int fireLevel)
		{
			Fire = fireLevel;

			UpdateFireRender();
		}

		public void AddFire(int amount)
		{
			Fire += amount;

			UpdateFireRender();
		}

		private void UpdateFireRender()
		{
			if (Fire > 0)
			{
				_fireRenderer.sprite = Resources.Load<Sprite>($"Art/Sprites/Fire");
			}
			else
			{
				_fireRenderer.sprite = null;
			}
		}
	}
}
