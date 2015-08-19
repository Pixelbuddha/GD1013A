using UnityEngine;
using System.Collections;

public class ChildDesintegrate : MonoBehaviour {

	private Animator childAnim;
	public GameObject model;

	void Start() {
		childAnim = model.GetComponent<Animator> ();
	}

	void OnTriggerEnter(Collider col) {
		
		if (col.tag == "Player") {
			Debug.Log("HIT");
			childAnim.SetBool("Disappear", true);
			Destroy(this.gameObject, 6.5f);
					}
			}
	}
