using UnityEngine;
using System.Collections;

public class App : MonoBehaviour
{
    private string StartingLevel = "level_00";
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
	    levelManager.Load(StartingLevel);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
