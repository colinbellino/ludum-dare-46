using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System.Collections.Generic;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
#endif

namespace GameJam.Core
{
	[GlobalConfig("Assets/Resources", UseAsset = true)]
	public class GameSettings : GlobalConfig<GameSettings>
	{
		[ReadOnly]
		[ListDrawerSettings(Expanded = true)]
		public List<Level> AllLevels;

		[ReadOnly]
		[ListDrawerSettings(Expanded = true)]
		public List<Terrain> AllTerrains;

		[ReadOnly]
		[ListDrawerSettings(Expanded = true)]
		public List<Structure> AllStructures;

		[Required]
		[AssetsOnly]
		public CellComponent CellPrefab;

		[Required]
		[AssetsOnly]
		public Animator BonfireAnimatorPrefab;

#if UNITY_EDITOR
		[Button(ButtonSizes.Medium), PropertyOrder(-1)]
		public void UpdateGameSettings()
		{
			AllLevels = AssetDatabase.FindAssets($"t:{typeof(Level).FullName}")
				.Select(guid => AssetDatabase.LoadAssetAtPath<Level>(AssetDatabase.GUIDToAssetPath(guid)))
				.ToList();

			AllTerrains = AssetDatabase.FindAssets($"t:{typeof(Terrain).FullName}")
				.Select(guid => AssetDatabase.LoadAssetAtPath<Terrain>(AssetDatabase.GUIDToAssetPath(guid)))
				.OrderBy(asset => asset.Id)
				.ToList();

			AllStructures = AssetDatabase.FindAssets($"t:{typeof(Structure).FullName}")
				.Select(guid => AssetDatabase.LoadAssetAtPath<Structure>(AssetDatabase.GUIDToAssetPath(guid)))
				.OrderBy(asset => asset.Id)
				.ToList();
		}
#endif
	}
}
