using UnityEngine;
using System.Collections;

public class FlyingWalls : MonoBehaviour {
	public GameObject wall1;
	public Vector3 crushDirection;
	public float maxDistance;
	private float counter = 0;
	public bool redMaskActivator;
	public CharacterControl kommschon;
	
	bool isCrushing = false;
	// Use this for initialization
	void Start () {
		
	}

	void SetIsCrushing() {
		isCrushing = true;
	}
	void OnTriggerEnter()  {
		Invoke("SetIsCrushing", 1);
		
	}

	void OnTriggerExit() {
		isCrushing = false;
	}
	
	void Crush () {

		/*if (wall2 = null) {
			wall1.transform.position = (wall1.transform.position + new Vector3 (crushDirection.x, crushDirection.y, 0));
			} else { */
		if (counter <= maxDistance) {				
			Debug.Log ("HALLO JANNIS" + counter + "" + maxDistance + "");

			if (!redMaskActivator) {
				wall1.transform.position = (wall1.transform.position + new Vector3 (crushDirection.x, crushDirection.y, 0)); 
			} else {
				if (kommschon.activeMask == 2) {

					if (Input.GetKeyDown(KeyCode.P))
						wall1.transform.position = (wall1.transform.position + new Vector3 (crushDirection.x, crushDirection.y, 0));
					maxDistance = -maxDistance;
					crushDirection = -crushDirection;
					counter = 0;
				}
			}
		} else {

			isCrushing = false;
		}
		counter ++;
	}
	//}

	void Update () {
		
		if (isCrushing) {

			Crush();
		}
	}
}