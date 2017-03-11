using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StatueRotation : MonoBehaviour {

    public List<float> RotationsAngles;
    public int correctRotationIndex = 0;

    public bool hasCorrectRotation { get { return (currentAngleIndex == correctRotationIndex); } }

    private int currentAngleIndex = 0;
    private bool invertNext = false;

    void Awake()
    {
        
    }

    // Use this for initialization
    void Start () {
        SetRotation(0);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void MoveNextRotation()
    {
        if (invertNext)
            SetRotation(currentAngleIndex - 1);
        else
            SetRotation(currentAngleIndex + 1);

        Debug.Log("MovedNextRotation, currentAngleIndex = " + currentAngleIndex + ", hasCorrectRotation = " + hasCorrectRotation);
    }

    public void SetRotation(int angleIndex)
    {
        if(angleIndex < 0 || angleIndex > RotationsAngles.Count - 1)
            return;

        currentAngleIndex = angleIndex;

        // Toogle iteration order if we have reached an end of the list
        if (currentAngleIndex == 0)
            invertNext = false;
        if (currentAngleIndex == RotationsAngles.Count - 1)
            invertNext = true;

        transform.localRotation = Quaternion.Euler(
            transform.localRotation.x,
            RotationsAngles[currentAngleIndex],
            transform.localRotation.z);

        Level_4_Manager.instance.UpdateLevelState();
    }
}
