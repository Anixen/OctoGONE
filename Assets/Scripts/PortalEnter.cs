using UnityEngine;
using System.Collections;

public class PortalEnter : MonoBehaviour
{
    public string destinationScene;

    private LevelManager levelManager;

    void Awake()
    {
        levelManager = Object.FindObjectOfType<LevelManager>();
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void EnterPortal()
    {
        Debug.Log("Entering portal : " + transform.parent.name);

        if(levelManager != null)
            levelManager.Load(destinationScene);
    }
}
