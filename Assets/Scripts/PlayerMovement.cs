using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour{
	
	[Header("References")]
	[SerializeField]
	private PlayerThrower thrower;
	public Rigidbody2D rb { get; private set; } 

	[Header("Settings")]
	[SerializeField]
	private float speed = 10f;
	[SerializeField]
	private float jumpForce = 5f;

	[Header("Controls")]
	[SerializeField]
	private bool canMove = true;
	[SerializeField]
	private KeyCode
		upKey = KeyCode.W,
		leftKey = KeyCode.A,
		rightKey = KeyCode.D;
	
	// booleans
	public bool isGrounded { get; private set; } = false;



	public void Start(){

		if(rb == null)
			rb = GetComponent<Rigidbody2D>();

	}

	public void Update(){

		// if reloading, stay still
		if(thrower.isReloading){
			
			rb.velocity = Vector3.zero;
			isGrounded = true;

			return;

		}



		if(!canMove) return;
		Move();



		// check if grounded and jump
		GroundCheck();
		if(Input.GetKeyDown(upKey) && isGrounded) Jump();

	}

	private void Move(){

		float input = (Input.GetKey(rightKey) ? 1 : 0) - (Input.GetKey(leftKey) ? 1 : 0);
		rb.velocity = new Vector2(input * speed, rb.velocity.y);

	}

	private void Jump(){

		rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
		isGrounded = false;

	}

	private void GroundCheck(){

		// TODO: change later
		if (rb.velocity.y == 0) isGrounded = true;
		else isGrounded = false;

	}

}