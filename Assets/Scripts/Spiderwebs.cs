using UnityEngine;
using System.Collections;

public class Spiderwebs : MonoBehaviour {

	public bool inWeb = false;

	void Start () {	
	}


	void OnTriggerEnter() {
		inWeb = true;
	}

	void OnTriggerExit() {
		inWeb = false;
	}

	// Update is called once per frame
	void Update () {
	
	}
}
