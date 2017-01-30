using UnityEngine;
using System.Collections;

using SWS;

public class WalkerMovement : MonoBehaviour
{
    public splineMove walker;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	    transform.position += walker.transform.localPosition;
	    walker.transform.localPosition = Vector3.zero;
	}
}
