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

    public void DrawHandle()
    {
        Vector3 nodePos = transform.position;
        float size = HandleUtility.GetHandleSize(nodePos) * 0.5f;

        //do not draw node header if too far away
        if (size < 3f)
        {
            //begin 2D GUI block
            Handles.BeginGUI();
            //translate waypoint vector3 position in world space into a position on the screen
            var guiPoint = HandleUtility.WorldToGUIPoint(nodePos);
            //create rectangle with that positions and do some offset
            var rect = new Rect(guiPoint.x - 50.0f, guiPoint.y - 40, 100, 20);
            //draw box at position with current waypoint name
            GUI.Box(rect, transform.name);
            Handles.EndGUI(); //end GUI block
        }

        //draw handles for the node, clamp size
        Handles.color = Color.red;
        size = Mathf.Clamp(size, 0, 1.2f);
        Vector3 newPos = Handles.FreeMoveHandle(nodePos, Quaternion.identity,
                         size, Vector3.zero, Handles.SphereCap);
        Handles.RadiusHandle(Quaternion.identity, nodePos, size / 2);

        if (nodePos != newPos)
        {
            Undo.RecordObject(transform, "Move Handles");
            transform.position = newPos;
        }
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
