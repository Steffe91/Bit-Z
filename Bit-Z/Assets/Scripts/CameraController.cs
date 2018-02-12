using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Use this for initialization
    void Awake ()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating("SpawnEnemy", spawnTime, spawnTime);
        //offset = transform.position - Player.transform.position;
    }
	
	// Update is called once per frame
	void Update ()
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

    }

    void SpawnEnemy()
    {
        Instantiate(Enemy, new Vector3(Random.Range(transform.position.x + offset, maxCameraPos.x), 0, 0),
                                       Quaternion.identity);
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
