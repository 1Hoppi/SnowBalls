using UnityEngine;

public class PlayerAnimation : MonoBehaviour{

	[Header("References")]
	[SerializeField]
	private GameObject sprite;
	private Animator animator;
	private SpriteRenderer spriteRenderer;
	
	[SerializeField]
	private PlayerMovement movement;
	[SerializeField]
	private PlayerThrower thrower;

	[SerializeField]
	private Transform otherPlayer;



	void Start(){

		animator = sprite.GetComponent<Animator>();
		spriteRenderer = sprite.GetComponent<SpriteRenderer>();

	}

	void Update(){

		animator.SetFloat("Speed", Mathf.Abs(movement.rb.velocity.x));
		animator.SetBool("Jumping", !movement.isGrounded);
		animator.SetBool("Throwing", thrower.isThrowing);
		animator.SetBool("Reloading", thrower.isReloading);

		spriteRenderer.flipX = gameObject.transform.position.x > otherPlayer.position.x;

	}

	public void Reload(){

		animator.SetTrigger("Reload");

	}

}