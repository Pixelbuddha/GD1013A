using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndScene : MonoBehaviour {

	public Image endScreen;
	private bool finishHim = false;
	private int i;

	// Use this for initialization
	void Start () {

	}


	void OnTriggerEnter(Collider col) {

		if (col.tag == "Player") {
			finishHim = true;


			}

		}



	
	// Update is called once per frame
	void Update () {
	 if (finishHim)
			i ++;
			Color textureColor = endScreen.color;
			textureColor.a = Mathf.Clamp(i, 0, 255);
			endScreen.color = textureColor;
	}
}
