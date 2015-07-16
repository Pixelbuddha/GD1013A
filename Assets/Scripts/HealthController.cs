using UnityEngine;
using System.Collections;

public class HealthController : MonoBehaviour {
	
	public int startHealth = 5;
	public float currenthealth;
	//public int statLifePoints = 3;
	private float health = 5;
	private bool isDead = false;
	private bool isDamageable = true;
	//private int lifePoints = 3;
	
	public CharacterControl player;
	
	void Start () {
		//anim = GetComponent<Animator>();
		player = GetComponent<CharacterControl> ();
		currenthealth = health;
		//Der Level-Index muss dem Spiel entsprechend angepasst werden, wenn es z.B. eine begrüßungsszene gibt (oder ein Hauptmenü)
		if (Application.loadedLevel == 0) {
			health = startHealth;
			//lifePoints = startLifePoints;
		} else {
			health = PlayerPrefs.GetFloat("Health");
			//lifePoints = PlayerPrefs.GetInt("LifePoints");
		}



		health = PlayerPrefs.GetFloat ("Health");
		//lifePoints = PlayerPrefs.GetInt ("LifePoints");

	}
	
	// Update is called once per frame


	void ApplyDamage(int damage) {

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
		player.enabled = false;
		if (health <= 0) {
			Invoke("RestartLevel",1);
		}
	}
	
	
	
	void StartGame()
	{
		Application.LoadLevel (0);
	}
	
	void RestartLevel(){
		health = startHealth;
		//anim.SetBool("Dying", false );
		//Level neu generieren und Spieler zurücksetzen
		player.enabled = true;
		
		if (!player.isLookingRight) {
			player.Flip ();
		}
		
	}
	
	void Damaging(){
		//animation: anim.SetTrigger("Damage");


	}

	void OnDestroy() {
		PlayerPrefs.SetFloat ("Health", health);			//PlayerPrefs speichert Werte zwischen Game Sessions, also auch wenn man das Spiel schliesst und dann neu ladet
		// PlayerPrefs.SetInt("LifePoints", lifePoints);

	}

}

