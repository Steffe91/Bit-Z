using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameOver : MonoBehaviour {

    public bool isDead;
    public GameObject gameOverCanvas;

	void Start() {
		isDead = false;
	}

    void Update()
    {
        if(isDead)
        {
			EnemyController_Test.Counter = 0;
            gameOverCanvas.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            gameOverCanvas.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void BackToMenu()
    {
            SceneManager.LoadScene("MainMenu");
    }
}
