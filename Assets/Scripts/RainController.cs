using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainController : MonoBehaviour{
	
	public SpriteRenderer sprite;
	public GameObject dropPrefab;

	public float
		warningDuration = 3f,
		rainDuration = 5f,
		spawnCooldown = 0.2f;

	public int blinkNumber = 3;

	public float maxAlpha = 0.4f;



	void Start(){

		if(sprite == null)
			sprite = GetComponent<SpriteRenderer>();

		StartCoroutine(BlinkSpawn());

	}

	private IEnumerator BlinkSpawn(){

		Color col = sprite.material.color;
		float blinkDuration = warningDuration / (float) blinkNumber;

		for(float t = 0; t < warningDuration; t += 0.02f){

			sprite.material.color = new Color(col.r, col.g, col.b, Mathf.Abs((t + blinkDuration / 2f) % blinkDuration - blinkDuration / 2f) * 2f / blinkDuration * maxAlpha);
			yield return new WaitForSeconds(0.02f);

		}

		for(float t = 0; t < rainDuration; t += spawnCooldown){

			GameObject drop = Instantiate(dropPrefab);
			drop.transform.position = new Vector2(Random.Range(-0.5f, 0.5f) * transform.localScale.x, transform.localScale.y / 2f);
			Destroy(drop, 1f);

			yield return new WaitForSeconds(spawnCooldown);

		}

	}

}