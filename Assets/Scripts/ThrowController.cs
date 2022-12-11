using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowController : MonoBehaviour{

	public KeyCode shootKey = KeyCode.S;

	public GameObject snowballPrefab;

	public float snowballSpeed = 30f;

	public float rot = 0f;
	public float minRot = -30f, maxRot = 60f;
	public float rotSpeed = 20f;



	void Update(){

		rot += rotSpeed * Time.deltaTime;
		if(rot > maxRot){

			rot = maxRot;
			rotSpeed = -rotSpeed;

		}else if(rot < minRot){

			rot = minRot;
			rotSpeed = -rotSpeed;

		}

		transform.rotation = Quaternion.Euler(0, 0, rot);



		if(Input.GetKeyDown(shootKey)){

			GameObject snowball = Instantiate(snowballPrefab);

			snowball.transform.position = transform.position + transform.rotation * Vector2.right * 0.75f;
			snowball.GetComponent<Rigidbody2D>().velocity =
				transform.rotation * Vector2.right * snowballSpeed;

			Destroy(snowball, 5f);

			

		}

	}

}