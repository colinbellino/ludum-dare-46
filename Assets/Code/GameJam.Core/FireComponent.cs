using Sirenix.OdinInspector;
using UnityEngine;

namespace GameJam.Core
{
	public class FireComponent : MonoBehaviour
	{
		[SerializeField] [Required] private SpriteRenderer _renderer;
		[SerializeField] [Required] private Sprite[] _fireSprites;

		public int Amount { get; private set; }

		public void Initialize(int fireAmount)
		{
			Amount = fireAmount;
			UpdateFireRenderer();
		}

		private void UpdateFireRenderer()
		{
			if (Amount > 0)
			{
				_renderer.sprite = _fireSprites[0];
			}
			else
			{
				_renderer.sprite = null;
			}
		}

		public void Kindle()
		{
			Amount += 1;
			UpdateFireRenderer();
		}

		public void Extinguish()
		{
			Amount = 0;
			UpdateFireRenderer();
		}
	}
}
