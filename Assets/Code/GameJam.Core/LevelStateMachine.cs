using UnityEngine;
using Stateless;

namespace GameJam.Core
{
	public class LevelStateMachine : MonoBehaviour
	{
		// [SerializeField] [Required] private BoardManager _boardManager;
		// [SerializeField] [Required] private Simulation _simulationManager;
		// [SerializeField] [Required] private GameObject _mainMenuUi;
		// [SerializeField] [Required] private GameObject _actionBars;

		private StateMachine<States, Triggers> _machine;

		public string StateName => _machine?.State.ToString();

		private void Awake()
		{
			CreateGameStateMachine();
		}

		private void CreateGameStateMachine()
		{
			_machine = new StateMachine<States, Triggers>(States.Idle);

			_machine.Configure(States.Idle)
				.Permit(Triggers.StartLevel, States.Prepare);

			_machine.Configure(States.Prepare)
				.Permit(Triggers.StartSimulation, States.Simulate);

			_machine.Configure(States.Simulate)
				.Permit(Triggers.Win, States.Win)
				.Permit(Triggers.Lose, States.Lose);

			_machine.Configure(States.Win);

			_machine.Configure(States.Lose);
		}

		public enum States
		{
			Idle,
			Prepare,
			Simulate,
			Win,
			Lose,
		}

		public enum Triggers
		{
			StartLevel,
			StartSimulation,
			Win,
			Lose,
		}
	}

}
