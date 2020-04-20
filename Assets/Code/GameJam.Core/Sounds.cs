using Sirenix.OdinInspector;
using UnityEngine;

namespace GameJam.Core
{
	public class Sounds : MonoBehaviour
	{
		[SerializeField] [Required] private AudioSource _audioSourceFireBackground;
		[SerializeField] [Required] private AudioSource _audioSourceFireIgnite;

		public void PlaySimulationMusic()
		{
			_audioSourceFireBackground.Play();
		}

		public void PlayBackgroundMusic()
		{
			_audioSourceFireBackground.Stop();
		}

		public void PlayFireIgniteSound()
		{
			_audioSourceFireIgnite.Play();
		}
	}
}
