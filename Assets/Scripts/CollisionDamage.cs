using UnityEngine;
using System.Collections;

public class CollisionDamage : MonoBehaviour {

	public float damage = 1;
	public float dmgTick = 1;

	void OnTriggerEnter (Collider other) {
		dmgTick = 0;
		if (other.CompareTag ("Player"))
			other.SendMessage ("ApplyDamage", damage);
	}

	void OnTriggerStay (Collider other) {
		
		if (other.CompareTag ("Player")) {
			dmgTick += Time.deltaTime;
			if (dmgTick < 1){
				return;
			}
			other.SendMessage ("ApplyDamage", damage);
			Debug.Log("IdealDamage: " + damage);
			dmgTick = 0;
		}
	}


}
