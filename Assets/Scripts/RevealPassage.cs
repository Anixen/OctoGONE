using UnityEngine;
using System.Collections;

public class RevealPassage : MonoBehaviour
{
    public float PosY_revaled;
    public float PosY_hidden;

    private NavGraphEdgeMovement edge_005;

    private bool revealed = true;

    void Awake()
    {
        edge_005 = GameObject.Find("Edge_005").GetComponent<NavGraphEdgeMovement>();
        //Hide();
    }

    // Use this for initialization
    void Start ()
	{
        //Hide();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Reveal()
    {
        Debug.Log("Reveal");
        if (revealed)
            return;

        gameObject.SetActive(true);
        /*
        transform.position.Set(
            transform.position.x,
            PosY_revaled,
            transform.position.z);
        //*/

        edge_005.startTrigger.Enable();
        edge_005.endTrigger.Enable();

        revealed = true;
    }

    public void Hide()
    {
        Debug.Log("Hide");
        if(!revealed)
            return;

        edge_005.startTrigger.Disable();
        edge_005.endTrigger.Disable();

        gameObject.SetActive(false);
        /*
        transform.position.Set(
            transform.position.x,
            PosY_hidden,
            transform.position.z);
        //*/

        revealed = false;
    }
}
