using UnityEngine;
using System.Collections;

public class FlameToggle : MonoBehaviour
{
    public bool active
    {
        get { return gameObject.activeSelf; }
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
        Level_1_Manager.instance.UpdateLevelState();
    }
}
