using UnityEngine;
using System.Collections;

public class IAmACheckpoint : MonoBehaviour {
	
	public CharacterControl player;

	void Start () {

	}

	void OnTriggerEnter(Collider col) {

		if (col.tag == "Player") {
			player.currentCheckpoint = this.gameObject;
		}

	}

	// Update is called once per frame
	void Update () {
	
	}
}
