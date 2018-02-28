using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class PauseMenu : MonoBehaviour {

    public string menu;

    public static bool isPaused;

    public GameObject pauseCanvas;
	public GameObject Player;
    private GameObject Camera;
    private GameObject[] Enemy;


    
    // Use this for initialization

    // Update is called once per frame

    void Start() {
		Player = GameObject.FindGameObjectWithTag("Player");
        Camera = GameObject.FindGameObjectWithTag("MainCamera");
        Enemy = GameObject.FindGameObjectsWithTag("Enemy");

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
            Time.timeScale = 0;

            Player.GetComponent<PlayerController>().enabled = false;
            Player.GetComponent<Shooting>().enabled = false;
            Camera.GetComponent<CameraController>().enabled = false;
            
            for(int i = 0; i < Enemy.Length; i++)
            {
                Debug.Log("in schleife");
                Enemy[i].GetComponent<EnemyController_Test>().enabled = true;
            }

        }
        else
        {
            pauseCanvas.SetActive(false);
            Time.timeScale = 1f;

            Player.GetComponent<PlayerController>().enabled = true;
            Player.GetComponent<Shooting>().enabled = true;
            Camera.GetComponent<CameraController>().enabled = true;

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
