using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SnowballController : MonoBehaviour{

    public bool fromPlayer1;

    private void OnTriggerEnter2D(Collider2D other){

        if(
            other.CompareTag("Environment") ||
            other.CompareTag("Walls")
        ) Destroy(gameObject);

		else if(
            (other.CompareTag("Player1") && !fromPlayer1) || 
            (other.CompareTag("Player2") &&  fromPlayer1)
        ){

            Destroy(gameObject);
            other.gameObject.GetComponent<PlayerHealth>().ChangeHP(-1);

        }

    }

}