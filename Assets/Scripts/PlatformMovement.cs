using UnityEngine;
using System.Collections;

public class PlatformMovement : MonoBehaviour {

	public float ausschlagX;
	public float ausschlagY;

	public float frequenz;
	private Vector3 position = Vector3.zero;

	public float startX;
	public float startY;
	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {


		position.x = startX - frequenz + (frequenz+Mathf.Sin (Time.timeSinceLevelLoad*frequenz) * ausschlagX);
		position.y = startY - frequenz + (frequenz+Mathf.Sin (Time.timeSinceLevelLoad*frequenz) * ausschlagY);
		this.transform.localPosition = position;


	}
}
// wtf = frequenz
// frequenz = start
// ausschlag richtig