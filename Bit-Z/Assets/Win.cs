using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Win : MonoBehaviour {

	public bool hasWon;
	public GameObject GameWinCanvas;

	void Start() {
		hasWon = false;
	}

	void Update()
	{
		if(hasWon)
		{
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

