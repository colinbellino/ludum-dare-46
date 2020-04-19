using Sirenix.OdinInspector;
using UnityEngine;

namespace GameJam.Core
{
	public class Sounds : MonoBehaviour
	{
		[SerializeField] [Required] private AudioClip _backgroundMusic;
		[SerializeField] [Required] private AudioClip _simulationMusic;
		[SerializeField] [Required] private AudioSource _audioSource;

		private void Awake()
		{
			_audioSource.clip = _backgroundMusic;
		}

		public void PlaySimulationMusic()
		{
			_audioSource.clip = _simulationMusic;
			_audioSource.Play();
		}

		public void PlayBackgroundMusic()
		{
			_audioSource.clip = _backgroundMusic;
			_audioSource.Play();
		}
	}
}
