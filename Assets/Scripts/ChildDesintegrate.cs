using UnityEngine;
using System.Collections;

public class ChildDesintegrate : MonoBehaviour {

	public CharacterControl player;
	
	void Start () {
		
	}
	
	void OnTriggerEnter(Collider col) {
		
		if (col.tag == "Player") {

		}
		
	}

	void Update () {
	
	}
}
