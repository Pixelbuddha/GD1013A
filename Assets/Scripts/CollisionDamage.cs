﻿using UnityEngine;
using System.Collections;

public class CollisionDamage : MonoBehaviour {

	public float damage = 1;

	void OnTriggerEnter2D (Collider2D other) {

		if (other.CompareTag ("cc"))
			other.SendMessage ("ApplyDamage", damage);
	}

	void OnTriggerStay2D (Collider2D other) {
		
		if (other.CompareTag ("cc"))
			other.SendMessage ("ApplyDamage", damage);
	}


}
