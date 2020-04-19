using UnityEngine;

namespace GameJam.Core
{
	public class ExitComponent : MonoBehaviour
	{
		private CellComponent _cell;

		private void Awake()
		{
			_cell = GetComponentInParent<CellComponent>();
		}

		private void OnEnable()
		{
			_cell.Burnt += OnBurnt;
		}

		private void OnDisable()
		{
			_cell.Burnt -= OnBurnt;
		}

		private void OnBurnt()
		{
			// TODO: Fire the level finished trigger
			// _gameStateMachine.FinishLevel();
			UnityEngine.Debug.Log("Exit => burnt");
		}
	}
}
