using UnityEngine;
using System.Collections;

public class FlyingWalls : MonoBehaviour {
	public GameObject wall1;
	public Vector3 crushSpeed;
	public float maxDuration;
	private float counter = 0;
	public bool redMaskActivator;
	public CharacterControl chara;
	
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
		if (counter <= maxDuration) {				
			
			
			if (!redMaskActivator) {
				
				wall1.transform.position = (wall1.transform.position + new Vector3 (crushSpeed.x, crushSpeed.y, 0)); 
			} else {
				if (chara.activeMask == 2) {
					Debug.Log ("HALLO JANNIS" + counter + "" + maxDuration + "");
					if (Input.GetKeyDown(KeyCode.P))
						wall1.transform.position = (wall1.transform.position + new Vector3 (crushSpeed.x, crushSpeed.y, 0));
					
					
					counter = 0; 
				}
			}
		} else {
			
			//isCrushing = false;
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