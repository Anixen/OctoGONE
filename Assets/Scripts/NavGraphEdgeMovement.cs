using UnityEngine;
using SWS;
using UnityEditor;
using UnityEngine.Events;

public class NavGraphEdgeMovement : MonoBehaviour {

    // Reference to path
    public PathManager PathContainer;

    public NavGraphNode StartNode;
    public NavGraphNode EndNode;
    public DG.Tweening.PathType movementType;


    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        WaypointManager.DrawCurved(PathContainer.GetPathPoints());
    }

    public void DrawHandles()
    {
        // get waypoint array of the contained path manager
        Transform[] waypoints = PathContainer.waypoints;
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

            Handles.color = PathContainer.color2;
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

    // Trigger movement function here
    public void StartMovement(splineMove mover, bool reverseMovement)
    {
        mover.Stop();

        mover.pathContainer = this.PathContainer;
        mover.reverse = reverseMovement;
        mover.pathType = movementType;

        // TODO Attach start and arrive events
        mover.events.Clear();
        for(int i = 0; i < PathContainer.GetEventsCount(); ++i)
            mover.events.Add(new UnityEvent());

        if (reverseMovement)
        {
            mover.events[0].AddListener(PingEnd_Reverse);
            mover.events[PathContainer.GetEventsCount() - 1].AddListener(PingStart_Reverse);
        }
        else
        {
            mover.events[0].AddListener(PingStart);
            mover.events[PathContainer.GetEventsCount() - 1].AddListener(PingEnd);
        }

        mover.StartMove();
    }

    void PingStart()
    {
        Debug.Log("Started Movement");

        StartNode.LeavingNodeEvent.Invoke();
    }

    void PingEnd()
    {
        EndNode.EnteringNodeEvent.Invoke();

        Debug.Log("Finished Movement");
    }

    void PingStart_Reverse()
    {
        Debug.Log("Started Movement (Reverse)");

        EndNode.LeavingNodeEvent.Invoke();
    }

    void PingEnd_Reverse()
    {
        StartNode.EnteringNodeEvent.Invoke();

        Debug.Log("Finished Movement (Reverse)");
    }

    public void updateSpline()
    {
        Debug.Log("Update spline of " + name);
        PathContainer.gameObject.transform.GetChild(0).transform.position = StartNode.transform.position;
        PathContainer.gameObject.transform.GetChild(PathContainer.waypoints.Length - 1).transform.position = EndNode.transform.position;
    } 

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
