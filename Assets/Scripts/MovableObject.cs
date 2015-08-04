using UnityEngine;
using System.Collections;



public class MovableObject : MonoBehaviour {

	public CharacterControl chara;
	public Vector3 moveMe = Vector3.zero;
	private Vector3 prevPos;
	private Vector3 curPos;
	private float lastTime;
	private float curTime;
	// Use this for initialization
	void Start () {

		lastTime = Time.time;
		curTime = Time.time;

		prevPos = transform.position;
		curPos = transform.position;

	}

	void OnTriggerStay() {
		if (chara.activeMask == 3) {
			if (Input.GetKey (KeyCode.S)) {
				Move ();
			}
		}
	}


	void Move() {
		lastTime = curTime;
		curTime = Time.time;
		prevPos = curPos;
		curPos += new Vector3 (chara.moveDirection.x * Time.deltaTime, 0, 0);
		
	}

	// Update is called once per frame
	void Update () {


		float deltaTime = curTime - lastTime;
		float deltaTimeNew = Time.time - curTime;
		if (deltaTime == 0)
			return;
		transform.position = Vector3.Lerp(prevPos, curPos, (deltaTimeNew / deltaTime) );

	}


}
