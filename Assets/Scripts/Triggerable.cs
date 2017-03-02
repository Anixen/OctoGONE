using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Triggerable : MonoBehaviour
{
    public bool locked = false;
    public bool repeatable = true;
    private int timesExecuted = 0;

    public UnityEvent TriggeredEvent;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Execute()
    {
        // Check the triggerable is not locked and can be repeated
        if( locked 
            || (!repeatable && timesExecuted > 0))
            return;

        // Invoke the  event
        TriggeredEvent.Invoke();
        ++timesExecuted;
    }
}
