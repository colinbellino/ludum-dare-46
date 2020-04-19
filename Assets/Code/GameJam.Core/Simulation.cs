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

		private Vector2Int[] _directions = new Vector2Int[4]
			{
				new Vector2Int(0, 1),
				new Vector2Int(0, -1),
				new Vector2Int(1, 0),
				new Vector2Int(-1, 0)
			};

		private List<CellComponent> _processedCells;

		public void StartSimulation()
		{
			InvokeRepeating("Tick", 0, 0.5f);
		}

		public void StopSimulation()
		{
			CancelInvoke("Tick");
		}

		private void Tick()
		{
			_processedCells = new List<CellComponent>();

			var cellsToProcess = _board.Board.Values.ToList().Where(cell => cell.IsOnFire()).ToList();

			foreach (var cell in cellsToProcess)
			{
				TryToBurn(cell);

				// Find neighbours TO BURN MOAHHAHAHA
				foreach (var direction in _directions)
				{
					var destination = cell.Position + direction;
					_board.Board.TryGetValue(destination, out var neighbour);

					if (neighbour?.CanBurn() == true)
					{
						TryToBurn(neighbour);
					}
				}
			}

			if (_processedCells.Count == 0)
			{
				// TODO:
				// _gameStateMachine.Lose();
				UnityEngine.Debug.Log("Simulation => Lose");
			}
		}

		private void TryToBurn(CellComponent cell)
		{
			if (_processedCells.Contains(cell))
			{
				return;
			}

			cell.Burn();
			_processedCells.Add(cell);
		}
	}
}
