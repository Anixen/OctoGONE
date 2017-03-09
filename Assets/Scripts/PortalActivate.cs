using UnityEngine;
using System.Collections;

public class PortalActivate : MonoBehaviour
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

    public void ActivatePortal()
    {
        Debug.Log("Activated portal : " + name);

        levelManager.Load(destinationScene);
    }
}
