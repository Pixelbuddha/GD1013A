using UnityEngine;
using System.Collections;

public class CharacterControl : MonoBehaviour {

	public float maxSpeed = 20;
	public float jumpForce = 5;
	public float gravity = 9.81f;
	public Vector3 moveDirection = Vector3.zero; //Eine Varibale vom Typ Vector3 (x,y,z) gesetzt auf (0,0,0)
	//[HideInInspector]//
	public bool isLookingRight = true;
	public bool isGrounded = true;
	public bool whiteMask = false;
	CharacterController cc;


	////////////////////////////////////////////////////////////////////////////fgh

	void Start () 
	{	
		cc = GetComponent<CharacterController>();
	}

	///

	void Update () {
		Move ();

	}

	void Move()
	{
		float velocity = Input.GetAxis ("Horizontal");

		moveDirection.x += velocity;
		moveDirection.x = Mathf.Clamp (moveDirection.x, -maxSpeed, maxSpeed);
		cc.radius = 0.4f+Mathf.Sin (Time.time*10f) * 0.051f;
		//moveDirection.x *= 0.98f;

		if (!cc.isGrounded) {
			moveDirection.y -= gravity * Time.deltaTime;
			isGrounded=false;
		} else {
			if (Mathf.Abs(velocity) < 0.9)
				moveDirection.x *= 0.175f;

			isGrounded=true;
			moveDirection.y = 0; //theoretisch nicht notwendig, wichtig falls jump auskommentiet wird
			moveDirection.y =  -gravity * Time.deltaTime;
			if (Input.GetButtonDown ("Jump")) {
				moveDirection.y = jumpForce ;
				Debug.Log("Jump:"+jumpForce);

			}
			if (Input.GetButtonDown ("Fire3") && cc.height == 2.98f) {
				cc.height = 1.5f;
			} else {
			
				if (Input.GetButtonDown ("Fire3") && cc.height == 1.5f) {
					cc.height = 2.98f;

					}
			}

			if (Input.GetButtonDown ("Fire2") && whiteMask == false){
				maxSpeed = 15;
				whiteMask = true;

			} else {
				
				if (Input.GetButtonDown ("Fire2") && whiteMask == true) {
					maxSpeed = 7;
					whiteMask = false;
					
				}
			}
		}

		cc.Move (moveDirection * Time.deltaTime);
		// x = Ich bewege mich pro Frame um 1/FPS
		// y = Ich bewege mich pro Frame um gravity/Fps²


		if ((velocity > 0 && !isLookingRight) || (velocity < 0 && isLookingRight))
			Flip ();


	}


	void OnControllerColliderHit(ControllerColliderHit hit) {
		if (hit.collider.gameObject.layer != 9) return;
		//Basically if you have a vector v, which represents the object's velocity, and a normalized normal vector n,
		//which is perpendicular to the surface with which the object collides, then the new velocity v' is given by the equation:
		//v' = 2 * (v . n) * n - v;
		//Where '*' is the scalar multiplication operator, '.' is the dot product of two vectors, and '-' is the subtraction operator for two vectors. v is reflected off of the surface, and gives a reflection vector v' which is used as the new velocity of the object. 
		Vector2 n = new Vector2 (hit.normal.x, hit.normal.y);
		Vector2 v = new Vector2 (moveDirection.x, moveDirection.y);
		if (n.y > 0)
			return;
		float dot = Vector2.Dot (v, n);
		if (dot >= 0)
			return;
		Vector2 nv = v-(n*(dot*2f));
		Debug.Log(""+n+v+nv+" "+dot+" "+hit.gameObject.name+" "+hit.point);

		moveDirection = new Vector3 (nv.x, nv.y, 0) * 0.75f;

	}


	public void Flip()
	{
		isLookingRight = !isLookingRight;
		Vector3 myScale = transform.localScale;
		myScale.x *= -1;
		transform.localScale = myScale;
	}



}
