﻿using Sirenix.OdinInspector;
using UnityEngine;

namespace GameJam.Core
{
	public class Sounds : MonoBehaviour
	{
		[SerializeField] [Required] private AudioSource _audioSourceFireBackground;
		[SerializeField] [Required] private AudioSource _audioSourceFireIgnite;
		[SerializeField] [Required] private AudioSource _audioSourceButtons;
		[SerializeField] [Required] private AudioClip _looseClip;
		[SerializeField] [Required] private AudioClip _winClip;
		[SerializeField] [Required] private AudioClip _structureIgniteClip;
		private bool _isMuted = false;

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
			_audioSourceFireIgnite.PlayOneShot(_structureIgniteClip);
		}

		public void PlayLooseClip()
		{
			_audioSourceFireIgnite.PlayOneShot(_looseClip);
		}

		public void PlayWinClip()
		{
			_audioSourceFireIgnite.PlayOneShot(_winClip);
		}

		public void PlayButtonClip()
		{
			_audioSourceButtons.Play();
		}

		public void MuteUnMute()
		{
			_isMuted = !_isMuted;

			AudioListener.pause = _isMuted;
		}
	}
}
