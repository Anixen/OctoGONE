using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(NavGraphManager))]
public class NavGraphManagerEditor : Editor {

    public override void OnInspectorGUI() {
        GUILayout.BeginVertical();

        DrawDefaultInspector();

        if (GUILayout.Button("Update Splines")) {
            Debug.Log("Updating splines");
                
            Target.UpdateSplines();
        }
        GUILayout.EndVertical();
    }

    NavGraphManager Target { get { return target as NavGraphManager; } }
}
