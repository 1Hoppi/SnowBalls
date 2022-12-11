using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour{
	
	public Animator animator;
	private Rigidbody2D rb;

	public float speed = 10f;
	public float jumpForce = 5f;
	public bool isGrounded = false,
		isReloading = false;

	private int collisionsCount = 0;

	private int hp = 5;



	public KeyCode
		upKey = KeyCode.W,
		rightKey = KeyCode.D,
		leftKey = KeyCode.A,
		reloadKey = KeyCode.R;



	public void Start(){

		if(rb == null)
			rb = GetComponent<Rigidbody2D>();

	}

	public void Update(){
		
		if(Input.GetKeyDown(reloadKey) && isGrounded)
			StartCoroutine(Reload());

		if(!isReloading){

			float input = (Input.GetKey(rightKey) ? 1 : 0) - (Input.GetKey(leftKey) ? 1 : 0);
			rb.velocity = new Vector2(input * speed, rb.velocity.y);



			if(Input.GetKeyDown(upKey) && isGrounded){

				rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
				isGrounded = false;

			}

		}



		animator.SetBool("Jumping", !isGrounded);
		animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
		
	}

	private void OnCollisionEnter2D(Collision2D collision){

		if(collision.gameObject.CompareTag("Environment")) collisionsCount++;
        if(collisionsCount > 0) isGrounded = true;

    }
    private void OnCollisionExit2D(Collision2D collision){

        if(collision.gameObject.CompareTag("Environment")) collisionsCount--;
        if(collisionsCount == 0) isGrounded = false;

    }

	private IEnumerator Reload(){
		
		isReloading = true;
		rb.velocity = Vector2.zero;

		animator.SetBool("Reloading", true);
		animator.SetTrigger("Reload");

		yield return new WaitForSeconds(1f);
		isReloading = false;
		
		animator.SetBool("Reloading", false);

	}

	public IEnumerator ChangeHP(int change){

		hp += change;
		Debug.Log(hp);
		if(hp <= 0){

			gameObject.SetActive(false);
			yield return new WaitForSeconds(3f);

			int curr = int.Parse(SceneManager.GetActiveScene().name[5].ToString());
			SceneManager.LoadScene($"Main{(curr + 1) % 3}");

		}

	}

}