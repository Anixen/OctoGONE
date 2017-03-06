using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.Events;

public class NavGraphNode : MonoBehaviour
{
    public UnityEvent LeavingNodeEvent;
    public UnityEvent EnteringNodeEvent;

    // Environnment triggers that should be enabled/disabled unpon entering/leaving the node
    // The movement triggers are assigned as child objects 
    public List<Trigger> Triggers;

    // Awake is always called before any Start functions
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
	void Update ()
    {

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.7f * GetHandleSize(transform.position));
    }

    public virtual float GetHandleSize(Vector3 pos)
    {
        float handleSize = 1f;
        #if UNITY_EDITOR
        handleSize = UnityEditor.HandleUtility.GetHandleSize(pos) * 0.4f;
        handleSize = Mathf.Clamp(handleSize, 0, 1.2f);
        #endif
        return handleSize;
    }

    void OnEnterNode()
    {
        Debug.Log("Entering " + this.name);

        NavGraphManager.instance.ActiveNode = this;

        // De-activate all associted environment triggers
        foreach (var trigger in Triggers)
            trigger.Enable();

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
        // De-activate all associted environment triggers
        foreach (var trigger in Triggers)
            trigger.Disable();

        // De-activate all associated (children) movement triggers
        foreach (Transform child in transform)
        {
            NavGraphEdgeTrigger edgeTrigger = child.GetComponent<NavGraphEdgeTrigger>();
            if (edgeTrigger != null)
                edgeTrigger.Hide();
        }
    }
}
