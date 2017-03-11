using UnityEngine;
using System.Collections;

public class PlateformElevator : MonoBehaviour
{
    public NavGraphNode NodeDown;
    public NavGraphNode NodeUp;

    private bool upstairs;
    private GameObject player;
    private Transform playerParent;

    void Awake()
    {
        upstairs = false;
        player = GameObject.Find("Player");
        playerParent = player.transform.parent;
    }

    // Use this for initialization
    void Start () {
	
	}


    public void GoUp()
    {
        if( upstairs 
            || NavGraphManager.instance.ActiveNode != NodeDown)
            return;

        NodeDown.LeavingNodeEvent.Invoke();

        player.transform.parent = this.gameObject.transform;
        GetComponent<Animator>().SetBool("isGoingUp", true);
    }

    public void GoDown()
    {
        if (!upstairs
            || NavGraphManager.instance.ActiveNode == NodeDown)
            return;

        NodeUp.LeavingNodeEvent.Invoke();

        player.transform.parent = this.gameObject.transform;
        GetComponent<Animator>().SetBool("isGoingDown", true);
    }

    void GoneUp()
    {
        //gameObject.transform.position += (NodeUp.transform.position - NodeDown.transform.position);
        player.transform.parent = playerParent;
        NodeUp.EnteringNodeEvent.Invoke();
        upstairs = true;

        GetComponent<Animator>().SetBool("isGoingUp", false);
    }

    void GoneDown()
    {
        //gameObject.transform.position -= (NodeUp.transform.position - NodeDown.transform.position);
        player.transform.parent = playerParent;
        NodeDown.EnteringNodeEvent.Invoke();
        upstairs = false;
        GetComponent<Animator>().SetBool("isGoingDown", false);
    }

    public void Toggle()
    {
        if(upstairs)
            GoDown();
        else
            GoUp();
    }
}
