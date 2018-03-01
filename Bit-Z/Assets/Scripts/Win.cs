using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Win : MonoBehaviour {

	public bool hasWon = false;
	public GameObject GameWinCanvas;

	void Start() {
		hasWon = false;
        Debug.Log("hasWon: " + hasWon);
	}

	void Update()
	{
        Debug.Log("UpdateHasWon: " + hasWon);
		if(hasWon)
		{
            Debug.Log("GEWONNEN!");
			EnemyController_Test.Counter = 0;
			GameWinCanvas.SetActive(true);
			Time.timeScale = 0;
		}
		else
		{
			GameWinCanvas.SetActive(false);
			Time.timeScale = 1;
		}
	}

	public void BackToMenu()
	{
		SceneManager.LoadScene("MainMenu");
	}
}

