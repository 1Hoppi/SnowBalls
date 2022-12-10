using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{

	public Rigidbody2D rb;

	public float speed = 10f;
	public float jumpForce = 5f;
	public bool isGrounded = true;

	private Vector2 oldVelocity = Vector2.zero;



	public KeyCode
		upKey = KeyCode.W,
		rightKey = KeyCode.D,
		leftKey = KeyCode.A;

	public void Start(){

		if(rb == null)
			rb = GetComponent<Rigidbody2D>();

	}

	public void Update(){

		float input = (Input.GetKey(rightKey) ? 1 : 0) - (Input.GetKey(leftKey) ? 1 : 0);
		rb.velocity = new Vector2(input * speed * Time.deltaTime, rb.velocity.y);



		CheckGrounded();

		if(Input.GetKeyDown(upKey) && isGrounded){

			rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
			isGrounded = false;

		}
		
	}

	private void CheckGrounded(){

		if(rb.velocity.y == 0 && oldVelocity.y != 0) isGrounded = true;
		else if(oldVelocity.y == 0 && rb.velocity.y != 0) isGrounded = false;

		oldVelocity = rb.velocity;

	}

}