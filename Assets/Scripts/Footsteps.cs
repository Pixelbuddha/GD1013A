using UnityEngine;
using System.Collections;

public class Footsteps : MonoBehaviour {
	//HealthController hc;
	CharacterController cc;
	CharacterControl auchCC;
	public AudioClip walkSound;
	public AudioClip crouchSound;
	public AudioClip landSound;
	public AudioClip wimmerSound;
	public AudioClip DieSound;

	// Use this for initialization
	void Start () {
		//hc = GetComponent<HealthController> ();
		cc = GetComponent<CharacterController> ();
		auchCC = GetComponent<CharacterControl> ();
	}
	
	// Update is called once per frame


		void Update () {
		//if (hc.healthGUI.fillAmount < 1){
			if (cc.isGrounded == true && GetComponent<AudioSource>().isPlaying == false && Mathf.Abs(cc.velocity.x) >= 1f) {
				if(auchCC.isCrouchedCheck == false) {
					GetComponent<AudioSource>().clip = walkSound;
				} else {
					GetComponent<AudioSource>().clip = crouchSound;
				}
				
				GetComponent<AudioSource>().volume  = Random.Range(0.2f, 0.6f);
				GetComponent<AudioSource>().pitch = Random.Range(0.85f,1.1f);
				GetComponent<AudioSource>().Play();
			}
	/*	} else {
			GetComponent<AudioSource>().clip = DieSound;
			GetComponent<AudioSource>().Play();
		}*/

	
}
}
