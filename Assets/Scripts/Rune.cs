using UnityEngine;
using System.Collections;

public class Rune : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireCube(transform.position, new Vector3(0.8f, 0.8f, 0.1f));
	}
}
