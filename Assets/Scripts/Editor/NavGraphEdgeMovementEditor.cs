using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(NavGraphEdgeMovement))]
public class NavGraphEdgeMovementEditor : Editor {

    // if this edge is selected, display its pathcontainer gizmo and handle
    void OnSceneGUI()
    {
        // Display intermediate waypoints handles
        HandleDisplayer.DrawHandles(Target);

        // Display start and end nodes handles
        HandleDisplayer.DrawHandle(Target.StartNode);
        HandleDisplayer.DrawHandle(Target.EndNode);

        // Draw triggers associated with start and end nodes that will activate this edge
        NavGraphEdgeTrigger[] edgesTriggers_start = Target.StartNode.gameObject.GetComponentsInChildren<NavGraphEdgeTrigger>();
        foreach (NavGraphEdgeTrigger edgeTrigger in edgesTriggers_start)
        {
            if(edgeTrigger.EdgeMovement != Target)
                continue;

            HandleDisplayer.DrawHandle(edgeTrigger);
        }

        NavGraphEdgeTrigger[] edgesTriggers_end = Target.EndNode.gameObject.GetComponentsInChildren<NavGraphEdgeTrigger>();
        foreach (NavGraphEdgeTrigger edgeTrigger in edgesTriggers_end)
        {
            if (edgeTrigger.EdgeMovement != Target)
                continue;

            HandleDisplayer.DrawHandle(edgeTrigger);
        }
    }

    NavGraphEdgeMovement Target { get { return target as NavGraphEdgeMovement; } }
}
