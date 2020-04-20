using Sirenix.OdinInspector;
using UnityEngine;

namespace GameJam.Core
{
	public class FireComponent : MonoBehaviour
	{
		[SerializeField] [Required] private SpriteRenderer _renderer;
		[SerializeField] [Required] private Animator _animator;

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
				_animator.enabled = true;
			}
			else
			{
				_animator.enabled = false;
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
