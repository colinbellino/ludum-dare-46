using System;
using UnityEngine;

namespace GameJam.Core
{
	public static class GameEvents
	{
		public static event Action GameLost;
		public static event Action GameWon;

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		static void Init()
		{
			GameLost = null;
			GameWon = null;
		}

		public static void LoseGame() => GameLost?.Invoke();
		public static void WinGame() => GameWon?.Invoke();
	}
}
