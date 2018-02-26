using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraController : MonoBehaviour {

    public GameObject Player;
    public GameObject Enemy;
    public Vector2 velocity;
    public float smoothTimex;
    public float smoothTimey;
    public bool bounds;
    public Vector3 minCameraPos;
    public Vector3 maxCameraPos;
    public float spawnTime = 3f;
    //public float RunBack;

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
		Vector3 CheeseScreenPosition = Camera.main.WorldToViewportPoint(Player.transform.position);

		if (CheeseScreenPosition.x < 0.25f || CheeseScreenPosition.x > 0.75f)
			CameraState = CameraState.Following;

		if (CameraState == CameraState.Following && PlayerState.Instance.Horizontal == Horizontal.Idle)
			CameraState = CameraState.Recentering;
		else if (CameraState == CameraState.Following)
			transform.position = new Vector3(Player.transform.position.x - offset * (int)PlayerState.Instance.DirectionFacing, transform.position.y, transform.position.z);

		if (CameraState == CameraState.Recentering)
		{
			float x = Mathf.Lerp(transform.position.x, Player.transform.position.x, 0.02f * Time.deltaTime * 60);
			transform.position = new Vector3(x, transform.position.y, transform.position.z);

			if (Math.Round(CheeseScreenPosition.x, 1) == 0.5f)
				CameraState = CameraState.Stationary;
		}
	}
	
	// Update is called once per frame
	/*void Update ()
    {

        offset = GetComponent<Camera>().transform.position.x - Player.transform.position.x;

        //Debug.Log(offset);

        //transform.position = Player.transform.position + offset;

        float posX = Mathf.SmoothDamp(transform.position.x, Player.transform.position.x, ref velocity.x, smoothTimex);
        float posY = Mathf.SmoothDamp(transform.position.y, Player.transform.position.y, ref velocity.y, smoothTimey);

        transform.position = new Vector3(posX, posY, transform.position.z);

        if (bounds)
        {       
            if ((PlayerController.dirFacing == 2) && (minCameraPos.x >= -41.4f) )
            {
                
                minCameraPos.x = GetComponent<Camera>().transform.position.x;
            }

            if(minCameraPos.x < -41.4f)
            {
                minCameraPos.x = -41.4f;
            }
            //else if (minCameraPos.x >= maxCameraPos.x)
            //{
            //    minCameraPos.x = maxCameraPos.x;
            //}

            transform.position = new Vector3(Mathf.Clamp(transform.position.x + offset, minCameraPos.x, maxCameraPos.x),
                                 Mathf.Clamp(transform.position.y, minCameraPos.y, maxCameraPos.y),
                                 Mathf.Clamp(transform.position.z, minCameraPos.z, maxCameraPos.z));

            //Debug.Log("Offset: " + offset);
            //Debug.Log("Transform Pos: " + transform.position);
            //Debug.Log("Camera: " + GetComponent<Camera>().transform.position.x);
            //Debug.Log("minCamera: " + minCameraPos.x);
        }

    } */

    void SpawnEnemy()
    {
        if (Time.timeScale != 0)
        {
            Instantiate(Enemy, new Vector3(UnityEngine.Random.Range(transform.position.x + offset, maxCameraPos.x), 0, 0), Quaternion.identity);
        }
            
        //Physics2D.IgnoreCollision(GetComponent<Camera>().GetComponent<BoxCollider2D>(), Enemy.GetComponent<Collider2D>(), true);

        //Debug.Log("Camera Collider: " + GetComponent<Collider2D>());
        //Debug.Log("Enemy Collider: " + Enemy.GetComponent<Collider2D>());
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