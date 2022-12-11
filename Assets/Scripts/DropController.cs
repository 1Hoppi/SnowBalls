using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropController : MonoBehaviour{

	public float speed = 10f;

	void Update(){
		
		transform.position += Vector3.down * speed * Time.deltaTime;

	}

}