using UnityEngine;
using UnityEngine.InputSystem;

namespace GameJam.Core
{
	public class Shortcuts : MonoBehaviour
	{
		private GameStateMachine _gameStateMachine;
		private Keyboard _keyboard;

		private void OnEnable()
		{
			_gameStateMachine = FindObjectOfType<GameStateMachine>();
			_keyboard = Keyboard.current;
		}

		private void Update()
		{
			if (_keyboard != null)
			{
				if (_keyboard.escapeKey.wasPressedThisFrame)
				{
					_gameStateMachine.ReturnToMenuOrQuit();
				}
			}
		}
	}
}
