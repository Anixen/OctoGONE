using UnityEngine;
using System.Collections;

public class Level_4_Manager : MonoBehaviour
{
    public static Level_4_Manager instance;

    private StatueRotation statue_000;
    private StatueRotation statue_001;
    private StatueRotation statue_002;
    private StatueRotation statue_003;

    private GameObject chest_01;

    private bool chestOpened;

    private RevealPassage passage_rochers;

    void Awake()
    {
        instance = this;

        statue_000 = GameObject.Find("statue_000").
            transform.FindChild("tete_rotator_000").GetComponent<StatueRotation>();

        statue_001 = GameObject.Find("statue_001").
            transform.FindChild("tete_rotator_001").GetComponent<StatueRotation>();

        statue_002 = GameObject.Find("statue_002").
            transform.FindChild("tete_rotator_002").GetComponent<StatueRotation>();

        statue_003 = GameObject.Find("statue_003").
            transform.FindChild("tete_rotator_003").GetComponent<StatueRotation>();

        chest_01 = GameObject.Find("chest_01");
        chest_01.transform.FindChild("chest_01_01").GetComponent<Trigger>().Lock();
        chest_01.transform.FindChild("chest_01_02").localRotation = Quaternion.Euler(-45, 90, -90);

        chestOpened = false;
        
        passage_rochers = GameObject.Find("passage_rochers").GetComponent<RevealPassage>();
    }

    // Use this for initialization
    void Start () {

        statue_000.SetRotation(2);
        statue_001.SetRotation(3);
        statue_002.SetRotation(1);
        statue_003.SetRotation(0);

        passage_rochers.Hide();
        passage_rochers.GetComponent<Triggerable>().Lock();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void UpdateLevelState()
    {
        if (!chestOpened
            && (statue_000.hasCorrectRotation 
                && statue_001.hasCorrectRotation 
                && statue_002.hasCorrectRotation 
                && statue_003.hasCorrectRotation)
            )
        {
            Debug.Log("Opening chest");

            statue_000.GetComponent<Trigger>().Lock();
            statue_001.GetComponent<Trigger>().Lock();
            statue_002.GetComponent<Trigger>().Lock();
            statue_003.GetComponent<Trigger>().Lock();

            chest_01.transform.FindChild("chest_01_01").GetComponent<Trigger>().Unlock();
            chest_01.transform.FindChild("chest_01_01").GetComponent<Trigger>().Enable();
            chest_01.transform.FindChild("chest_01_02").localRotation = Quaternion.Euler(0, 90, -90);

            passage_rochers.GetComponent<Triggerable>().Unlock();

            chestOpened = true;
        }
    }
}
