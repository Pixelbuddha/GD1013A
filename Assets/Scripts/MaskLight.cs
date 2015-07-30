using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Light))]
public class MaskLight : MonoBehaviour {

	// Use this for initialization
	private Light myLight;
	public CharacterControl myCharacter;

	public void TurnLightOff() {
		myLight.intensity = 0;
	}

	public void TurnLightOn() {
		myLight.intensity = 5;
		myLight.color = Color.red;
	}

	void Start () {
		myLight = GetComponent<Light> ();
		//Debug.Log (myLight);
	}

	public void LightControl() {

		if(Input.GetKeyDown(KeyCode.R))
			myLight.intensity ++;



		if (Input.GetKeyDown(KeyCode.F)) 
			myLight.intensity --;

	}


	
	// Update is called once per frame
	void Update () {
		if (myCharacter.activeMask == 2)
			LightControl ();
	}
}
