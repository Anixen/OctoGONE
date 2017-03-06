using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(NavGraphEdgeMovement))]
public class NavGraphEdgeMovementEditor : Editor {

    // if this edge is selected, display its pathcontainer gizmo and handle
    void OnSceneGUI()
    {
        // Display intermediate waypoints handles
        HandleDisplay.DrawHandles(Target);

        // Display start and end nodes handles
        HandleDisplay.DrawHandle(Target.StartNode);
        HandleDisplay.DrawHandle(Target.EndNode);

        // Draw triggers associated with start and end nodes that will activate this edge
        NavGraphEdgeTrigger[] edgesTriggers_start = Target.StartNode.gameObject.GetComponentsInChildren<NavGraphEdgeTrigger>();
        foreach (NavGraphEdgeTrigger edgeTrigger in edgesTriggers_start)
        {
            if(edgeTrigger.EdgeMovement != Target)
                continue;

            HandleDisplay.DrawHandle(edgeTrigger);
        }

        NavGraphEdgeTrigger[] edgesTriggers_end = Target.EndNode.gameObject.GetComponentsInChildren<NavGraphEdgeTrigger>();
        foreach (NavGraphEdgeTrigger edgeTrigger in edgesTriggers_end)
        {
            if (edgeTrigger.EdgeMovement != Target)
                continue;

            HandleDisplay.DrawHandle(edgeTrigger);
        }
    }

    NavGraphEdgeMovement Target { get { return target as NavGraphEdgeMovement; } }
}
