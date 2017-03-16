using UnityEngine;
using System.Collections;

public class Level_3_Manager : MonoBehaviour
{
    public static Level_3_Manager instance;

    private ButtonPush button_000;
    private NavGraphEdgeMovement edge_003;
    private GameObject passerelle_005;
    private GameObject passerelle_006;
    private GameObject passerelle_007;
    private GameObject passerelle_008;
    private GameObject passerelle_010;
    private GameObject passerelle_013;

    private ButtonPush button_001;
    private NavGraphEdgeMovement edge_005;
    private GameObject passerelle_009;
    private GameObject passerelle_011;

    private ButtonPush button_002;
    private NavGraphEdgeMovement edge_006;
    private GameObject passerelle_012;

    private bool path_1_revaled = false;
    private bool path_2_revaled = false;
    private bool path_3_revaled = false;

    private PortalOpen portal;

    void Awake()
    {
        instance = this;

        button_000 = GameObject.Find("bouton_000").GetComponent<ButtonPush>();
        button_001 = GameObject.Find("bouton_001").GetComponent<ButtonPush>();
        button_002 = GameObject.Find("bouton_002").GetComponent<ButtonPush>();

        edge_003 = GameObject.Find("Edge_003").GetComponent<NavGraphEdgeMovement>();
        edge_005 = GameObject.Find("Edge_005").GetComponent<NavGraphEdgeMovement>();
        edge_006 = GameObject.Find("Edge_006").GetComponent<NavGraphEdgeMovement>();

        passerelle_005 = GameObject.Find("level3_passerelle_rectangulaire_005");
        passerelle_006 = GameObject.Find("level3_passerelle_rectangulaire_006");
        passerelle_007 = GameObject.Find("level3_passerelle_rectangulaire_007");
        passerelle_008 = GameObject.Find("level3_passerelle_rectangulaire_008");
        passerelle_009 = GameObject.Find("level3_passerelle_rectangulaire_009");
        passerelle_010 = GameObject.Find("level3_passerelle_rectangulaire_010");
        passerelle_011 = GameObject.Find("level3_passerelle_rectangulaire_011");
        passerelle_012 = GameObject.Find("level3_passerelle_rectangulaire_012");
        passerelle_013 = GameObject.Find("level3_passerelle_rectangulaire_013");

        portal = GameObject.Find("level3_portail").GetComponent<PortalOpen>();
    }

    // Use this for initialization
    void Start () {
        button_000.GetComponent<Trigger>().Disable();
        button_001.GetComponent<Trigger>().Disable();
        button_002.GetComponent<Trigger>().Disable();

        edge_003.startTrigger.Disable();
        edge_003.endTrigger.Disable();
        edge_005.startTrigger.Disable();
        edge_005.endTrigger.Disable();
        edge_006.startTrigger.Disable();
        edge_006.endTrigger.Disable();

        passerelle_005.SetActive(false);
        passerelle_006.SetActive(false);
        passerelle_007.SetActive(false);
        passerelle_008.SetActive(false);
        passerelle_009.SetActive(false);
        passerelle_010.SetActive(false);
        passerelle_011.SetActive(false);
        passerelle_012.SetActive(false);
        passerelle_013.SetActive(false);

        portal.OpenPortal();

    }

    public void UpdateLevelState()
    {
        if (!path_1_revaled && button_000.Pushed)
        {
            passerelle_005.SetActive(true);
            passerelle_006.SetActive(true);
            passerelle_007.SetActive(true);
            passerelle_008.SetActive(true);
            passerelle_010.SetActive(true);
            passerelle_013.SetActive(true);

            edge_003.startTrigger.Enable();
            edge_003.endTrigger.Enable();

            path_1_revaled = true;
        }

        if (!path_2_revaled && button_002.Pushed)
        {
            passerelle_009.SetActive(true);
            passerelle_011.SetActive(true);

            edge_005.startTrigger.Enable();
            edge_005.endTrigger.Enable();

            path_2_revaled = true;
        }

        if (!path_3_revaled && button_001.Pushed)
        {
            passerelle_012.SetActive(true);

            edge_006.startTrigger.Enable();
            edge_006.endTrigger.Enable();

            path_3_revaled = true;
        }
    }
}
