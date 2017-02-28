using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(NavGraphManager))]
public class NavGraphManagerEditor : Editor {

    public override void OnInspectorGUI() {
        GUILayout.BeginVertical();

        DrawDefaultInspector();

        if (GUILayout.Button("Update Splines")) {
            Debug.Log("Updating splines");
                
            NavGraphEdgeMovement[] edges = Target.gameObject.GetComponentsInChildren<NavGraphEdgeMovement>();
            foreach (NavGraphEdgeMovement edge in edges)
                edge.updateSpline();
        }
        GUILayout.EndVertical();
    }

    NavGraphManager Target { get { return target as NavGraphManager; } }
}
