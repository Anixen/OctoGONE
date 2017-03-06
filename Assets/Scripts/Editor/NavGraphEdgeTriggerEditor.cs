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
        HandleDisplay.DrawHandle(Target);

        // Draw associated edge
        HandleDisplay.DrawHandles(Target.EdgeMovement);
        HandleDisplay.DrawHandle(Target.EdgeMovement.StartNode);
        HandleDisplay.DrawHandle(Target.EdgeMovement.EndNode);
    }

    NavGraphEdgeTrigger Target { get { return target as NavGraphEdgeTrigger; } }
  
}
