using UnityEngine;
using System.Collections;

public class DDoL : MonoBehaviour {

	void Awake () {
	    DontDestroyOnLoad(gameObject);
        Debug.Log("DDoL : " + gameObject.name);
	}
}
