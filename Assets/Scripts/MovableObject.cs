using UnityEngine;
using System.Collections;



public class MovableObject : MonoBehaviour {

	public CharacterControl chara;
	public Vector3 moveMe = Vector3.zero;
	// Use this for initialization
	void Start () {
	

	}

	void MoveThis() {
		if (chara.activeMask == 3) {
			if (Input.GetKeyDown (KeyCode.S)) {
				this.moveMe = chara.moveDirection;
			}
		}
	}

	// Update is called once per frame
	void Update () {
		MoveThis ();
	
	}
}
