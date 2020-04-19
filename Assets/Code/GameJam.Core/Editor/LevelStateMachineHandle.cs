#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace GameJam.Core
{
	[CustomEditor(typeof(LevelStateMachine))]
	[InitializeOnLoad]
	public class LevelStateMachineHandle : Editor
	{
		[DrawGizmo(GizmoType.InSelectionHierarchy | GizmoType.NotInSelectionHierarchy)]
		static void DrawHandles(LevelStateMachine stateMachine, GizmoType gizmoType)
		{
			GUIStyle style = new GUIStyle();
			style.normal.textColor = Color.white;
			Handles.Label(stateMachine.transform.position, $"Level state :{stateMachine.StateName}", style);
		}
	}
}
#endif
