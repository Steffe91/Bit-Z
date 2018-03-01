using UnityEngine;
using System.Collections;

#region - Script Synopsis
/*
This script is attached to the main floor GameObject and instantiates, whenever a new floor GameObject is created, random
Platform GameObject prefabs found in the Prefabs folder. An example of basic procedural generation, the platforms are created
with either a more patterend or chaotic approach, depending on which method is run, either CreateChaotic() or CreatePatterened().
*/
#endregion

public class PlatformFactory : MonoBehaviour
{
    //References to the three different types of platform prefabs, set in the inspector of the 1_floor GameObject
    public GameObject StoneyPlatform;
    public GameObject GrassyPlatform;
    public GameObject RubberPlatform;

    void Awake()
    {
        CleanSlate();
    }

	void Start()
	{
        if (transform.position.x != 0)
        {
            Vector3[,] possiblePositions = new Vector3[5, 3];

            float yPosition = -2f;
            float xPosition = -6.6f;

            int MaxRows = 5;
            int MaxColumns = 3;

            for (int i = 0; i < MaxRows; i++)
            {
                for (int n = 0; n < MaxColumns; n++)
                {
                    possiblePositions[i, n] = new Vector3(transform.position.x + xPosition, yPosition, 1);
                    xPosition += (xPosition == 3.6f) ? -7.2f : 3.6f;
                }
                yPosition += 1.5f;
            }

            //Platform Array Initializer
            GameObject[] randomPlatforms = new GameObject[3] { StoneyPlatform, GrassyPlatform, RubberPlatform };

            int PatternOrRandom = Random.Range(1, 4);
			CreateChaotic(possiblePositions, randomPlatforms);
        }
	}

    //Method of instatiation a more randomized pattern of platforms
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
		
    //Ensures that all platforms are wiped clean when copied along with a new floor tile copy
    private void CleanSlate()
    {
        foreach (Transform child in transform)
            Destroy(child.gameObject);
    }
}