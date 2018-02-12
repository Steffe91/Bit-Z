using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject m_PLayerPrefab;
    public GameObject m_EnemyPrefab;
    public GameObject m_MainCamera;
    public GameObject m_Background;

	// Use this for initialization
	void Start ()
    {
        Instantiate(m_PLayerPrefab);
        Instantiate(m_EnemyPrefab);
        Instantiate(m_Background);
        Instantiate(m_MainCamera);

	}
	
	// Update is called once per frame
	void Update ()
    {
		 if(!m_PLayerPrefab)
        {
            Destroy(this.gameObject);
        }
	}
}
