using UnityEngine;
using System.Collections;
using Mono.CSharp;
using SWS;
using UnityEditor;

[CustomEditor(typeof(NavGraphEdgeTrigger))]
public class NavGraphEdgeTriggerEditor : Editor
{

    void OnSceneGUI()
    {
        HandleDisplayer.DrawHandle(Target);

        // Draw associated edge
        HandleDisplayer.DrawHandles(Target.EdgeMovement);
        HandleDisplayer.DrawHandle(Target.EdgeMovement.StartNode);
        HandleDisplayer.DrawHandle(Target.EdgeMovement.EndNode);
    }

    NavGraphEdgeTrigger Target { get { return target as NavGraphEdgeTrigger; } }
  
}
