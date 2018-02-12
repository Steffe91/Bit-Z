using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject m_PLayerPrefab;
    public GameObject m_EnemyPrefab;
    public GameObject m_MainCamera;
    public GameObject m_Background;
    public GameObject m_HUD;
    public Text level;
    public Sprite[] HealthArr;
    public Image Life;

    private PauseMenu pauseMenu;
    private PlayerController player;

    // Use this for initialization
    void Start ()
    {
        
        //Instantiate(m_PLayerPrefab);
        Instantiate(m_Background);
        //Instantiate(m_MainCamera);
        //Instantiate(m_HUD);

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        var lvlName = SceneManager.GetActiveScene().name;
        level.text = lvlName;
    }
	
	// Update is called once per frame
	void Update ()
    {
        Life.sprite = HealthArr[player._currentHealth];
        Debug.Log(HealthArr[player._currentHealth]);
	}
}
