#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace GameJam.Core
{
	[CustomEditor(typeof(Cell))]
	[InitializeOnLoad]
	public class CellHandle : Editor
	{
		[DrawGizmo(GizmoType.InSelectionHierarchy | GizmoType.NotInSelectionHierarchy)]
		static void DrawHandles(Cell cell, GizmoType gizmoType)
		{
			GUIStyle style = new GUIStyle();
			style.normal.textColor = Color.white;
			Handles.Label(cell.transform.position, $"[{cell.Position.x},{cell.Position.y}]", style);
		}
	}
}
#endif
