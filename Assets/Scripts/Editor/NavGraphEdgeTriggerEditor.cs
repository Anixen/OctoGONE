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
        Target.DrawHandle();

        // Draw associated edge
        Target.EdgeMovement.DrawHandles();
        Target.EdgeMovement.StartNode.DrawHandle();
        Target.EdgeMovement.EndNode.DrawHandle();
    }

    NavGraphEdgeTrigger Target { get { return target as NavGraphEdgeTrigger; } }
  
}
