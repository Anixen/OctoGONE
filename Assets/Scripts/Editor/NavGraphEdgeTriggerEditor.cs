using UnityEngine;
using System.Collections;
using Mono.CSharp;
using UnityEditor;

[CustomEditor(typeof(NavGraphEdgeTrigger))]
public class NavGraphEdgeTriggerEditor : Editor
{

    void OnSceneGUI()
    {
        Vector3 triggerPos = Vector3.zero;
        Quaternion triggerRot = Quaternion.identity;
        Vector3 newPos = Vector3.zero;
        float size = 1f;

        Transform cubeTransform = Target.transform.FindChild("Cube");

        // draw start nodes
        triggerPos = Target.transform.position;
        triggerRot = Target.transform.rotation;
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
            GUI.Box(rect, Target.name);
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
            Undo.RecordObject(Target.transform, "Move Handles");
            Target.transform.position = newPos;
        }
    }

    NavGraphEdgeTrigger Target { get { return target as NavGraphEdgeTrigger; } }
  
}
