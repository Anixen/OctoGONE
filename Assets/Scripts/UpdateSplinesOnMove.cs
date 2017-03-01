using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class UpdateSplinesOnMove : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (transform.hasChanged)
        {
            transform.parent.GetComponent<NavGraphManager>().UpdateSplines();
            transform.hasChanged = false;
        }
    }
}
