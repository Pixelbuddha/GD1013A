using UnityEngine;
using System.Collections;

public class CharacterControl : MonoBehaviour {

	public float maxSpeed = 4;
	public float jumpForce = 5;
	public float gravity = 9.81f;
	public Vector3 moveDirection = Vector3.zero; //Eine Varibale vom Typ Vector3 (x,y,z) gesetzt auf (0,0,0)
	//[HideInInspector]//
	public bool isLookingRight = true;
	public bool isGrounded = true;
	CharacterController cc;


	////////////////////////////////////////////////////////////////////////////

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
		float velocity = Input.GetAxis ("Horizontal") * maxSpeed;
		moveDirection.x = velocity;

		if (!cc.isGrounded) {
			moveDirection.y -= gravity * Time.deltaTime;
			isGrounded=false;
		} else {
			isGrounded=true;
			moveDirection.y = 0; //theoretisch nicht notwendig, wichtig falls jump auskommentiet wird
			if (Input.GetAxis ("Vertical")>0.01f) {
				moveDirection.y = jumpForce ;
				Debug.Log("Jump:"+jumpForce);
			}
		}

		cc.Move (moveDirection * Time.deltaTime);
		// x = Ich bewege mich pro Frame um 1/FPS
		// y = Ich bewege mich pro Frame um gravity/Fps²


		if ((velocity > 0 && !isLookingRight) || (velocity < 0 && isLookingRight))
			Flip ();


	}

	public void Flip()
	{
		isLookingRight = !isLookingRight;
		Vector3 myScale = transform.localScale;
		myScale.x *= -1;
		transform.localScale = myScale;
	}



}
