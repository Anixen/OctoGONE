using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Collider))]
public class Trigger : MonoBehaviour
{
    public bool locked = false;
    public bool repeatable = true;
    private int timesTriggered = 0;

    public List<Triggerable> Triggerables;


    private Collider collider;


    void Awake()
    {
        collider = GetComponent<Collider>();
    }

    public void Enable()
    {
        collider.enabled = true;
    }

    public void Disable()
    {
        collider.enabled = false;
    }

    public void Lock()
    {
        locked = true;
    }

    public void Unlock()
    {
        locked = false;
    }

    /// Called when the user is looking on a GameObject with this script,
    /// as long as it is set to an appropriate layer (see GvrGaze).
    public void OnGazeEnter()
    {
        // TODO Higlight the object : change its color, show its outline
    }

    /// Called when the user stops looking on the GameObject, after OnGazeEnter
    /// was already called.
    public void OnGazeExit()
    {
        // TODO Reset the highlight apllied to the object
    }

    /// Called when the viewer's trigger is used, between OnGazeEnter and OnPointerExit.
    public void OnGazeTrigger()
    {
        // Check the triggerable is not locked and can be repeated
        if (locked
            || (!repeatable && timesTriggered > 0))
            return;

        // Execute the associated Triggerables
        foreach (var t in Triggerables)
        {
            t.Execute();
        }

        ++timesTriggered;
    }
}
