using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour{

	[SerializeField]
	private float
		minSize = 3f,
		maxSize = 9f;

	[SerializeField]
	private Vector2 size = new Vector2(30f, 50f), centerOffset = new Vector2(), padding = new Vector2(1f, 2f);

	[SerializeField]
	private Transform player1, player2;

	private Camera cam;

	void Start(){

		cam = GetComponent<Camera>();

	}

	void Update(){
		
		float magicConst = 1.75f, // used to convert camera size to units
		aspectRatio = 16f / 9f;

		// calculate new size of the camera based on players' positions
		float newSize = Mathf.Clamp(
			Mathf.Max(
				Mathf.Abs(player1.position.x - player2.position.x) / (magicConst * aspectRatio) + padding.x,
				Mathf.Abs(player1.position.y - player2.position.y) / magicConst + padding.y
			),
			minSize,
			maxSize
		);

		// apply the new size
		cam.orthographicSize = newSize;

		float
			leftEdge = -size.x / 2f  +  newSize * magicConst,
			bottomEdge = (-size.y / 2f)  +  newSize * magicConst / aspectRatio;

		// calculate camera's new position based on players' positions and edges of the map
		float
		newX = Mathf.Clamp(
			(player1.position.x + player2.position.x) / 2f, // mean of players' x coords
			+leftEdge + centerOffset.x, // left edge + half of camera's width
			-leftEdge + centerOffset.x  // right edge - half of camera's width
		),
		newY = Mathf.Clamp(
			(player1.position.y + player2.position.y) / 2f, // mean of players' y coords
			+bottomEdge + centerOffset.y, // bottom edge + half of camera's height
			-bottomEdge + centerOffset.y  // top edge - half of camera's height
		);

		if(newX < leftEdge + centerOffset.x || newX > -leftEdge + centerOffset.x) newX = centerOffset.x;
		if(newY < bottomEdge + centerOffset.y || newY > -bottomEdge + centerOffset.y) newY = centerOffset.y;

		// apply new position
		transform.position = new Vector3(newX, newY, -10f);

	}
	
}