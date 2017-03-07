using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(NavGraphManager))]
public class NavGraphManagerEditor : Editor {

    public override void OnInspectorGUI() {
        GUILayout.BeginVertical();

        DrawDefaultInspector();

        if (GUILayout.Button("Update Splines")) {
            Debug.Log("NavGraphManagerditor : Updating splines");
                
            NavGraphBuilder.UpdateSplines(Target);
        }
        GUILayout.EndVertical();
    }

    NavGraphManager Target { get { return target as NavGraphManager; } }
}
