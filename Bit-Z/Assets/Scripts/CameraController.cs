using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraController : MonoBehaviour {

    public GameObject Player;
    public GameObject Enemy;
	public GameObject Boss;
    public Vector2 velocity;
    public float smoothTimex;
    public float smoothTimey;
    public bool bounds;
    public Vector3 minCameraPos;
    public Vector3 maxCameraPos;
    public float spawnTime = 3f;
    public GameObject stan;
    public Stanley _stan;
	private bool BossSpawned = false;

    private float offset;


	// tweak camera
	public CameraState CameraState;


    // Use this for initialization
    void Awake ()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        InvokeRepeating("SpawnEnemy", spawnTime, spawnTime);
        //offset = transform.position - Player.transform.position;
    }

	void Start() {
		CameraState = CameraState.Stationary;
	}

	void LateUpdate()
	{
		float offset = Camera.main.orthographicSize * Camera.main.aspect / 2;
		Vector3 PlayerPos = Camera.main.WorldToViewportPoint(Player.transform.position);

		if (PlayerPos.x < 0.25f || PlayerPos.x > 0.75f)
			CameraState = CameraState.Following;

		if (CameraState == CameraState.Following && PlayerState.Instance.Horizontal == Horizontal.Idle)
			CameraState = CameraState.Recentering;
		else if (CameraState == CameraState.Following)
			transform.position = new Vector3(Player.transform.position.x - offset * (int)PlayerState.Instance.DirectionFacing, transform.position.y, transform.position.z);

		if (CameraState == CameraState.Recentering)
		{
			float x = Mathf.Lerp(transform.position.x, Player.transform.position.x, 0.02f * Time.deltaTime * 60);
			transform.position = new Vector3(x, transform.position.y, transform.position.z);

			if (Math.Round(PlayerPos.x, 1) == 0.5f)
				CameraState = CameraState.Stationary;
		}
	}


    void SpawnEnemy()
    {
		offset = 20.2f;
		
		if (Time.timeScale != 0 && EnemyController_Test.Counter < 20)
        {
			if (!PauseMenu.isPaused) {
				Instantiate(Enemy, new Vector3(UnityEngine.Random.Range(transform.position.x + offset, maxCameraPos.x), 0, 0), Quaternion.identity);
			}
        }

		if (EnemyController_Test.Counter >= 20 && !BossSpawned) 
		{
			Debug.Log ("Spawn Stanley please!");
			SpawnBoss ();
		}

    } 

	void SpawnBoss()
	{
		Debug.Log("STANLEY!");
        //Instantiate (Boss, new Vector3 (UnityEngine.Random.Range(transform.position.x + offset, maxCameraPos.x),0,0), Quaternion.identity);
        stan = (GameObject)Instantiate(Boss, new Vector3(UnityEngine.Random.Range(transform.position.x + offset, maxCameraPos.x), 0, 0), Quaternion.identity);
        Stanley _stan = stan.GetComponent<Stanley>();

        BossSpawned = true;
	}


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Physics2D.IgnoreLayerCollision(0, 10);
            //Physics2D.IgnoreCollision(GetComponent<Camera>().GetComponent<Collider2D>(), Enemy.GetComponent<Collider2D>(), true);
        }
    }
}

public enum CameraState {
	Stationary,
	Following,
	Recentering
}