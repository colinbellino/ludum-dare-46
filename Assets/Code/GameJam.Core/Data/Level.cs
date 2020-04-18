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
		public CellSlot[,] q = new CellSlot[10, 10];

		public Dictionary<Vector2Int, CellData> Board = new Dictionary<Vector2Int, CellData> {
			{ new Vector2Int(0, 0), new CellData { Content = 0, Type = 0, Fire = 1 } },
			{ new Vector2Int(0, 1), new CellData { Content = -1, Type = 0 } },
			{ new Vector2Int(0, 2), new CellData { Content = -1, Type = 0 } },
			{ new Vector2Int(0, 3), new CellData { Content = -1, Type = 0 } },
			{ new Vector2Int(0, 4), new CellData { Content = 0, Type = 0 } },
			{ new Vector2Int(1, 0), new CellData { Content = -1, Type = 0 } },
			{ new Vector2Int(1, 1), new CellData { Content = -1, Type = 1 } },
			{ new Vector2Int(1, 2), new CellData { Content = -1, Type = 1 } },
			{ new Vector2Int(1, 3), new CellData { Content = -1, Type = 1 } },
			{ new Vector2Int(1, 4), new CellData { Content = 0, Type = 0 } },
			{ new Vector2Int(2, 0), new CellData { Content = -1, Type = 0 } },
			{ new Vector2Int(2, 1), new CellData { Content = -1, Type = 1 } },
			{ new Vector2Int(2, 2), new CellData { Content = 0, Type = 1 } },
			{ new Vector2Int(2, 3), new CellData { Content = -1, Type = 1 } },
			{ new Vector2Int(2, 4), new CellData { Content = -1, Type = 0 } },
			{ new Vector2Int(3, 0), new CellData { Content = -1, Type = 0 } },
			{ new Vector2Int(3, 1), new CellData { Content = -1, Type = 1 } },
			{ new Vector2Int(3, 2), new CellData { Content = 0, Type = 1 } },
			{ new Vector2Int(3, 3), new CellData { Content = -1, Type = 1 } },
			{ new Vector2Int(3, 4), new CellData { Content = -1, Type = 0 } },
			{ new Vector2Int(4, 0), new CellData { Content = -1, Type = 0 } },
			{ new Vector2Int(4, 1), new CellData { Content = -1, Type = 0 } },
			{ new Vector2Int(4, 2), new CellData { Content = 0, Type = 0 } },
			{ new Vector2Int(4, 3), new CellData { Content = 0, Type = 0 } },
			{ new Vector2Int(4, 4), new CellData { Content = 1, Type = 0 } },
		};
	}

	[Serializable]
	public struct CellSlot
	{
		public int ItemCount;
		public Item Item;
	}

	internal sealed class CellSlotCellDrawer<TArray> : TwoDimensionalArrayDrawer<TArray, CellSlot>
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

		protected override CellSlot DrawElement(Rect rect, CellSlot value)
		{
			// var id = DragAndDropUtilities.GetDragAndDropId(rect);
			// DragAndDropUtilities.DrawDropZone(rect, value.Item ? value.Item.Icon : null, null, id); // Draws the drop-zone using the items icon.

			// if (value.Item != null)
			// {
			// 	// Item count
			// 	var countRect = rect.Padding(2).AlignBottom(16);
			// 	value.ItemCount = EditorGUI.IntField(countRect, Mathf.Max(1, value.ItemCount));
			// 	GUI.Label(countRect, "/ " + value.Item.ItemStackSize, SirenixGUIStyles.RightAlignedGreyMiniLabel);
			// }

			// value = DragAndDropUtilities.DropZone(rect, value);                                     // Drop zone for CellSlot structs.
			// value.Item = DragAndDropUtilities.DropZone<Item>(rect, value.Item);                     // Drop zone for Item types.
			// value = DragAndDropUtilities.DragZone(rect, value, true, true);                         // Enables dragging of the CellSlot

			return value;
		}

		protected override void DrawPropertyLayout(GUIContent label)
		{
			base.DrawPropertyLayout(label);

			// Draws a drop-zone where we can destroy items.
			var rect = GUILayoutUtility.GetRect(0, 40).Padding(2);
			var id = DragAndDropUtilities.GetDragAndDropId(rect);
			DragAndDropUtilities.DrawDropZone(rect, null as UnityEngine.Object, null, id);
			DragAndDropUtilities.DropZone<CellSlot>(rect, new CellSlot(), false, id);
		}
	}
}
