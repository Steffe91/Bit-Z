﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleFactory : MonoBehaviour {
	public GameObject Acid;
	public GameObject Sewer2;
	public GameObject Sewer;

	void Awake()
	{
		CleanSlate();
	}

	void Start()
	{
		if (transform.position.x != 0)
		{
			Vector3[,] possiblePositions = new Vector3[5, 3];

			float yPosition = -3.6f;
			float xPosition = -3.6f;

			int MaxRows = 5;
			int MaxColumns = 3;

			for (int i = 0; i < MaxRows; i++)
			{
				for (int n = 0; n < MaxColumns; n++)
				{
					possiblePositions[i, n] = new Vector3(transform.position.x + xPosition, yPosition, 1);
					xPosition += (xPosition == 3.6f) ? -7.2f : 3.6f;
				}
			}

			GameObject[] randomPlatforms = new GameObject[3] { Acid, Sewer2, Sewer };

			int PatternOrRandom = Random.Range(1, 7);
			CreateChaotic(possiblePositions, randomPlatforms);
		}
	}

	private void CreateChaotic(Vector3[,] possiblePositions, GameObject[] randomPlatforms)
	{
		int percentChance = 75;

		foreach(Vector3 actualPosition in possiblePositions)
		{
			int diceRoll = Random.Range(1, 100);

			if (diceRoll < percentChance)
			{
				GameObject createdPlatform = (GameObject)GameObject.Instantiate(randomPlatforms[Random.Range(0, 3)], actualPosition, transform.rotation);
				createdPlatform.transform.parent = this.gameObject.transform;
				percentChance -= 5;
			}
			percentChance -= 5;
		}
	}
		

	private void CleanSlate()
	{
		foreach (Transform child in transform)
			Destroy(child.gameObject);
	}
}
