using UnityEngine;
using System.Collections;
using SWS;

public class NavGraphManager : MonoBehaviour {

    public static NavGraphManager instance = null;
    public splineMove mover = null;
    public NavGraphNode StartNode = null;

    private NavGraphNode activeNode = null;

    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
            //if not, set instance to this
            instance = this;
        //If instance already exists and it's not this:
        else if (instance != this)
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
    }

    // Use this for initialization
     void Start ()
     {
        StartNode.EnteringNodeEvent.Invoke();
        mover.transform.position = activeNode.transform.position;
     }
	
	// Update is called once per frame
	void Update () {
	
	}

    public NavGraphNode ActiveNode
    {
        get { return activeNode; }
        set { activeNode = value; Debug.Log("New Active Node : " + activeNode.name);}
    }

    public void UpdateSplines()
    {
        NavGraphEdgeMovement[] edges = gameObject.GetComponentsInChildren<NavGraphEdgeMovement>();
        foreach (NavGraphEdgeMovement edge in edges)
            edge.updateSpline();
    }
}
