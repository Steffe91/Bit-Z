using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    public string menu;

    public bool isPaused;

    public GameObject pauseCanvas;
	private GameObject Player;

	// Use this for initialization
	
	// Update is called once per frame

	void Start() {
		Player = GameObject.FindGameObjectWithTag("Player");
	}

	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			isPaused = !isPaused;
		}

		if(isPaused)
        {
            pauseCanvas.SetActive(true);
            Time.timeScale = 0f;
			//Player.GetComponent<PlayerController> ().enabled = Player.GetComponentInChildren<CameraController> ().enabled = false;

        }
        else
        {
            pauseCanvas.SetActive(false);
            Time.timeScale = 1f;
			//Player.GetComponent<PlayerController> ().enabled = Player.GetComponentInChildren<CameraController> ().enabled = true;	
        }


	}

    public void Resume()
    {
        isPaused = false;
    }

    public void Quit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
