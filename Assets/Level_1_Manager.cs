using UnityEngine;
using System.Collections;
using UnityEngine.Assertions.Must;

public class Level_1_Manager : MonoBehaviour
{
    public static Level_1_Manager instance;

    private FlameToggle flame_000;
    private FlameToggle flame_011;
    private NavGraphEdgeMovement edge_006;
    private PortalActivate exit;

    private bool exitOpened = false;

    void Awake()
    {
        instance = this;

        flame_000 = GameObject.Find("torche_000").
            transform.FindChild("feu").GetComponent<FlameToggle>();
        flame_011 = GameObject.Find("torche_011").
            transform.FindChild("feu").GetComponent<FlameToggle>();

        edge_006 = GameObject.Find("Edge_006").GetComponent<NavGraphEdgeMovement>();

        exit = GameObject.Find("exit").GetComponent<PortalActivate>();
    }

	// Use this for initialization
	void Start ()
	{
        exit.gameObject.SetActive(false);

	    edge_006.startTrigger.Disable();
	    edge_006.endTrigger.Disable();

        flame_000.gameObject.SetActive(false);
        flame_011.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
	    //UpdateLevelState();
	}

    public void UpdateLevelState()
    {
        if (!exitOpened
            && (flame_000.active && flame_011.active))
        {
            exit.gameObject.SetActive(true);

            edge_006.startTrigger.Enable();
            edge_006.endTrigger.Enable();

            exitOpened = true;
        }

        if (exitOpened
            && !(flame_000.active && flame_011.active))
        {
            exit.gameObject.SetActive(false);

            edge_006.startTrigger.Disable();
            edge_006.endTrigger.Disable();

            exitOpened = false;
        }
    }
}
