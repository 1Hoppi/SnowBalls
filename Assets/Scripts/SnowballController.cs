using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SnowballController : MonoBehaviour{

    public bool byPlayer1;

    private void OnTriggerEnter2D(Collider2D other){

        if(
            other.CompareTag("Environment") ||
            other.CompareTag("Walls")
        ) Destroy(gameObject);

		else if(
            (other.CompareTag("Player1") && !byPlayer1) || 
            (other.CompareTag("Player2") &&  byPlayer1)
        ){

            Destroy(gameObject);
            other.gameObject.GetComponent<PlayerHealth>().ChangeHP(-1);

        }

    }

}