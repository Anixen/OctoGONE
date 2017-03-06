using UnityEngine;
using SWS;
using UnityEditor;
using UnityEngine.Events;

public class NavGraphEdgeMovement : MonoBehaviour {

    // Reference to path
    [SerializeField]
    public PathManager PathContainer;
    [SerializeField]
    public NavGraphEdgeTrigger startTrigger;
    [SerializeField]
    public NavGraphEdgeTrigger endTrigger;

    public NavGraphNode StartNode;
    public NavGraphNode EndNode;
    public DG.Tweening.PathType movementType;

    public GameObject EdgeTrigger; // Prefab

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        WaypointManager.DrawCurved(PathContainer.GetPathPoints());
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

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}