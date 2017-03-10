using UnityEngine;
using System.Collections;
using UnityEngine.Assertions.Must;

public class Level_0_Manager : MonoBehaviour
{
    public static Level_0_Manager instance;

    private PortalOpen porte_000;
    private PortalOpen porte_001;
    private PortalOpen porte_002;
    private PortalOpen porte_003;

    void Awake()
    {
        instance = this;

        porte_000 = GameObject.Find("porte_000")
            .GetComponent<PortalOpen>();

        porte_001 = GameObject.Find("porte_001")
            .GetComponent<PortalOpen>();

        porte_002 = GameObject.Find("porte_002")
            .GetComponent<PortalOpen>();

        porte_003 = GameObject.Find("porte_003")
            .GetComponent<PortalOpen>();
    }

    // Use this for initialization
    void Start()
    {
        porte_000.OpenPortal();
    }

    // Update is called once per frame
    void Update()
    {
        //UpdateLevelState();
    }

    public void UpdateLevelState()
    {

    }
}
