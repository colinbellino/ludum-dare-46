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
		private Cell _data;

		public int StructureId => _structure.Id;

		public void Initialize(Vector2Int position, Cell data)
		{
			_data = data;
			Position = position;

			if (data.Terrain > -1)
			{
				var terrain = GameSettings.Instance.AllTerrains.Find(t => t.Id == data.Terrain);
				_terrain.Initialize(terrain);
			}

			if (data.Structure > -1)
			{
				var structure = GameSettings.Instance.AllStructures.Find(t => t.Id == data.Structure);
				_structure.Initialize(structure, position);
			}

			_fire.Initialize(data.Fire);
		}

		public bool HasStructure() => _structure.IsActive;

		public void PlaceStructure(Structure structure) => _structure.PlaceStructure(structure, Position);

		public void DestroyStructure() => _structure.DestroyStructure();

		public bool CanBurn() => HasStructure();

		// 🎩
		public bool CanConstruct()
		{
			return _structure.IsActive == false;
		}

		// 🤠
		public bool CanDestroy()
		{
			return HasComponent<IndestructibleFlag>() == false && _structure.IsActive == true;
		}

		public bool IsOnFire() => _fire.Amount > 0;

		public bool Burn()
		{
			if (HasComponent<UnburnableFlag>())
			{
				return false;
			}

			_fire.Kindle();

			// TODO: Get this from content data
			var limit = 2;
			if (_fire.Amount <= limit)
			{
				return false;
			}

			_fire.Extinguish();
			DestroyStructure();
			Burnt?.Invoke();

			return true;
		}

		public override string ToString()
		{
			return $"Cell [{Position.x},{Position.y}]";
		}

		public bool HasComponent<T>()
		{
			return
				gameObject.TryGetComponent<T>(out _) ||
				_structure.TryGetComponent<T>(out _) ||
				_terrain.TryGetComponent<T>(out _);
		}
	}
}
