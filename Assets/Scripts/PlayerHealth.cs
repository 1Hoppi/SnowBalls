using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour{
    
	private int health = 5;

    [SerializeField]
	private GameObject healthMask;

    public void ChangeHP(int change){

		health += change;

		healthMask.transform.localPosition = Vector3.right * health * 0.5f;

		if(health <= 0) Die();

	}

	private void Die(){
	
		gameObject.SetActive(false);

        // TODO: better scene change

        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings);
        
	}

}