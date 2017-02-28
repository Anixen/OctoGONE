using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelManager))]
public class LevelManagerEditor : Editor
{

    public override void OnInspectorGUI()
    {
        GUILayout.BeginVertical();


        DrawDefaultInspector();

        
        if (GUILayout.Button("Load Level 00"))
        {
            Debug.Log("Loading Level 00");
            Target.Load("level_00");
        }

        if (GUILayout.Button("Load Level 01"))
        {
            Debug.Log("Loading Level 01");
            Target.Load("level_01");
        }

        if (GUILayout.Button("Load Level 02"))
        {
            Debug.Log("Loading Level 02");
            Target.Load("level_02");
        }
        //*/
        GUILayout.EndVertical();
    }

    LevelManager Target { get { return target as LevelManager; } }
}
