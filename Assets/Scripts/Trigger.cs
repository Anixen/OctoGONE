using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(EventTrigger))]
public class Trigger : MonoBehaviour
{
    public bool locked = false;
    public bool repeatable = true;
    private int timesTriggered = 0;

    public List<Triggerable> Triggerables;

    private Collider collider;
    private EventTrigger eventTrigger;


    void Awake()
    {
        collider = GetComponent<Collider>();
        eventTrigger = GetComponent<EventTrigger>();

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
    public void OnGazeEnter(BaseEventData data)
    {
        // TODO Higlight the object : change its color, show its outline
    }

    /// Called when the user stops looking on the GameObject, after OnGazeEnter
    /// was already called.
    public void OnGazeExit(BaseEventData data)
    {
        // TODO Reset the highlight apllied to the object
    }

    /// Called when the viewer's trigger is used, between OnGazeEnter and OnPointerExit.
    public void OnGazeTrigger(BaseEventData data)
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
