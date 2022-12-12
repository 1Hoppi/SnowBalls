using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrower : MonoBehaviour{

	[Header("References")]
	[SerializeField]
	private PlayerAnimation animator;

	private Transform currentPlayer;
	[SerializeField]
	private Transform otherPlayer;
	[SerializeField]
	private GameObject snowballPrefab;

	[Header("Snowball settings")]
	[SerializeField]
	private List<GameObject> snowballs = new List<GameObject>();

	[SerializeField]
	private float
		snowballSpeed = 30f,
		throwCooldown = 1f;
	
	private int snowballCount = 3;

	[Header("Rotation settings")]
	[SerializeField]
	private float rotSpeed = 100f;
	[SerializeField]
	public float minRot = -30f, maxRot = 60f;

	private float rot = 0f;

	[Header("Controls")]
	[SerializeField]
	private KeyCode shootKey = KeyCode.S;
	[SerializeField]
	private KeyCode reloadKey = KeyCode.R;

	// booleans
	public bool isReloading { get; private set; } = false;
	public bool isThrowing { get; private set; } = false;
	public bool canThrow { get; private set; } = true;


	
	void Start(){

		currentPlayer = gameObject.transform.parent;

	}

	void Update(){

		if(isThrowing) return;

		RotateAim();
		if(Input.GetKeyDown(reloadKey)) StartCoroutine(Reload());

		if(isReloading) return;

		if(Input.GetKeyDown(shootKey)) StartCoroutine(Throw());

	}

	private void RotateAim(){

		// rotate the aim
		rot += rotSpeed * Time.deltaTime;
		if(rot != Mathf.Clamp(rot, minRot, maxRot)){

			rot = Mathf.Clamp(rot, minRot, maxRot);
			rotSpeed *= -1;

		}
		// apply the rotation
		transform.rotation = Quaternion.Euler(0, 0, (currentPlayer.position.x <= otherPlayer.position.x) ? rot : 180f - rot);

	}

	private IEnumerator Throw(){
		
		if(!canThrow || isReloading || snowballCount <= 0) yield break;

		canThrow = false;
		isThrowing = true;

		yield return new WaitForSeconds(0.25f);

		isThrowing = false;

		// create a new snowball, which will disappear in 5 seconds
		GameObject snowball = Instantiate(snowballPrefab);
		Destroy(snowball, 5f);

		// change snowball's position and velocity
		snowball.transform.position = transform.position + transform.rotation * Vector2.right * 0.75f;
		snowball.GetComponent<Rigidbody2D>().velocity = (transform.rotation * Vector2.right) * snowballSpeed;
		snowball.GetComponent<SnowballController>().fromPlayer1 = currentPlayer.gameObject.CompareTag("Player1");

		// change snowball counter
        snowballs[--snowballCount].SetActive(false);

		yield return new WaitForSeconds(throwCooldown);

		canThrow = true;
		
	}

	private IEnumerator Reload(){

		if(isReloading || snowballCount >= 3) yield break;

		isReloading = true;
		animator.Reload();

		yield return new WaitForSeconds(1f);

		isReloading = false;

        snowballs[snowballCount++].SetActive(true);

	}

}