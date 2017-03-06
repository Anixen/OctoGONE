using System;
using UnityEngine;
using System.Collections;
using SWS;

public class NavGraphBuilder : MonoBehaviour {

    public static void UpdateSpline(NavGraphEdgeMovement target)
    {
        Debug.Log("Update spline of " + target.name);
        target.PathContainer.gameObject.transform.GetChild(0).transform.position = 
            target.StartNode.transform.position;
        target.PathContainer.gameObject.transform.GetChild(target.PathContainer.waypoints.Length - 1).transform.position =
            target.EndNode.transform.position;
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
                    target.startTrigger = edgeTrigger;
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
                    target.endTrigger = edgeTrigger;
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

        if (target.startTrigger != null)
        {
            Vector3 startTangent = (pathPoints[2] - pathPoints[1]).normalized;
            target.startTrigger.transform.position = target.StartNode.transform.position + 2.0f * startTangent;
        }

        if (target.endTrigger != null)
        {
            Vector3 endTangent = (pathPoints[pathPoints.Length - 3] - pathPoints[pathPoints.Length - 2]).normalized;
            target.endTrigger.transform.position = target.EndNode.transform.position + 2.0f * endTangent;
        }
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
