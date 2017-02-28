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
