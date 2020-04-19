using Sirenix.OdinInspector;
using UnityEngine;
using Stateless;

namespace GameJam.Core
{
	public class GameStateMachine : MonoBehaviour
	{
		[SerializeField] [Required] private BoardManager _boardManager;
		[SerializeField] [Required] private Simulation _simulationManager;
		[SerializeField] [Required] private GameObject _mainMenuUi;
		[SerializeField] [Required] private GameObject _actionBars;

		private StateMachine<States, Triggers> _gameState;

		public string StateName => _gameState?.State.ToString();

		private void CreateGameStateMachine()
		{
			_gameState = new StateMachine<States, Triggers>(States.Idle);

			_gameState.Configure(States.Idle)
				.Permit(Triggers.ShowMenu, States.MainMenu);

			_gameState.Configure(States.MainMenu)
				.Permit(Triggers.ShowCredits, States.Credits)
				.Permit(Triggers.StartPlay, States.Game)
				.Permit(Triggers.QuitGame, States.Quit)
				.OnEntry(MainMenuEnter)
				.OnExit(MainMenuExit);

			_gameState.Configure(States.Game)
				.Permit(Triggers.ShowCredits, States.Credits)
				.Permit(Triggers.ShowMenu, States.MainMenu)
				.OnEntry(OnGameStart)
				.OnExit(OnGameStop);

			_gameState.Configure(States.Credits)
				.Permit(Triggers.ShowMenu, States.MainMenu);

			_gameState.Configure(States.Quit)
				.OnEntry(QuitEnter);

		}

		[Button]
		public void PlayTheGame()
		{
			_gameState.Fire(Triggers.StartPlay);
		}

		public void QuitGame()
		{
			if (_gameState.IsInState(States.MainMenu))
			{
				_gameState.Fire(Triggers.QuitGame);
			}
		}

		public void BackToMenu()
		{
			_gameState.Fire(Triggers.ShowMenu);
		}

		private void QuitEnter()
		{
#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
#else
			Application.Quit();
#endif
		}

		private void Start()
		{
			CreateGameStateMachine();

			_gameState.Fire(Triggers.ShowMenu);
		}

		private void MainMenuEnter()
		{
			_mainMenuUi.SetActive(true);
		}

		private void MainMenuExit()
		{
			_mainMenuUi.SetActive(false);
		}

		private void OnGameStart()
		{
			_boardManager.enabled = true;
			_actionBars.SetActive(true);
		}

		private void OnGameStop()
		{
			_boardManager.enabled = false;
			_simulationManager.StopSimulation();
			_actionBars.SetActive(false);
		}

		public enum States
		{
			Idle,
			MainMenu,
			Game,
			Credits,
			Quit,
		}

		public enum Triggers
		{
			ShowMenu,
			StartPlay,
			ShowCredits,
			QuitGame,
		}
	}
}
