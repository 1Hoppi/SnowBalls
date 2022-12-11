using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowController : MonoBehaviour{
	
	public GameObject Sprite;
	private Animator animator;
	private new SpriteRenderer renderer;

	public Transform player1, player2;
	public GameObject snowballPrefab;
	
	public KeyCode shootKey = KeyCode.S;

	public bool canThrow = true;
	public float throwCooldown = 1f;

	public float snowballSpeed = 30f;

	public float rot = 0f;
	public float minRot = -30f, maxRot = 60f;
	public float rotSpeed = 100f;

	PlayerController pc;

	private void Start(){
		
		animator = Sprite.GetComponent<Animator>();
		renderer = Sprite.GetComponent<SpriteRenderer>();
		pc = gameObject.GetComponentInParent<PlayerController>();
	}

	void Update(){
		
		float realRot, realMinRot, realMaxRot, realRotSpeed;
		if(player1.position.x <= player2.position.x){

			realRot = rot;
			realMinRot = minRot;
			realMaxRot = maxRot;
			realRotSpeed = rotSpeed;
			renderer.flipX = false;

		}else{

			realRot = 180f - rot;
			realMinRot = 180f - maxRot;
			realMaxRot = 180f - minRot;
			realRotSpeed = -rotSpeed;
			renderer.flipX = true;

		}

		realRot += realRotSpeed * Time.deltaTime;
		if(realRot > realMaxRot){

			realRot = realMaxRot;
			rotSpeed *= -1;

		}else if(realRot < realMinRot){

			realRot = realMinRot;
			rotSpeed *= -1;

		}

		if(player1.position.x <= player2.position.x)
			rot = realRot;
		else
			rot = 180f - realRot;

		transform.rotation = Quaternion.Euler(0, 0, realRot);



		if(Input.GetKeyDown(shootKey) && pc.snowballsCount > 0)
			StartCoroutine(Throw());

	}

	private IEnumerator Throw(){

		if(!canThrow) yield break;

		canThrow = false;

		animator.SetBool("Throwing", true);

		yield return new WaitForSeconds(0.25f);

		GameObject snowball = Instantiate(snowballPrefab);

        pc.snowballs[pc.snowballsCount-1].SetActive(false);
        pc.snowballsCount--;

		Destroy(snowball, 5f);

		snowball.transform.position = transform.position + transform.rotation * Vector2.right * 0.75f;
		snowball.GetComponent<Rigidbody2D>().velocity =
			transform.rotation * Vector2.right * snowballSpeed;

		animator.SetBool("Throwing", false);

		yield return new WaitForSeconds(throwCooldown);

		canThrow = true;
		
	}

}