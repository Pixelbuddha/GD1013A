using UnityEngine;
using System.Collections;

public class PlatformMovement : MonoBehaviour {

	public float ausschlagX;
	public float ausschlagY;

	public float frequenz;
	private Vector3 platformMoveDirection = Vector3.zero;

	public float startX;
	public float startY;
	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
		platformMoveDirection.x = startX - frequenz + (frequenz+Mathf.Sin (Time.timeSinceLevelLoad*frequenz) * ausschlagX);
		platformMoveDirection.y = startY - frequenz + (frequenz+Mathf.Sin (Time.timeSinceLevelLoad*frequenz) * ausschlagY);
		this.transform.position = platformMoveDirection;

	}
}
// wtf = frequenz
// frequenz = start
// ausschlag richtig