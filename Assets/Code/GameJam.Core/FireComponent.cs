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
				var index = Mathf.Min(Amount - 1, _fireSprites.Length - 1);
				_renderer.sprite = _fireSprites[index];
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
