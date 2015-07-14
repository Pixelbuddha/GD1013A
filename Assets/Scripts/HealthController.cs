using UnityEngine;
using System.Collections;

public class HealthController : MonoBehaviour {
	
	public int startHealth = 5;
	private float health = 5;
	private bool isDead = false;
	//private float lifePoints = 3;
	
	public CharacterControl playerController;
	
	void Start () {
		
		playerController = GetComponent<CharacterControl> ();
	}
	
	// Update is called once per frame
	void ApplyDamage(int damage) {
		health -= damage;
		
		health = Mathf.Max (0, health);
		
		if (!isDead) {
			if (health == 0) {
				isDead = true;
				Dying ();
			} else {
				Damaging ();
			}
		}
	}
	
	void Dying(){
		// anim.SetBool("Dying", true);
		// lifepoints --;
		//if (lifepoints <=0){
		//Invoke("StartGame",3);  // Startet das Spiel 3 Sekunden nach dem man den letzten LP verloren hat NEU!
		// }
		playerController.enabled = false;
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
		playerController.enabled = true;
		
		if (!playerController.isLookingRight) {
			playerController.Flip ();
		}
		
	}
	
	void Damaging(){
	}
}

