using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(NavGraphEdgeMovement))]
public class NavGraphEdgeMovementEditor : Editor {

    // if this edge is selected, display its pathcontainer gizmo and handle
    void OnSceneGUI()
    {
         // get waypoint array of the contained path manager
        Transform[] waypoints = Target.PathContainer.waypoints;
        //do not execute further code if we have no waypoints defined
        //(just to make sure, practically this can not occur)
        if (waypoints.Length == 0) return;
        Vector3 nodePos = Vector3.zero;
        Vector3 wpPos = Vector3.zero;
        Vector3 newPos = Vector3.zero;
        float size = 1f;

        // draw start nodes
        nodePos = Target.StartNode.transform.position;
        size = HandleUtility.GetHandleSize(nodePos) * 0.5f;

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
            GUI.Box(rect, Target.StartNode.name);
            Handles.EndGUI(); //end GUI block
        }

        //draw handles for the node, clamp size
        Handles.color = Color.red;
        size = Mathf.Clamp(size, 0, 1.2f);
        newPos = Handles.FreeMoveHandle(nodePos, Quaternion.identity,
                         size, Vector3.zero, Handles.SphereCap);
        Handles.RadiusHandle(Quaternion.identity, nodePos, size / 2);

        if (nodePos != newPos)
        {
            Undo.RecordObject(Target.StartNode.transform, "Move Handles");
            Target.StartNode.transform.position = newPos;
        }


        // draw end nodes
        nodePos = Target.EndNode.transform.position;
        size = HandleUtility.GetHandleSize(nodePos) * 0.5f;

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
            GUI.Box(rect, Target.EndNode.name);
            Handles.EndGUI(); //end GUI block
        }

        //draw handles for the node, clamp size
        Handles.color = Color.red;
        size = Mathf.Clamp(size, 0, 1.2f);
        newPos = Handles.FreeMoveHandle(nodePos, Quaternion.identity,
                         size, Vector3.zero, Handles.SphereCap);
        Handles.RadiusHandle(Quaternion.identity, nodePos, size / 2);

        if (nodePos != newPos)
        {
            Undo.RecordObject(Target.EndNode.transform, "Move Handles");
            Target.EndNode.transform.position = newPos;
        }


        // draw intermediate waypoints
        // loop through waypoint array 
        for (int i = 1; i < waypoints.Length - 1; i++)
        {
            if (!waypoints[i]) continue;
            wpPos = waypoints[i].position;
            size = HandleUtility.GetHandleSize(wpPos) * 0.2f;

            // do not draw waypoint header if too far away
            if (size < 3f)
            {
                // begin 2D GUI block
                Handles.BeginGUI();
                // translate waypoint vector3 position in world space into a position on the screen
                var guiPoint = HandleUtility.WorldToGUIPoint(wpPos);
                // create rectangle with that positions and do some offset
                var rect = new Rect(guiPoint.x - 50.0f, guiPoint.y - 40, 100, 20);
                // draw box at position with current waypoint name
                GUI.Box(rect, waypoints[i].name);
                Handles.EndGUI(); // end GUI block
            }

            // draw handles per waypoint, clamp size
            // ignore start and end points
            if (i == 0 || i == waypoints.Length - 1)
                continue;

            Handles.color = Target.PathContainer.color2;
            size = Mathf.Clamp(size, 0, 1.2f);
            newPos = Handles.FreeMoveHandle(wpPos, Quaternion.identity,
                             size, Vector3.zero, Handles.SphereCap);
            Handles.RadiusHandle(Quaternion.identity, wpPos, size / 2);

            if (wpPos != newPos)
            {
                Undo.RecordObject(waypoints[i], "Move Handles");
                waypoints[i].position = newPos;
            }
        }

        // Draw Triggers associated with this edge for start and end nodes
        NavGraphEdgeTrigger[] edgesTriggers_start = Target.StartNode.gameObject.GetComponentsInChildren<NavGraphEdgeTrigger>();
        foreach (NavGraphEdgeTrigger edgeTrigger in edgesTriggers_start)
        {
            if(edgeTrigger.EdgeMovement != Target)
                continue;

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
            if (edgeTrigger.ReverseMovement)
                Handles.color = Color.cyan;
            else
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

        NavGraphEdgeTrigger[] edgesTriggers_end = Target.EndNode.gameObject.GetComponentsInChildren<NavGraphEdgeTrigger>();
        foreach (NavGraphEdgeTrigger edgeTrigger in edgesTriggers_end)
        {
            if (edgeTrigger.EdgeMovement != Target)
                continue;

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
            if (edgeTrigger.ReverseMovement)
                Handles.color = Color.cyan;
            else
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

    NavGraphEdgeMovement Target { get { return target as NavGraphEdgeMovement; } }
}
