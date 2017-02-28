using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(NavGraphNode))]
public class NavGraphNodeEditor : Editor {

    //if this path is selected, display small info boxes above all waypoint positions
    //also display handles for the waypoints
    void OnSceneGUI()
    {
        Vector3 nodePos = Target.transform.position;
        float size = HandleUtility.GetHandleSize(nodePos) * 0.4f;

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
    }

    NavGraphNode Target { get { return target as NavGraphNode; } }
}
