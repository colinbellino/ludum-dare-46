using System;
using UnityEngine;

namespace GameJam.Core
{
	public class CellComponent : MonoBehaviour
	{
		public Vector2Int Position { get; private set; }
		public TerrainComponent Terrain { get; private set; }
		public StructureComponent Structure { get; private set; }

		public void Initialize(Vector2Int position, Cell data)
		{
			Position = position;

			if (data.Terrain != null)
			{
				Terrain = SpawnTerrain((int)data.Terrain);
			}

			if (data.Structure != null)
			{
				Structure = SpawnStructure((int)data.Structure);
				Structure.SetFire(data.Fire);
			}
		}

		public void PlaceStructure(int structureId)
		{
			Structure = SpawnStructure(structureId);
		}

		public void DestroyStructure()
		{
			if (Structure == null)
			{
				return;
			}

			Destroy(Structure.gameObject);
			Structure = null;
		}

		private TerrainComponent SpawnTerrain(int id)
		{
			var data = GameSettings.Instance.AllTerrains.Find(t => t.Id == id);

			var terrain = Instantiate(GameSettings.Instance.TerrainPrefab, transform);
			terrain.Initialize(data);

			return terrain;
		}

		private StructureComponent SpawnStructure(int id)
		{
			var data = GameSettings.Instance.AllStructures.Find(t => t.Id == id);

			var structure = Instantiate(GameSettings.Instance.StructurePrefab, transform);
			structure.Initialize(data);

			return structure;
		}

		public override string ToString()
		{
			return $"Cell [{Position.x},{Position.y}]";
		}
	}
}
