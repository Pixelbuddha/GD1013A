﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthController : MonoBehaviour {


	public int startHealth = 10000;
	public float currenthealth;
	public int healthReg;
	//public int statLifePoints = 3;
	private float health;
	private bool isDead = false;
	public bool debugDead = false;
	private bool isDamageable = true;
	public float healthRegSpeed;
	private float lives = 100;
	//private int lifePoints = 3;
	
	public CharacterControl player;

	// GUI

	public Image healthGUI;
	public Image nearDeath;


	public Canvas canvasReference;

	void Start () {
		//anim = GetComponent<Animator>();
		player = GetComponent<CharacterControl> ();
		health = startHealth;
		//Der Level-Index muss dem Spiel entsprechend angepasst werden, wenn es z.B. eine begrüßungsszene gibt (oder ein Hauptmenü)
		if (Application.loadedLevel == 0) {


			UpdateView();
		}
	}

	public void HealthRegeneration () {
			if (health <= startHealth) {
				health += healthReg;
			} else {
				health = startHealth;
			}

			if (health <= 0 )
				Die ();
		UpdateView ();
	}

	public void RepeatHealthReg() {
		InvokeRepeating("HealthRegeneration", 1, 0.01f);
	}



	void ApplyDamage(int damage) {
		if (damage >= startHealth) {
			Die ();
		}

		if (isDamageable) {
			health -= damage;

			health = Mathf.Max (0, health);
		
			if (isDead) {
				return;
			}
			if (health == 0) {
				isDead = true;
				Die ();

			} else {
				if (isDamageable) {
					Damaging ();
				}
			}
			Debug.Log ("" + health + "");
			isDamageable = false;
			Invoke ("ResetIsDamageable", 1);
		}

	}

	void ResetIsDamageable() {
		isDamageable = true;
	}
	

	void Die(){
	
		// anim.SetBool("Dying", true);
		// lifepoints --;
		//if (lifepoints <=0){
		//Invoke("StartGame",3);  // Startet das Spiel 3 Sekunden nach dem man den letzten LP verloren hat NEU!
		// }
		lives --;


		if (lives <= 0) {
			if (health <= 0) {
				Invoke ("RestartLevel", 1);

				canvasReference.gameObject.SetActive (false);
				UpdateView ();
				player.enabled = false;
			}
		} else {
			if (health <= 0) {
				player.transform.position = player.currentCheckpoint.transform.position;
				health = startHealth;
			}

		}
	}
	
	
	
	void StartGame()
	{
		Application.LoadLevel (0);
	}
	
	void RestartLevel(){
		//healthGUI.gameObject.SetActive(true);
		Invoke("canvasReference.gameObject.SetActive (true)", 1);
		health = startHealth;
		//anim.SetBool("Dying", false );
		//Level neu generieren und Spieler zurücksetzen
		player.enabled = true;
		
		if (!player.isLookingRight) {
			player.Flip ();
		}
		StartGame ();
		
	}
	
	void Damaging(){
		//animation: anim.SetTrigger("Damage");

		UpdateView ();
		}


		void UpdateView() {
		Color textureColor = nearDeath.color;
		if (health / startHealth < 0.5) {
			textureColor.a = Mathf.Clamp(Mathf.Abs (Mathf.Sin (Time.time * 2.0f)),0.6f - (health / startHealth), 1f - (health / startHealth));
		} else {
			textureColor.a = 0;
		}
		Debug.Log ("" + textureColor.a + "");
		nearDeath.color = textureColor;
			
		healthGUI.fillAmount = 1 - (health / startHealth);
		currenthealth = health;



		}
	/*
	void OnDestroy() {
		PlayerPrefs.SetFloat ("Health", health);			//PlayerPrefs speichert Werte zwischen Game Sessions, also auch wenn man das Spiel schliesst und dann neu ladet
		// PlayerPrefs.SetInt("LifePoints", lifePoints);

	}
	*/

}

