using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class NavGraphNode : MonoBehaviour
{
    public UnityEvent LeavingNodeEvent;
    public UnityEvent EnteringNodeEvent;

    //Awake is always called before any Start functions
    void Awake()
    {
        LeavingNodeEvent.AddListener(OnLeaveNode);
        EnteringNodeEvent.AddListener(OnEnterNode);
    }

    // Use this for initialization
    void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnEnterNode()
    {
        Debug.Log("Entering " + this.name);

        NavGraphManager.instance.ActiveNode = this;

        // TODO
        // De-activate all associted environment triggers

        // Activate all associated (children) movement triggers
        foreach (Transform child in transform)
        {
            NavGraphEdgeTrigger edgeTrigger = child.GetComponent<NavGraphEdgeTrigger>();
            if (edgeTrigger != null)
                edgeTrigger.UnHide();
        }
    }

    void OnLeaveNode()
    {
        Debug.Log("Leaving " + this.name);
        // TODO
        // De-activate all associted environment triggers

        // De-activate all associated (children) movement triggers
        foreach (Transform child in transform)
        {
            NavGraphEdgeTrigger edgeTrigger = child.GetComponent<NavGraphEdgeTrigger>();
            if (edgeTrigger != null)
                edgeTrigger.Hide();
        }
    }
}
