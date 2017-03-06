using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(NavGraphNode))]
public class NavGraphNodeEditor : Editor {

    // if this node is selected, display small info box above the node
    // also display a handle
    void OnSceneGUI()
    {
        HandleDisplay.DrawHandle(Target);

        // Draw asociated triggers
        NavGraphEdgeTrigger[] edgesTriggers = Target.gameObject.GetComponentsInChildren<NavGraphEdgeTrigger>();
        foreach (NavGraphEdgeTrigger edgeTrigger in edgesTriggers)
        {
            HandleDisplay.DrawHandle(edgeTrigger);
        }
    }

    NavGraphNode Target { get { return target as NavGraphNode; } }
}
