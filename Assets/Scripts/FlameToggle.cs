using UnityEngine;
using System.Collections;

public class FlameToggle : MonoBehaviour
{
    public bool active
    {
        get { return gameObject.activeInHierarchy; }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Toggle()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
