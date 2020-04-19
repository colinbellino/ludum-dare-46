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

		private void Tick()
		{
			_processedCells = new List<CellComponent>();

			var cellsToProcess = _board.Board.Values.ToList().Where(IsBurning).ToList();

			foreach (var cell in cellsToProcess)
			{
				if (cell.Structure?.Fire < 1)
				{
					return;
				}

				TryToBurn(cell);

				// Find neighbours TO BURN MOAHHAHAHA
				foreach (var direction in _directions)
				{
					var destination = cell.Position + direction;
					_board.Board.TryGetValue(destination, out var neighbour);

					if (neighbour?.Structure)
					{
						TryToBurn(neighbour);
					}
				}
			}
		}

		private bool IsBurning(CellComponent cell, int index)
		{
			return cell.Structure?.Fire > 0;
		}

		private void TryToBurn(CellComponent cell)
		{
			if (_processedCells.Contains(cell))
			{
				return;
			}

			cell.Structure.AddFire(1);
			_processedCells.Add(cell);

			var limit = 2; // Get this from content data
			if (cell.Structure.Fire > limit)
			{
				cell.DestroyStructure();
			}
		}
	}
}
