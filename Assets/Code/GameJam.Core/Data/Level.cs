using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Sirenix.OdinInspector.Demos.RPGEditor;
using System;
using Sirenix.OdinInspector.Editor.Drawers;
using Sirenix.Utilities.Editor;
using Sirenix.Utilities;

namespace GameJam.Core
{
	[CreateAssetMenu(fileName = "Level", menuName = "GameJam/Level")]
	public class Level : SerializedScriptableObject
	{
		// public CellData[,] q = new CellData[10, 10];

		[TableMatrix(SquareCells = true, DrawElementMethod = "DrawCell")]
		public CellData[,] ddd = new CellData[10, 10] {
			{ new CellData(), new CellData(), new CellData(), new CellData(), new CellData(), new CellData(), new CellData(), new CellData(), new CellData(), new CellData() },
			{ new CellData(), new CellData(), new CellData(), new CellData(), new CellData(), new CellData(), new CellData(), new CellData(), new CellData(), new CellData() },
			{ new CellData(), new CellData(), new CellData(), new CellData(), new CellData(), new CellData(), new CellData(), new CellData(), new CellData(), new CellData() },
			{ new CellData(), new CellData(), new CellData(), new CellData(), new CellData(), new CellData(), new CellData(), new CellData(), new CellData(), new CellData() },
			{ new CellData(), new CellData(), new CellData(), new CellData(), new CellData(), new CellData(), new CellData(), new CellData(), new CellData(), new CellData() },
			{ new CellData(), new CellData(), new CellData(), new CellData(), new CellData(), new CellData(), new CellData(), new CellData(), new CellData(), new CellData() },
			{ new CellData(), new CellData(), new CellData(), new CellData(), new CellData(), new CellData(), new CellData(), new CellData(), new CellData(), new CellData() },
			{ new CellData(), new CellData(), new CellData(), new CellData(), new CellData(), new CellData(), new CellData(), new CellData(), new CellData(), new CellData() },
			{ new CellData(), new CellData(), new CellData(), new CellData(), new CellData(), new CellData(), new CellData(), new CellData(), new CellData(), new CellData() },
			{ new CellData(), new CellData(), new CellData(), new CellData(), new CellData(), new CellData(), new CellData(), new CellData(), new CellData(), new CellData() },
		};

		public Dictionary<Vector2Int, CellData> Board = new Dictionary<Vector2Int, CellData> {
			{ new Vector2Int(0, 0), new CellData { Content = 0, Fire = 1 } },
			{ new Vector2Int(0, 1), new CellData { Content = -1 } },
			{ new Vector2Int(0, 2), new CellData { Content = -1 } },
			{ new Vector2Int(0, 3), new CellData { Content = -1 } },
			{ new Vector2Int(0, 4), new CellData { Content = 0 } },
			{ new Vector2Int(1, 0), new CellData { Content = -1 } },
			{ new Vector2Int(1, 1), new CellData { Content = -1 } },
			{ new Vector2Int(1, 2), new CellData { Content = -1 } },
			{ new Vector2Int(1, 3), new CellData { Content = -1 } },
			{ new Vector2Int(1, 4), new CellData { Content = 0 } },
			{ new Vector2Int(2, 0), new CellData { Content = -1 } },
			{ new Vector2Int(2, 1), new CellData { Content = -1 } },
			{ new Vector2Int(2, 2), new CellData { Content = 0 } },
			{ new Vector2Int(2, 3), new CellData { Content = -1 } },
			{ new Vector2Int(2, 4), new CellData { Content = -1 } },
			{ new Vector2Int(3, 0), new CellData { Content = -1 } },
			{ new Vector2Int(3, 1), new CellData { Content = -1 } },
			{ new Vector2Int(3, 2), new CellData { Content = 0 } },
			{ new Vector2Int(3, 3), new CellData { Content = -1 } },
			{ new Vector2Int(3, 4), new CellData { Content = -1 } },
			{ new Vector2Int(4, 0), new CellData { Content = -1 } },
			{ new Vector2Int(4, 1), new CellData { Content = -1 } },
			{ new Vector2Int(4, 2), new CellData { Content = 0 } },
			{ new Vector2Int(4, 3), new CellData { Content = 0 } },
			{ new Vector2Int(4, 4), new CellData { Content = 1 } },
		};
		private static Terrain[] _terrains;
		private static StructureData[] _structures;

		[Button]
		private void RefreshResources()
		{
			_terrains = Resources.LoadAll<Terrain>("Data/Terrains");
			_structures = Resources.LoadAll<StructureData>("Data/Structures");
		}

		private static CellData DrawCell(Rect rect, CellData value)
		{
			if (Event.current.type == EventType.MouseDown && rect.Contains(Event.current.mousePosition))
			{
				if (Event.current.modifiers.HasFlag(EventModifiers.Alt))
				{
					value.Content = 0;
				}
				else
				{
					value.Terrain = 0;
				}
			}

			if (value.Terrain != null)
			{
				var sprite = GetTerrain((int)value.Terrain).Sprite;
				UnityEditor.EditorGUI.DrawPreviewTexture(rect.Padding(1), TextureUtilities.ConvertSpriteToTexture(sprite).CropTexture(new Rect(0, 0, 16, 16)));
			}

			if (value.Content != null)
			{
				var sprite = GetStructure((int)value.Content).Sprite;
				UnityEditor.EditorGUI.DrawPreviewTexture(rect.Padding(10), TextureUtilities.ConvertSpriteToTexture(sprite).CropTexture(new Rect(0, 0, 16, 16)));
			}

			return value;
		}

		private static Terrain GetTerrain(int id)
		{
			for (int i = 0; i < _terrains.Length; i++)
			{
				if (_terrains[i].Id == id)
				{
					return _terrains[i];
				}
			}

			return null;
		}

		private static StructureData GetStructure(int? id)
		{
			for (int i = 0; i < _structures.Length; i++)
			{
				if (_structures[i].Id == id)
				{
					return _structures[i];
				}
			}

			return null;
		}
	}

	internal sealed class CellDataCellDrawer<TArray> : TwoDimensionalArrayDrawer<TArray, CellData>
		where TArray : System.Collections.IList
	{
		protected override TableMatrixAttribute GetDefaultTableMatrixAttributeSettings()
		{
			return new TableMatrixAttribute()
			{
				SquareCells = true,
				HideColumnIndices = true,
				HideRowIndices = true,
				ResizableColumns = false
			};
		}

		protected override CellData DrawElement(Rect rect, CellData value)
		{
			// var id = DragAndDropUtilities.GetDragAndDropId(rect);
			// DragAndDropUtilities.DrawDropZone(rect, value.Terrain ? value.Terrain.Texture : null, null, id);

			// // if (value.Item != null)
			// // {
			// // 	// Item count
			// // 	var countRect = rect.Padding(2).AlignBottom(16);
			// // 	value.ItemCount = EditorGUI.IntField(countRect, Mathf.Max(1, value.ItemCount));
			// // 	GUI.Label(countRect, "/ " + value.Item.ItemStackSize, SirenixGUIStyles.RightAlignedGreyMiniLabel);
			// // }

			// value = DragAndDropUtilities.DropZone(rect, value);                                     // Drop zone for CellData structs.
			// value.Content = DragAndDropUtilities.DropZone<int>(rect, value.Content);                     // Drop zone for Item types.
			// value = DragAndDropUtilities.DragZone(rect, value, true, true);                         // Enables dragging of the CellData

			return value;
		}

		protected override void DrawPropertyLayout(GUIContent label)
		{
			base.DrawPropertyLayout(label);

			// Draws a drop-zone where we can destroy items.
			var rect = GUILayoutUtility.GetRect(0, 40).Padding(2);
			var id = DragAndDropUtilities.GetDragAndDropId(rect);
			DragAndDropUtilities.DrawDropZone(rect, null as UnityEngine.Object, null, id);
			DragAndDropUtilities.DropZone<CellData>(rect, new CellData(), false, id);
		}
	}
}
