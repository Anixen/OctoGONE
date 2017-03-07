using UnityEngine;
using System.Collections;

public class App : MonoBehaviour
{
    private LevelManager levelManager;
    // TODO SaveManager
    // TODO GameManager

    void Awake()
    {
        levelManager = Object.FindObjectOfType<LevelManager>();
    }

	// Use this for initialization
	void Start ()
	{
	    levelManager.Load("level_00");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
