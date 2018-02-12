using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameOver : MonoBehaviour {

    public bool isDead = false;
    public GameObject gameOverCanvas;

    void Update()
    {
        if(isDead)
        {
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
