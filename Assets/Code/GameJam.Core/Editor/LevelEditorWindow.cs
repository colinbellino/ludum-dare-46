#if UNITY_EDITOR
using UnityEditor;
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector;
using Sirenix.Utilities.Editor;
using Sirenix.Utilities;
using System;
using UnityEngine;
using Sirenix.OdinInspector.Editor.Drawers;
using System.Linq;

namespace GameJam.Core
{
	public class LevelEditorWindow : OdinMenuEditorWindow
	{
		[MenuItem("Tools/GameJam/LevelEditor")]
		private static void OpenWindow()
		{
			var window = GetWindow<LevelEditorWindow>();

			window.position = GUIHelper.GetEditorWindowRect().AlignCenter(700, 700);
		}

		protected override OdinMenuTree BuildMenuTree()
		{
			var tree = new OdinMenuTree(true);
			tree.DefaultMenuStyle.IconSize = 28.00f;
			tree.Config.DrawSearchToolbar = true;

			// Adds the character overview table.
			GameSettings.Instance.UpdateGameSettings();
			// tree.Add("Characters", new CharacterTable(GameSettings.Instance.AllCharacters));

			// Adds all characters.
			tree.AddAllAssetsAtPath("Levels", "Assets/Game/Data/Levels", typeof(Level), true, true);

			// Add drag handles to items, so they can be easily dragged into the inventory if characters etc...
			// tree.EnumerateTree().Where(x => x.Value as Item).ForEach(AddDragHandles);

			// Add icons to characters and items.
			// tree.EnumerateTree().AddIcons<Level>(x => x.Icon);
			// tree.EnumerateTree().AddIcons<Item>(x => x.Icon);

			return tree;
		}
	}
}
#endif
