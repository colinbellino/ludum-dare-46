#if UNITY_EDITOR
namespace GameJam.Core
{
	using Sirenix.OdinInspector;
	using Sirenix.Utilities;
	using System.Linq;

#if UNITY_EDITOR
	using UnityEditor;
#endif

	[GlobalConfig("Game/Data", UseAsset = true)]
	public class GameSettings : GlobalConfig<GameSettings>
	{
		[ReadOnly]
		[ListDrawerSettings(Expanded = true)]
		public Level[] AllLevels;

#if UNITY_EDITOR
		[Button(ButtonSizes.Medium), PropertyOrder(-1)]
		public void UpdateGameSettings()
		{
			AllLevels = AssetDatabase.FindAssets("t:Level")
				.Select(guid => AssetDatabase.LoadAssetAtPath<Level>(AssetDatabase.GUIDToAssetPath(guid)))
				.ToArray();
		}
#endif
	}
}
#endif
