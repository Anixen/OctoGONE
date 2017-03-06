using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(NavGraphNode))]
public class NavGraphNodeEditor : Editor {

    // if this node is selected, display small info box above the node
    // also display a handle
    void OnSceneGUI()
    {
        Vector3 nodePos = Target.transform.position;
        float size = HandleUtility.GetHandleSize(nodePos) * 0.5f;

        //do not draw node header if too far away
        if (size < 3f)
        {
            //begin 2D GUI block
            Handles.BeginGUI();
            //translate waypoint vector3 position in world space into a position on the screen
            var guiPoint = HandleUtility.WorldToGUIPoint(nodePos);
            //create rectangle with that positions and do some offset
            var rect = new Rect(guiPoint.x - 50.0f, guiPoint.y - 40, 100, 20);
            //draw box at position with current waypoint name
            GUI.Box(rect, Target.name);
            Handles.EndGUI(); //end GUI block
        }

        //draw handles for the node, clamp size
        Handles.color = Color.red;
        size = Mathf.Clamp(size, 0, 1.2f);
        Vector3 newPos = Handles.FreeMoveHandle(nodePos, Quaternion.identity,
                         size, Vector3.zero, Handles.SphereCap);
        Handles.RadiusHandle(Quaternion.identity, nodePos, size / 2);

        if (nodePos != newPos)
        {
            Undo.RecordObject(Target.transform, "Move Handles");
            Target.transform.position = newPos;
        }

        NavGraphEdgeTrigger[] edgesTriggers = Target.gameObject.GetComponentsInChildren<NavGraphEdgeTrigger>();
        foreach (NavGraphEdgeTrigger edgeTrigger in edgesTriggers)
        {
            Vector3 triggerPos = Vector3.zero;
            Quaternion triggerRot = Quaternion.identity;

            Transform cubeTransform = Target.transform.FindChild("Cube");

            // draw start nodes
            triggerPos = edgeTrigger.transform.position;
            triggerRot = edgeTrigger.transform.rotation;
            size = HandleUtility.GetHandleSize(triggerPos) * 0.5f;

            //do not draw node header if too far away
            if (size < 3f)
            {
                //begin 2D GUI block
                Handles.BeginGUI();
                //translate waypoint vector3 position in world space into a position on the screen
                var guiPoint = HandleUtility.WorldToGUIPoint(triggerPos);
                //create rectangle with that positions and do some offset
                var rect = new Rect(guiPoint.x - 50.0f, guiPoint.y - 40, 120, 20);
                //draw box at position with current waypoint name
                GUI.Box(rect, edgeTrigger.name);
                Handles.EndGUI(); //end GUI block
            }

            //draw handles for the node, clamp size
            Handles.color = Color.red + Color.green * 0.4f;
            size = Mathf.Clamp(size, 0, 1.2f);
            newPos = Handles.FreeMoveHandle(triggerPos, triggerRot,
                size, Vector3.zero, Handles.SphereCap);
            Handles.RadiusHandle(Quaternion.identity, triggerPos, size / 2);

            if (triggerPos != newPos)
            {
                Undo.RecordObject(edgeTrigger.transform, "Move Handles");
                edgeTrigger.transform.position = newPos;
            }
        }
    }

    NavGraphNode Target { get { return target as NavGraphNode; } }
}
