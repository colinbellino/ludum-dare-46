using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameJam.Core
{
	public class LevelEditor : MonoBehaviour
	{
		[SerializeField] private Level _level;

		public Dictionary<Vector2Int, Cell> Board { get; } = new Dictionary<Vector2Int, Cell>();

		[Button]
		private void Load()
		{
			foreach (var cell in _level.Board)
			{
				var position = cell.Key;
				Board.Add(position, GenerateCell(position, cell.Value));
			}
		}

		[Button]
		private void Save()
		{

		}

		private Cell GenerateCell(Vector2Int position, CellData authoringData)
		{
			var prefab = Resources.Load<GameObject>("Prefabs/Cell");
			var worldPosition = new Vector3(position.y, position.x, 0f);

			var instance = Instantiate(prefab, worldPosition, Quaternion.identity);
			instance.name = $"Cell [{position.x},{position.y}]";

			var cell = instance.GetComponent<Cell>();
			cell.Initialize(position, authoringData);

			return cell;
		}
	}
}
