using UnityEngine;
using SWS;
using UnityEditor;

public class NavGraphBuilder : MonoBehaviour {

    public static void UpdateSpline(NavGraphEdgeMovement target)
    {
        string edgeNumber = target.name.Split('_')[1];

        Debug.Log("Update spline of " + target.name);

        if (target.PathContainer == null)
        {
            WaypointManager waypointManager = target.transform.parent.GetComponentInChildren<WaypointManager>();
            PathManager[] pathManagers = waypointManager.transform.GetComponentsInChildren<PathManager>();

            foreach (PathManager pathManager in pathManagers)
            {
                if (pathManager.name == "Path_" + edgeNumber)
                {
                    target.PathContainer = pathManager;
                    break;
                }
            }

            if (target.PathContainer == null)
            {
                // Instantiate a new Path container as a child of waypointManager

                //create a new container transform which will hold all new waypoints
                GameObject pathContainer = new GameObject();
                //reset position and parent container gameobject to this manager gameobject
                pathContainer.transform.position = waypointManager.gameObject.transform.position;
                pathContainer.transform.parent = waypointManager.gameObject.transform;
                pathContainer.name = "Path_" + edgeNumber;

                //create new waypoint gameobject
                GameObject waypoint_start = new GameObject("Waypoint 0");
                GameObject waypoint_end = new GameObject("Waypoint 1");
                waypoint_start.transform.parent = pathContainer.transform;
                waypoint_end.transform.parent = pathContainer.transform;

                //Undo.RegisterCreatedObjectUndo(waypoint_start, "Created waypoint_start");
                //Undo.RegisterCreatedObjectUndo(waypoint_end, "Created waypoint_end");

                target.PathContainer = pathContainer.AddComponent<PathManager>();
                target.PathContainer.waypoints = new Transform[2];
                target.PathContainer.waypoints[0] = waypoint_start.transform;
                target.PathContainer.waypoints[1] = waypoint_end.transform;

                WaypointManager.AddPath(pathContainer);
            }
            target.PathContainer.name = "Path_" + edgeNumber;
        }

        target.PathContainer.gameObject.transform
        .FindChild(target.PathContainer.waypoints[0].name).transform.position = 
            target.StartNode.transform.position;

        target.PathContainer.gameObject.transform
        .FindChild(target.PathContainer.waypoints[target.PathContainer.waypoints.Length - 1].name).transform.position =
            target.EndNode.transform.position;

        EditorUtility.SetDirty(target);
    }

    public static void UpdateTriggers(NavGraphEdgeMovement target)
    {
        string edgeNumber = target.name.Split('_')[1];

        if (target.startTrigger == null)
        {
            NavGraphEdgeTrigger[] edgesTriggers = target.StartNode.gameObject.GetComponentsInChildren<NavGraphEdgeTrigger>();
            foreach (NavGraphEdgeTrigger edgeTrigger in edgesTriggers)
            {
                if (edgeTrigger.EdgeMovement == target)
                {
                    target.startTrigger = edgeTrigger;
                    break;
                }
            }

            if (target.startTrigger == null)
            {
                target.startTrigger = ((GameObject)Instantiate(target.EdgeTrigger, target.StartNode.transform)).GetComponent<NavGraphEdgeTrigger>();
                target.startTrigger.ReverseMovement = false;
                target.startTrigger.EdgeMovement = target;
            }

            target.startTrigger.name = "EdgeTrigger_" + edgeNumber;
        }

        if (target.endTrigger == null)
        {
            NavGraphEdgeTrigger[] edgesTriggers = target.EndNode.gameObject.GetComponentsInChildren<NavGraphEdgeTrigger>();
            foreach (NavGraphEdgeTrigger edgeTrigger in edgesTriggers)
            {
                if (edgeTrigger.EdgeMovement == target)
                {
                    target.endTrigger = edgeTrigger;
                    break;
                }
            }

            if (target.endTrigger == null)
            {
                target.endTrigger = ((GameObject)Instantiate(target.EdgeTrigger, target.EndNode.transform)).GetComponent<NavGraphEdgeTrigger>();
                target.endTrigger.ReverseMovement = true;
                target.endTrigger.EdgeMovement = target;
            }

            target.endTrigger.name = "EdgeTrigger_" + edgeNumber + "_R";
        }

        // Set the start and end triggers
        Vector3[] pathPoints = WaypointManager.GetCurved(target.PathContainer.GetPathPoints());

        if (target.startTrigger != null && !target.startTrigger.OverridePlacement)
        {
            Vector3 startTangent = (pathPoints[2] - pathPoints[1]).normalized;
            float distance = target.startTrigger.DistanceToNode;
            target.startTrigger.transform.position = target.StartNode.transform.position + startTangent * distance;
        }

        if (target.endTrigger != null && !target.endTrigger.OverridePlacement)
        {
            Vector3 endTangent = (pathPoints[pathPoints.Length - 3] - pathPoints[pathPoints.Length - 2]).normalized;
            float distance = target.endTrigger.DistanceToNode;
            target.endTrigger.transform.position = target.EndNode.transform.position + endTangent * distance;
        }

        EditorUtility.SetDirty(target);
    }

    public static void UpdateSplines(NavGraphManager target)
    {
        NavGraphEdgeMovement[] edges = target.gameObject.GetComponentsInChildren<NavGraphEdgeMovement>();
        foreach (NavGraphEdgeMovement edge in edges)
        {
            UpdateSpline(edge);
            UpdateTriggers(edge);
        }
    }
}
