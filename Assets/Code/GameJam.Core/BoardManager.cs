using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameJam.Core
{
	public class BoardManager : MonoBehaviour
	{
		[SerializeField] [AssetsOnly] [Required] private GameObject[] _cellPrefabs;

		private Board _board;
		[SerializeField] [TextArea(10, 10)] private string _cellsFromLevel;

		private void Awake()
		{
			PrepareBoard();
			RenderBoard();
		}

		[Button]
		private void PrepareBoard()
		{
			var size = new Vector2Int(3, 3);
			_board = new Board(size);

			var rows = _cellsFromLevel.Split('\n');

			for (int x = 0; x < rows.Length; x++)
			{
				var cells = rows[x].ToCharArray();
				for (int y = 0; y < cells.Length; y++)
				{
					var type = (CellTypes)Enum.Parse(typeof(CellTypes), cells[y].ToString());
					_board.Cells[x, y] = new Cell { Type = type, Content = null };
				}
			}
		}

		[Button]
		private void RenderBoard()
		{
			for (int x = 0; x < _board.Cells.GetLength(0); x++)
			{
				for (int y = 0; y < _board.Cells.GetLength(1); y++)
				{
					var cell = _board.Cells[x, y];
					var position = new Vector2Int(x, y);
					SpawnCell(position, cell);
				}
			}
		}

		private void SpawnCell(Vector2Int position, Cell cell)
		{
			var worldPosition = new Vector3(position.y, position.x, 0f);
			var instance = GameObject.Instantiate(_cellPrefabs[(int)cell.Type], worldPosition, Quaternion.identity);
		}

		[Button]
		private void PlacePlant()
		{

		}
	}
}
