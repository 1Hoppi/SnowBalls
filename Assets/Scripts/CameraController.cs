using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour{

	public float
		minSize = 3f,
		maxSize = 8.5f;

	private Camera cam;

	public Transform player1, player2;

	void Start(){

		cam = GetComponent<Camera>();

	}

	void Update(){

		float newSize = Mathf.Clamp(Mathf.Abs(player1.position.x - player2.position.x) / 3f, minSize, maxSize);
		cam.orthographicSize = newSize;

		transform.position = new Vector3((player1.position.x + player2.position.x) / 2f, newSize - 5, -10f);

	}
	
}