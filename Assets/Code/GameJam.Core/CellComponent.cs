using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameJam.Core
{
	public class CellComponent : MonoBehaviour
	{
		[SerializeField] [Required] private Animator _animator;
		[SerializeField] [Required] private TerrainComponent _terrain;
		[SerializeField] [Required] private StructureComponent _structure;
		[SerializeField] [Required] private FireComponent _fire;

		public Vector2Int Position { get; private set; }
		public event Action Burnt;
		private Cell _data;

		public int StructureId => _structure.Id;
		public int SortingOrder { get; private set; }

		public void Initialize(Vector2Int position, Cell data)
		{
			_data = data;
			Position = position;

			SortingOrder = Position.x * -10;

			if (data.Terrain > -1)
			{
				var terrain = FindObjectOfType<GameStateMachine>().GameSettings.AllTerrains.Find(t => t.Id == data.Terrain);
				_terrain.Initialize(terrain);
			}

			if (data.Structure > -1)
			{
				var structure = FindObjectOfType<GameStateMachine>().GameSettings.AllStructures.Find(t => t.Id == data.Structure);
				_structure.Initialize(structure, position);
				_animator.Play("Has Structure");
			}

			_fire.Initialize(data.Fire);
		}

		public bool HasStructure() => _structure.IsActive;

		public void PlaceStructure(Structure structure)
		{
			_animator.SetTrigger("PlaceStructure");
			_structure.PlaceStructure(structure, Position);
		}

		public void DestroyStructure()
		{
			_animator.SetTrigger("DestroyStructure");
			_structure.DestroyStructure();
		}

		public bool CanBurn() => HasStructure() && HasComponent<UnburnableFlag>() == false;

		// 🎩
		public bool CanConstruct()
		{
			return _structure.IsActive == false && _terrain.Id != 99;
		}

		// 🤠
		public bool CanDestroy()
		{
			return HasComponent<IndestructibleFlag>() == false && _structure.IsActive == true;
		}

		private bool CantBurnt()
		{
			return HasComponent<IndestructibleOnFireFlag>() == true;
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

			if (CantBurnt())
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
