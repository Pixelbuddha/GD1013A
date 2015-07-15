using UnityEngine;
using System.Collections;

public class HealthControllerNew : MonoBehaviour {
    const int health = 5;     //private as default Modifier
    const int life = 3;
    int currentHealth;
    bool isDead;
    int currentLife;

    CharacterControl player = new CharacterControl();

	// Use this for initialization
	void Start () {
        this.player = GetComponent<CharacterControl>();
        setCurrentHealth(health);
        setLife(life);
	}

	// Update is called once per frame
	void Update () {
	}

    void setDamage(int damage)  //Evtl. Exceptionhandling einbauen (try/catch Block)
    {  
        setCurrentHealth(this.currentHealth - damage);
        if (!isDead && this.currentHealth <= 0)
        {
            Dying();     
        }
    }

    void Dying()
    {
        /**
        *   CALL ANIMATION SCRIPTS 
        */
        setCurrentLife(getCurrentLife() - 1);
        if (getCurrentLife() <= 0)
        {
            //GameOver
        }
        player.enabled = false;
        Invoke("RestartLevel", 1);
    }

    void StartGame()
    {
        Application.LoadLevel(0);
        if (!this.player.isLookingRight)
        {
            this.player.Flip();
        }
    }

    void RestartLevel()
    {
        /**
        *   CALL ANIMATION SCRIPTS 
        */
        this.currentHealth = this.health;
        this.player.enabled = true;
        StartGame();
    }

    int getCurrentHealth()
    {
        return this.currentHealth;
    }

    int getCurrentLife()
    {
        return this.currentLife;
    }

    void setCurrentHealth(int currentHealth)
    {
        this.currentHealth = currentHealth;
    }

    void setCurrentLife(int currentLife)
    {
        this.currentLife = life;
    }
}
