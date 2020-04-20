using UnityEngine;

namespace GameJam.Core
{
	public class ExitFlag : MonoBehaviour
	{
		private Animator _animator;

		private void OnEnable()
		{
			_animator = Instantiate(GameSettings.Instance.BonfireAnimatorPrefab, transform);
			_animator.transform.localPosition = new Vector3(0, -0.075f, 0);
			_animator.GetComponent<SpriteRenderer>().sortingOrder = GetComponentInChildren<SpriteRenderer>().sortingOrder;
		}

		private void OnDisable()
		{
			Destroy(_animator.gameObject);
		}
	}
}
