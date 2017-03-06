using UnityEngine;
using UnityEditor;

public class HandleDisplay {

    public static void DrawHandle(NavGraphNode target)
    {
        Vector3 nodePos = target.transform.position;
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
            GUI.Box(rect, target.name);
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
            Undo.RecordObject(target.transform, "Move Handles");
            target.transform.position = newPos;
        }
    }

    public static void DrawHandle(NavGraphEdgeTrigger target)
    {
        Vector3 triggerPos = target.transform.position;
        Quaternion triggerRot = target.transform.rotation;
        Vector3 newPos = Vector3.zero;
        float size = HandleUtility.GetHandleSize(triggerPos) * 0.5f;

        Transform cubeTransform = target.transform.FindChild("Cube");

        //do not draw trigger header if too far away
        if (size < 3f)
        {
            //begin 2D GUI block
            Handles.BeginGUI();
            //translate waypoint vector3 position in world space into a position on the screen
            var guiPoint = HandleUtility.WorldToGUIPoint(triggerPos);
            //create rectangle with that positions and do some offset
            var rect = new Rect(guiPoint.x - 50.0f, guiPoint.y - 40, 120, 20);
            //draw box at position with current waypoint name
            GUI.Box(rect, target.transform.name);
            Handles.EndGUI(); //end GUI block
        }

        //draw handles for the node, clamp size
        if (target.ReverseMovement)
            Handles.color = Color.cyan;
        else
            Handles.color = Color.red + Color.green * 0.4f;

        size = Mathf.Clamp(size, 0, 1.2f);
        newPos = Handles.FreeMoveHandle(triggerPos, triggerRot,
            size, Vector3.zero, Handles.SphereCap);
        Handles.RadiusHandle(Quaternion.identity, triggerPos, size / 2);

        if (triggerPos != newPos)
        {
            Undo.RecordObject(target.transform, "Move Handles");
            target.transform.position = newPos;
        }
    }

    public static void DrawHandles(NavGraphEdgeMovement target)
    {
        // get waypoint array of the contained path manager
        Transform[] waypoints = target.PathContainer.waypoints;
        //do not execute further code if we have no waypoints defined
        //(just to make sure, practically this can not occur)
        if (waypoints.Length == 0) return;

        Vector3 wpPos = Vector3.zero;
        Vector3 newPos = Vector3.zero;
        float size = HandleUtility.GetHandleSize(wpPos) * 0.2f; ;

        // draw intermediate waypoints
        // loop through waypoint array 
        for (int i = 1; i < waypoints.Length - 1; i++)
        {
            if (!waypoints[i]) continue;
            wpPos = waypoints[i].position;

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

            Handles.color = target.PathContainer.color2;
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
    }

}
