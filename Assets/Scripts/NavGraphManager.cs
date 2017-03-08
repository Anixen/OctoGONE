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
        instance = this;
        /*
        //Check if instance already exists
        if (instance == null)
            //if not, set instance to this
            instance = this;
        //If instance already exists and it's not this:
        else if (instance != this)
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        //*/
    }

    // Use this for initialization
     void Start ()
     {
        Initialize();
     }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Initialize()
    {
        StartNode.EnteringNodeEvent.Invoke();
        mover.transform.position = activeNode.transform.position;
    }

    public NavGraphNode ActiveNode
    {
        get { return activeNode; }
        set { activeNode = value; Debug.Log("New Active Node : " + activeNode.name);}
    }
}
