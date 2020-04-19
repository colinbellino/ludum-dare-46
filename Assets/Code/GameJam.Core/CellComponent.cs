using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameJam.Core
{
	public class CellComponent : MonoBehaviour
	{
		[SerializeField] [Required] private TerrainComponent _terrain;
		[SerializeField] [Required] private StructureComponent _structure;
		[SerializeField] [Required] private FireComponent _fire;

		public Vector2Int Position { get; private set; }
		public event Action Burnt;

		public void Initialize(Vector2Int position, Cell data)
		{
			Position = position;

			if (data.Terrain != null)
			{
				var terrain = GameSettings.Instance.AllTerrains.Find(t => t.Id == data.Terrain);
				_terrain.Initialize(terrain);
			}

			if (data.Structure != null)
			{
				var structure = GameSettings.Instance.AllStructures.Find(t => t.Id == data.Structure);
				_structure.Initialize(structure);
			}

			_fire.Initialize(data.Fire);
		}

		public bool HasStructure() => _structure.IsActive;

		public void PlaceStructure(Structure structure) => _structure.PlaceStructure(structure);

		public void DestroyStructure() => _structure.DestroyStructure();

		public bool CanBurn() => HasStructure();

		public bool IsOnFire() => _fire.Amount > 0;

		public void Burn()
		{
			_fire.Kindle();

			// TODO: Get this from content data
			var limit = 2;
			if (_fire.Amount > limit)
			{
				_fire.Extinguish();
				DestroyStructure();
				Burnt?.Invoke();
			}
		}

		public override string ToString()
		{
			return $"Cell [{Position.x},{Position.y}]";
		}
	}
}
