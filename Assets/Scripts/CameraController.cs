using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour{

	public float
		minSize = 3f,
		maxSize = 9f;

	private Camera cam;

	public Transform player1, player2;

	void Start(){

		cam = GetComponent<Camera>();

	}

	void Update(){
		
		float newSize = Mathf.Clamp(
			Mathf.Max(
				Mathf.Abs(player1.position.x - player2.position.x) / (1.7f * 16f / 9f) + 1f,
				Mathf.Abs(player1.position.y - player2.position.y) / (1.7f) + 2f
		), minSize, maxSize);
		
		cam.orthographicSize = newSize;

		transform.position = new Vector3(
			Mathf.Clamp((player1.position.x + player2.position.x) / 2f, -15 + newSize * 3.5f / 2f, 15 - newSize * 3.5f / 2f),
			Mathf.Max((player1.position.y + player2.position.y) / 2f, newSize - 5.25f),
		-10f);

	}
	
}