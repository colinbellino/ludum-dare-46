using Sirenix.OdinInspector;
using UnityEngine;
using Stateless;
using Stateless.Graph;

namespace GameJam.Core
{
	public class GameStateMachine : MonoBehaviour
	{
		[SerializeField] [Required] private BoardManager _boardManager;
		[SerializeField] [Required] private Simulation _simulationManager;
		[SerializeField] [Required] private Sounds _sounds;
		[SerializeField] [Required] private GameObject _mainMenuUi;
		[SerializeField] [Required] private GameObject _prepareHud;
		[SerializeField] [Required] private GameObject _backToMenuButton;
		[SerializeField] [Required] private GameObject _gameOverMenu;
		[SerializeField] [Required] private GameObject _creditsPage;
		[SerializeField] [Required] private CameraRig _cameraRig;

		private StateMachine<States, Triggers> _machine;

		[ShowInInspector] [ReadOnly] public string StateName => _machine?.State.ToString();

		private void Start()
		{
			CreateGameStateMachine();

			_machine.Fire(Triggers.ShowTitle);
		}

		private void CreateGameStateMachine()
		{
			_machine = new StateMachine<States, Triggers>(States.Idle);

			_machine.Configure(States.Idle)
				.Permit(Triggers.ShowTitle, States.MainMenu);

			_machine.Configure(States.MainMenu)
				.Permit(Triggers.ShowCredits, States.Credits)
				.Permit(Triggers.StartGame, States.GamePrepare)
				.Permit(Triggers.QuitGame, States.Quit)
				.OnEntry(MainMenuEnter)
				.OnExit(MainMenuExit);

			_machine.Configure(States.Game)
				.Permit(Triggers.ShowCredits, States.Credits)
				.Permit(Triggers.ShowTitle, States.MainMenu)
				.OnEntry(() =>
				{
					GameEvents.GameWon += Win;
					GameEvents.GameLost += Lose;

					_cameraRig.MoveToBoard();

					_backToMenuButton.SetActive(true);
				})
				.OnExit(() =>
				{
					GameEvents.GameWon -= Win;
					GameEvents.GameLost -= Lose;

					_cameraRig.MoveToTitle();

					_boardManager.Deactivate();
					_backToMenuButton.SetActive(false);
				});

			{
				_machine.Configure(States.GamePrepare)
					.SubstateOf(States.Game)
					.Permit(Triggers.StartSimulation, States.GameSimulate)
					.OnEntry(() =>
					{
						_prepareHud.SetActive(true);
						_boardManager.Activate();
					})
					.OnExit(() =>
					{
						_prepareHud.SetActive(false);
					});

				_machine.Configure(States.GameSimulate)
					.SubstateOf(States.Game)
					.Permit(Triggers.Win, States.GameWin)
					.Permit(Triggers.Lose, States.GameLose)
					.OnEntry(() =>
					{
						_sounds.PlaySimulationMusic();
						_simulationManager.StartSimulation();
						_boardManager.DisableInteractions();
					})
					.OnExit(() =>
					{
						_sounds.PlayBackgroundMusic();
						_simulationManager.StopSimulation();
					});

				_machine.Configure(States.GameWin)
					.SubstateOf(States.Game)
					.Permit(Triggers.StartGame, States.GamePrepare)
					.Permit(Triggers.ShowCredits, States.Credits)
					.OnEntry(() =>
					{
						_sounds.PlayWinClip();
						var isLastLevel = _boardManager.NextLevelIndex();
						if (isLastLevel)
						{
							_machine.Fire(Triggers.ShowCredits);
						}
						else
						{
							_gameOverMenu.SetActive(true);
						}
					})
					.OnExit(() =>
					{
						_gameOverMenu.SetActive(false);
					});

				_machine.Configure(States.GameLose)
					.SubstateOf(States.Game)
					.Permit(Triggers.StartGame, States.GamePrepare)
					.OnEntry(() =>
					{
						_machine.Fire(Triggers.StartGame);
					});
			}

			_machine.Configure(States.Credits)
				.Permit(Triggers.ShowTitle, States.MainMenu)
				.OnEntry(() =>
				{
					_creditsPage.SetActive(true);
				})
				.OnExit(() =>
				{
					_creditsPage.SetActive(false);
				});

			_machine.Configure(States.Quit)
				.OnEntry(QuitEnter);

			// _machine.OnTransitioned((transition) => { Debug.Log(StateName); });
		}

		// Use something like this to vizualize: http://www.webgraphviz.com/
		[Button]
		[DisableInEditorMode]
		public void CopyGraph()
		{
			Debug.Log("Graph data copied to clipboard!");
			GUIUtility.systemCopyBuffer = UmlDotGraph.Format(_machine.GetInfo());
		}

		public void PlayTheGame() => _machine.Fire(Triggers.StartGame);

		public void StartSimulation() => _machine.Fire(Triggers.StartSimulation);

		public void ShowCredits() => _machine.Fire(Triggers.ShowCredits);

		public void QuitGame()
		{
			if (_machine.IsInState(States.MainMenu))
			{
				_machine.Fire(Triggers.QuitGame);
			}
		}

		public void BackToMenu() => _machine.Fire(Triggers.ShowTitle);

		public void Win() => _machine.Fire(Triggers.Win);

		public void Lose() => _machine.Fire(Triggers.Lose);

		private void QuitEnter()
		{
#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
#else
			Application.Quit();
#endif
		}

		private void MainMenuEnter()
		{
			_boardManager.ResetLevelIndex();
			_mainMenuUi.SetActive(true);
		}

		private void MainMenuExit()
		{
			_mainMenuUi.SetActive(false);
		}

		public enum States
		{
			Idle,
			MainMenu,
			Game,
			GamePrepare,
			GameSimulate,
			GameWin,
			GameLose,
			Credits,
			Quit,
		}

		public enum Triggers
		{
			ShowTitle,
			StartGame,
			StartSimulation,
			Win,
			Lose,
			ShowCredits,
			QuitGame,
		}
	}
}
