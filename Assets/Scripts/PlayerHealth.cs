using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour{
    
	private int health = 5;

    [SerializeField]
	private List<GameObject> HP = new List<GameObject>();


    public async void ChangeHP(int change){

		health += change;

		if (change < 0) HP[health].SetActive(false);
		else HP[health].SetActive(true);

        if (health <= 0) Die();

	}

	private void Die(){
	
		gameObject.SetActive(false);

        // TODO: better scene change

        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings);
        
	}

}