using UnityEngine;
using System.Collections;

public class ButtonPush : MonoBehaviour
{
    public Material initialMaterial;
    public Material pushedMaterial;

    private bool pushed;
    public bool Pushed { get { return pushed;} }

    
	// Use this for initialization
	void Start ()
	{
	    gameObject.GetComponent<MeshRenderer>().material = initialMaterial;
	}

    public void Push()
    {
        gameObject.GetComponent<MeshRenderer>().material = pushedMaterial;
        pushed = true;

        gameObject.GetComponent<Trigger>().Lock();
        gameObject.GetComponent<Triggerable>().Lock();

        Level_3_Manager.instance.UpdateLevelState();
    }
}
