using UnityEngine;
using System.Collections;

public class FlyingWalls : MonoBehaviour {
	public GameObject wall1;
	public Vector3 crushDirection;
	public float maxDistance;
	private float counter;
	
	bool isCrushing = false;
	// Use this for initialization
	void Start () {
		
		
	}
	void OnTriggerEnter()  {
		isCrushing = true;
		
	}
	
	void Crush () {
		/*if (wall2 = null) {
			wall1.transform.position = (wall1.transform.position + new Vector3 (crushDirection.x, crushDirection.y, 0));
			} else { */
		if (counter <= maxDistance) {
			
			wall1.transform.position = (wall1.transform.position + new Vector3 (crushDirection.x, crushDirection.y, 0)); 

		} else {
			isCrushing = false;
		}
		counter ++;
	}
	//}
	
	// Update is called once per frame
	void Update () {
		
		if (isCrushing) 
			Invoke ("Crush", 1);
		
	}
}