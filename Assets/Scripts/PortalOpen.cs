using UnityEngine;
using System.Collections;

public class PortalOpen : MonoBehaviour {

    public bool isOpened { get { return (meshRenderer.enabled && trigger.enabled) ; } }

    private GameObject portalSurface;
    private MeshRenderer meshRenderer;
    private Trigger trigger;

    void Awake()
    {
        portalSurface = GetComponentInChildren<PortalEnter>().gameObject;
        meshRenderer = portalSurface.GetComponent<MeshRenderer>();
        trigger = portalSurface.GetComponent<Trigger>();
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void TogglePortal()
    {
        if(isOpened)
            ClosePortal();
        else
            OpenPortal();
    }

    public void OpenPortal()
    {
        //Debug.Log("Open Portal :" + name);
        portalSurface.GetComponent<MeshRenderer>().enabled = true;
        portalSurface.GetComponent<Trigger>().Unlock();
    }

    public void ClosePortal()
    {
        //Debug.Log("Close Portal :" + name);
        portalSurface.GetComponent<Trigger>().Lock();
        portalSurface.GetComponent<MeshRenderer>().enabled = false;
    }
}
