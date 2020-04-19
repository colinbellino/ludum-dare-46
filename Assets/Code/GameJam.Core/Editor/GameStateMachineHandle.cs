#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace GameJam.Core
{
	[CustomEditor(typeof(GameStateMachine))]
	[InitializeOnLoad]
	public class GameStateMachineHandle : Editor
	{
		[DrawGizmo(GizmoType.InSelectionHierarchy | GizmoType.NotInSelectionHierarchy)]
		static void DrawHandles(GameStateMachine stateMachine, GizmoType gizmoType)
		{
			GUIStyle style = new GUIStyle();
			style.normal.textColor = Color.white;
			Handles.Label(stateMachine.transform.position, $"Game state: {stateMachine.StateName}", style);
		}
	}
}
#endif
