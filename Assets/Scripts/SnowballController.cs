using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SnowballController : MonoBehaviour{

    private void OnTriggerEnter2D(Collider2D other){

        if(other.CompareTag("Environment")) Destroy(gameObject);
        if(other.CompareTag("Player1") || other.CompareTag("Player2")) SceneManager.LoadScene("Main");

    }

}