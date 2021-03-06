using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameJam.Core
{
	public class Simulation : SerializedMonoBehaviour
	{
		[SerializeField]
		[Required]
		private BoardManager _board;
		[SerializeField] [Required] private Sounds _sounds;

		private Vector2Int[] _directions = new Vector2Int[4]
			{
				new Vector2Int(0, 1),
				new Vector2Int(0, -1),
				new Vector2Int(1, 0),
				new Vector2Int(-1, 0)
			};
		private const float _tickInterval = 0.35f;

		private List<CellComponent> _processedCells;
		private List<CellComponent> _leftToBurnCell;
		private Coroutine _tickCoroutine;

		public void StartSimulation()
		{
			_tickCoroutine = StartCoroutine(Tick());
		}

		public void StopSimulation()
		{
			StopCoroutine(_tickCoroutine);
		}

		private IEnumerator Tick()
		{
			while (true)
			{
				_processedCells = new List<CellComponent>();
				_leftToBurnCell = new List<CellComponent>();

				var cellsToProcess = _board.Board.Values.ToList().Where(cell => cell.IsOnFire()).ToList();

				foreach (var cell in cellsToProcess)
				{
					TryToBurn(cell);

					// Find neighbours TO BURN MOAHHAHAHA
					foreach (var direction in _directions)
					{
						var destination = cell.Position + direction;
						_board.Board.TryGetValue(destination, out var neighbour);

						if (neighbour == null)
						{
							continue;
						}

						if (neighbour.CanBurn())
						{
							TryToBurn(neighbour);
						}

						// if (neighbour.HasComponent<ExitFlag>())
						// {
						// 	GameEvents.WinGame();
						// 	yield break;
						// }
					}
				}

				var numberOfBonfire = _board.Board.Values.ToList().Where(cell => cell.HasComponent<ExitFlag>()).ToList().Count;
				var numberOfBonfireLit =
					_leftToBurnCell.Where(cell => cell.HasComponent<ExitFlag>()).ToList().Count;

				if (_leftToBurnCell.Count == 0 || (numberOfBonfireLit == _leftToBurnCell.Count && numberOfBonfireLit != numberOfBonfire))
				{
					_sounds.PlayLooseClip();
					GameEvents.LoseGame();
					yield break;
				}



				if (numberOfBonfireLit == _leftToBurnCell.Count && numberOfBonfireLit == numberOfBonfire)
				{
					GameEvents.WinGame();
					yield break;
				}

				_sounds.PlayFireIgniteSound();

				yield return new WaitForSeconds(_tickInterval);
			}
		}

		private void TryToBurn(CellComponent cell)
		{
			if (_processedCells.Contains(cell))
			{
				return;
			}

			var isBurnt = cell.Burn();
			_processedCells.Add(cell);
			if (!isBurnt)
			{
				_leftToBurnCell.Add(cell);
			}
		}
	}
}
