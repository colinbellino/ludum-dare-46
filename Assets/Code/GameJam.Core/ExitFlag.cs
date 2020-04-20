using UnityEngine;

namespace GameJam.Core
{
	public class ExitFlag : MonoBehaviour
	{
		private Animator _fire;

		private void OnEnable()
		{
			_fire = Instantiate(GameSettings.Instance.FireAnimator, transform);
			_fire.transform.localPosition = new Vector3(0, 0.075f, 0);
		}

		private void OnDisable()
		{
			Destroy(_fire.gameObject);
		}
	}
}
