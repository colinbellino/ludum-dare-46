using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameJam.Core
{
	public class CameraRig : MonoBehaviour
	{
		[SerializeField] private Camera _camera;

		private const float _speed = 5f;
		private Vector3 _titlePosition;
		private Vector3 _boardPosition = new Vector3(2, 1, -10);
		private Coroutine _transition;

		private void Awake()
		{
			_titlePosition = transform.position;
		}

		[Button]
		public void MoveToBoard()
		{
			if (_transition != null)
			{
				StopCoroutine(_transition);
			}

			_transition = StartCoroutine(LerpCameraTo(_boardPosition));
		}

		[Button]
		public void MoveToTitle()
		{
			if (_transition != null)
			{
				StopCoroutine(_transition);
			}

			_transition = StartCoroutine(LerpCameraTo(_titlePosition));
		}

		private IEnumerator LerpCameraTo(Vector3 destination)
		{
			while (Vector3.Distance(_camera.transform.localPosition, destination) > 0.01f)
			{
				_camera.transform.localPosition = Vector3.Lerp(_camera.transform.localPosition, destination, Time.deltaTime * _speed);
				yield return null;
			}

			_camera.transform.localPosition = destination;
		}
	}
}
