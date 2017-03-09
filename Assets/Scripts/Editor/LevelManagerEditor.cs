using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelManager))]
public class LevelManagerEditor : Editor
{
    public string LevelToLoad = "level_00";

    public override void OnInspectorGUI()
    {
        GUILayout.BeginVertical();

        DrawDefaultInspector();


        GUILayout.BeginHorizontal();

        LevelToLoad = GUILayout.TextField(LevelToLoad);

        if (GUILayout.Button("Load Level"))
        {
            Debug.Log("Loading Level : " + LevelToLoad);
            Target.Load(LevelToLoad);
        }
        GUILayout.EndHorizontal();

        GUILayout.EndVertical();
    }

    LevelManager Target { get { return target as LevelManager; } }
}
