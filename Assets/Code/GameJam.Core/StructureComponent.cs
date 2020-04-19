using Sirenix.OdinInspector;
using UnityEngine;

namespace GameJam.Core
{
	public class StructureComponent : MonoBehaviour
	{
		[SerializeField] [Required] private SpriteRenderer _renderer;
		[SerializeField] [Required] private SpriteRenderer _fireRenderer;

		public int Fire { get; private set; }

		public void Initialize(Structure data)
		{
			name = $"Content ({data.Name})";

			_renderer.sprite = data.Sprite;
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
				// TODO: Move the fire handling to the cell level
				_fireRenderer.sprite = GameSettings.Instance.FireSprite;
			}
			else
			{
				_fireRenderer.sprite = null;
			}
		}
	}
}
