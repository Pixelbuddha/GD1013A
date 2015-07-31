using UnityEngine;
using System.Collections;


///	SHIT TO DO
/// hitbox??
/// übertragen auf GUI - done
/// GUI Masken
/// Crushing Walls
/// Steuerung muss auch mit Controller gehen
/// 
/// Checkpoint sachen im Checkpoint Speichern, keine Playerprefs
/// Leben, Position etc im Checkpoint speichern, und bei tot von dort aufrufen
/// 


public class CharacterControl : MonoBehaviour {
	
	public const int layerNumber = 9;
	public float maxHitHeight = 2.98f;
	public float minHitHeight = 1.5f;
	
	public float maxSpeed = 20;
	public float jumpForce = 10;
	public float gravity = 9.81f;
	public Vector3 moveDirection = Vector3.zero; //Eine Varibale vom Typ Vector3 (x,y,z) gesetzt auf (0,0,0)
	
	public bool isLookingRight = true;
	public bool isGroundedDebug = true;

	public GameObject modelGO;

	private Animator anim;
	
	//private float angle;
	//private Vector2 moveDirection2D;
	
	public MaskLight maskLight;
	
	
	CharacterController cc;


	public HealthController hc;
	
	//
	////////////////////////////////////////////////////////////////////////////
	//Masken
	
	enum MaskType{
		white = 0,
		orange = 1,
		red = 2,
		yellow = 3
	};
	
	public int activeMask = 0;
	public bool [] masksFound;
	
	void Start () 
	{	
		if (modelGO == null)
			modelGO = transform.FindChild (transform.name + "_model").gameObject;
		anim = modelGO.GetComponent<Animator> ();
		cc = GetComponent<CharacterController>();
		if(cc==null) {Debug.LogError("Missing CharacterController!!!!");enabled=false;return;}
		hc = GetComponent<HealthController> ();
		if(hc==null) {Debug.LogError("Missing HealthController!!!!");enabled=false;return;}
		maskLight = GetComponentInChildren<MaskLight> ();
		if(maskLight==null) {Debug.LogError("Missing MaskLight!!!!");enabled=false;return;}


		masksFound = new bool[] { true, true, true, true}; 	// initiieren des Masken Arrays
		maskLight.myCharacter = this;
		hc.FU();
	}
	
	
	void Update () {
		Move ();
		MaskController ();

		
	}
	
	void Move()
	{	

		

		float velocity = Input.GetAxis ("Horizontal");
		
		//		ControllerColliderHit schraeg_hit = new ControllerColliderHit();
		//		Vector2 schraeg = new Vector2 (schraeg_hit.normal.x, schraeg_hit.normal.y);
		moveDirection.x += velocity;											// Links Rechts Input
		//moveDirection.y += velocity ;
		moveDirection.x = Mathf.Clamp (moveDirection.x, -maxSpeed, maxSpeed);	// Begrenzung min und max Speed (Clamp)
		cc.radius = 0.4f + Mathf.Sin (Time.time * 10f) * 0.051f;					// jitter aka Herumskalieren um nicht in Ecken hängenzubleiben (Der Entzitterer)
		moveDirection.x *= 0.98f;												// Luftreibung
		
		if (!cc.isGrounded) {													// Wenn in Luft
			moveDirection.y -= gravity * Time.deltaTime;						// mach Gravitiy an (Beschleunigend Linear)
			isGroundedDebug = false;											// Anzeige um zu schauen ob isGrounded Aktiv ist
			anim.SetBool ("IsGroundedAnim", isGroundedDebug);
		} else {																// Wenn nicht in Luft
			if (Mathf.Abs (velocity) < 0.9)										// Für Keyboardsteuerung, Sobald Horizontal Wert unter 0.9 sinkt (kurz nach loslassender Taste), Bremse
				moveDirection.x *= 0.175f;										// Wert für Bremsstärke
			
			isGroundedDebug = true;												//Debug Anzeige wieder
			anim.SetBool ("IsGroundedAnim", isGroundedDebug);
			moveDirection.y = 0; 												//theoretisch nicht notwendig, wichtig falls jump auskommentiet wird
			moveDirection.y = -gravity * Time.deltaTime;						// konstante nicht beschleunigende Gravity
			if (Input.GetButtonDown ("Jump")) {									// Steuerungseingabe (Keyboard Spacebar, Controller das dementsprechende)
				moveDirection.y = jumpForce;									// hcchspringen
				//Debug.Log("Jump:"+jumpForce);									// Log Anzeige für die Kraft des Sprungs
				
			}
			if (activeMask != (int)MaskType.orange) {	
				if (Input.GetButtonDown ("Fire3") && cc.height == maxHitHeight) {			// Crouch Funktion
					cc.height = minHitHeight;												// Mach Hitbox Kleiner
				} else {
					
					if (Input.GetButtonDown ("Fire3") && cc.height == minHitHeight) {
						cc.height = maxHitHeight;											// Mach Hitbox Größer
						
					}
				}
				
			}
		}
		
		//moveDirection2D.x = moveDirection.x;
		//moveDirection2D.y = moveDirection.y;
		//angle = Vector2.Dot(norm, moveDirection2D) / Vector2.Dot(norm, moveDirection2D);
		//Debug.Log("" + angle + "");
		cc.Move (moveDirection * Time.deltaTime);								// Bewegen in die Oben ausgerechnete Richtung
		Debug.Log (moveDirection * Time.deltaTime);
		anim.SetFloat ("Speed", Mathf.Abs (velocity));
		if (moveDirection.y == 0) {
			anim.SetFloat ("VerticalSpeed", 0);
		} else {
			if (moveDirection.y > 0) {
				anim.SetFloat ("VerticalSpeed", 1);
			} else {
				anim.SetFloat ("VerticalSpeed", -1);
			}
		}
	
		
		if ((velocity > 0 && !isLookingRight) || (velocity < 0 && isLookingRight))	// wenn wir uns nach links bewegen und nach rechts schauen oder umgekehrt
			Flip ();																// dann dreh dich um
		
		//Debug.Log ("Velocity is:" + velocity + "");
	}
	
	
	void OnControllerColliderHit(ControllerColliderHit hit) {						// funktion fürs Abprallen von Wänden und Decken beim Gegenspringen und Gegenlaufen
		if (hit.collider.gameObject.layer != layerNumber)
			return;
		
		
		//Basically if you have a vector v, which represents the object's velocity, and a normalized normal vector n,
		//which is perpendicular to the surface with which the object collides, then the new velocity v' is given by the equation:
		//v' = 2 * (v . n) * n - v;
		//Where '*' is the scalar multiplication operator, '.' is the dot product of two vectors, and '-' is the subtraction operator for two vectors. v is reflected off of the surface, and gives a reflection vector v' which is used as the new velocity of the object. 
		
		
		Vector2 n = new Vector2 (hit.normal.x, hit.normal.y);						// Krasse
		Vector2 v = new Vector2 (moveDirection.x, moveDirection.y);					// Mathematische
		//if (n.y > 3f) {
		if (n.y >= -0.1)																// Berechnungen
			return;																	// um
		float dot = Vector2.Dot (v, n);												// cool
		if (dot >= 0)																// Abprallen
			return;																	// zu
		Vector2 nv = v - (n * (dot * 2f));												// können
		//Debug.Log ("" + n + v + nv + " " + dot + " " + hit.gameObject.name + " " + hit.point);			// :)
		
		moveDirection = new Vector3 (nv.x, nv.y, 0) * 0.75f;						// ausführen des Abprallens
		
		//}
	}
	
	
	public void Flip()																// die "ich Dreh mich um Funktion"
	{
		isLookingRight = !isLookingRight;											//Eigentlich selbsterklärend
		Vector3 myScale = transform.localScale;
		myScale.x *= -1;
		transform.localScale = myScale;
	}
	
	
	
	
	///
	/// Masken//////////////////////
	/// 
	
	void NextMask(){
		
		
		do {											// mache
			// nächste Maske Auswählen
			if (activeMask == masksFound.Length - 1) {		// Wenn am Ende angekommen
				activeMask = 0;							// Mach am Anfang weiter
			}
			else {
				activeMask ++;
			}
		} while (masksFound[activeMask] = false);		// Während die aktuelle Maske noch nicht gefunden mache
		
		MaskUpdate ();									// Rufe die Funktionen der Aktuellen Maske auf
	}
	
	
	void PreviousMask(){								// Das Gleiche nur in die Andere Richtung
		
		activeMask --;
		do {
			
			if (activeMask < 0) {
				//Debug.Log (masksFound.Length);
				activeMask = masksFound.Length -1;		// Wenn am Anfang angekommen mache am Ende weiter
			}
		} while (masksFound[activeMask] = false);
		
		MaskUpdate ();									// bump
	}
	
	
	
	void MaskUpdate(){									// Was die Einzelnen Masken können, und dass die Funktion
		
		jumpForce = 10;									// der Momentan ausgewählten Maske
		maxSpeed = 7;
		//healthreg = - X								// auch ausgeführt wird.
		
		if (activeMask == (int)MaskType.white) {				// Weisse Maske (Im Array Platz 0)
			//healthreg = +Y;
			maskLight.TurnLightOff();
			hc.healthRegSpeed = 0.01f;
			hc.healthReg = 5;
							
		}

		
		if (activeMask == (int)MaskType.orange) {			// Orangene Maske (Im Array Platz 1)
			jumpForce = 13;
			maxSpeed = 10;
			maskLight.TurnLightOff();
			hc.healthRegSpeed = 0.01f;
			hc.healthReg = -1;
			
		}
		
		if (cc.height == minHitHeight) 
			return;
		
		if (activeMask == (int)MaskType.red) {				// Rote Maske (Im Array Platz 2)
			jumpForce = 20;
			maxSpeed = 5;
			maskLight.TurnLightOn();
			hc.healthRegSpeed = 0.01f;
			hc.healthReg = -2;
		}
		
		if (activeMask == (int)MaskType.yellow) {			// Gelbe Maske ( (Im Array Platz 3)
			jumpForce = 5;
			maxSpeed = 50;
			maskLight.TurnLightOff();
			hc.healthRegSpeed = 0.01f;
			hc.healthReg = -4;
		}
		// had to cast enum to (int) bc Unity couldnt match the int activeMask to the enum MaskType on its own //
	}
	
	
	void MaskController(){								// Die Funktion die Masken zu wechseln
		
		
		
		if (Input.GetKeyDown(KeyCode.E) ){					// Nächste Maske
			NextMask();
		}
		
		
		if (Input.GetKeyDown(KeyCode.Q) ){					// Vorherige Maske
			PreviousMask();
		}
		//Debug.Log("" + activeMask);
		
	}
	
	
}
