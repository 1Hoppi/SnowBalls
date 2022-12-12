using UnityEngine;

public class PlayerAnimation : MonoBehaviour{

    [Header("References")]
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private PlayerMovement movement;
    [SerializeField]
    private PlayerThrower thrower;

    void Update(){

        animator.SetFloat("Speed", Mathf.Abs(movement.rb.velocity.x));
        animator.SetBool("Jumping", !movement.isGrounded);
        animator.SetBool("Throwing", thrower.isThrowing);
		animator.SetBool("Reloading", thrower.isReloading);

    }

    public void Reload(){

        animator.SetTrigger("Reload");

    }

}