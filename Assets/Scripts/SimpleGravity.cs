using UnityEngine;
using System.Collections;

public class SimpleGravity : MonoBehaviour {

	private Vector3 moveDirection = Vector3.zero;
	private float gravity = 0.5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		moveDirection.y -= gravity * Time.deltaTime;
		this.transform.position = (this.transform.position + new Vector3 (0, moveDirection.y, 0));
			//Debug.Log ("STONESPEED" + moveDirection.y + "");

	}
}
