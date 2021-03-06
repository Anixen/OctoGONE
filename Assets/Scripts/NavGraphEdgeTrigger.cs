﻿using UnityEngine;
using System.Collections;

using SWS;
using UnityEngine.EventSystems;

//[RequireComponent(typeof(Collider))]
public class NavGraphEdgeTrigger : MonoBehaviour
{
    // Reference to path
    public NavGraphEdgeMovement EdgeMovement;
    public bool ReverseMovement = false;
    public bool Enabled = true;

    public Material InactiveMaterial;
    public Material GazedAtMaterial;

    public bool OverridePlacement = false; // Set this to true if you don t want your trigger position to be changed by the NavGraphBuilder 
    public float DistanceToNode = 3.0f;

    private GameObject cube; // Reference to the cube used for user interaction

    //private float rotationSpeed;

    void Awake()
    {
        cube = transform.FindChild("Cube").gameObject;

        EventTrigger eventTrigger = transform.GetChild(0).GetComponent<EventTrigger>();
        if (eventTrigger != null)
        {
            // Set up entries here intead of doing it manually
            EventTrigger.Entry pointerEnter_entry = new EventTrigger.Entry();
            pointerEnter_entry.eventID = EventTriggerType.PointerEnter;
            pointerEnter_entry.callback.AddListener(OnGazeEnter);
            eventTrigger.triggers.Add(pointerEnter_entry);

            EventTrigger.Entry pointerExit_entry = new EventTrigger.Entry();
            pointerExit_entry.eventID = EventTriggerType.PointerExit;
            pointerExit_entry.callback.AddListener(OnGazeExit);
            eventTrigger.triggers.Add(pointerExit_entry);

            EventTrigger.Entry pointerClick_entry = new EventTrigger.Entry();
            pointerClick_entry.eventID = EventTriggerType.PointerClick;
            pointerClick_entry.callback.AddListener(OnGazeTrigger);
            eventTrigger.triggers.Add(pointerClick_entry);
        }

        Hide();
    }

    void Start()
    {
    }

    void LateUpdate()
    {
        GvrViewer.Instance.UpdateState();
        if (GvrViewer.Instance.BackButtonPressed)
        {
            Application.Quit();
        }
    }

    void OnDrawGizmos()
    {
        float handleSize = 1f;
        #if UNITY_EDITOR
        handleSize = UnityEditor.HandleUtility.GetHandleSize(transform.position) * 0.4f;
        handleSize = Mathf.Clamp(handleSize, 0, 1.2f);
        #endif

        Gizmos.color = Color.white;
        Transform cube = transform.FindChild("Cube");
        Gizmos.DrawWireCube(cube.position, new Vector3(1, 1, 1) * 0.7f * handleSize);
    }


    public void SetGazedAt(bool gazedAt)
    {
        if (InactiveMaterial != null && GazedAtMaterial != null)
        {
            cube.GetComponent<Renderer>().material = gazedAt ? GazedAtMaterial : InactiveMaterial;
            return;
        }
        cube.GetComponent<Renderer>().material.color = gazedAt ? Color.green : Color.red;
    }

    public void Hide()
    {
        Debug.Log("Hide : " + name);
        SetGazedAt(false);
        cube.SetActive(false);
    }

    public void UnHide()
    {
        Debug.Log("Unhide : " + name + ", Enabled = " + Enabled);
        if (!Enabled)
            return;
        cube.SetActive(true);
        SetGazedAt(false);
    }

    public void Enable()
    {
        Enabled = true;

        // If the parent node is currently the active one, reveal the trigger
        if (NavGraphManager.instance.ActiveNode.gameObject == transform.parent.gameObject)
        {
            UnHide();
        }
            
    }

    public void Disable()
    {
        Hide();

        SetGazedAt(false);
        Enabled = false;            
    }

    // Trigger movement function here
    public void StartMovement(splineMove mover)
    {
        EdgeMovement.StartMovement(mover, ReverseMovement);
    }

    /// Called when the user is looking on a GameObject with this script,
    /// as long as it is set to an appropriate layer (see GvrGaze).
    public void OnGazeEnter(BaseEventData data)
    {
        if (!Enabled)
            return;

        SetGazedAt(true);
    }

    /// Called when the user stops looking on the GameObject, after OnGazeEnter
    /// was already called.
    public void OnGazeExit(BaseEventData data)
    {
        if (!Enabled)
            return;

        SetGazedAt(false);
    }

    /// Called when the viewer's trigger is used, between OnGazeEnter and OnPointerExit.
    public void OnGazeTrigger(BaseEventData data)
    {
        if (!Enabled)
            return;

        StartMovement(NavGraphManager.instance.mover); 
    }
}
