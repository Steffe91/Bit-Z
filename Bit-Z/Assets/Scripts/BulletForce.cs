using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletForce : MonoBehaviour {

    public int speed;
    public bool goLeft = false;
    public bool goRight = false;


	// Use this for initialization
	void Start ()
    {
	    if(PlayerController.dirFacing == 1)
        {
            goLeft = true;
        }
        if(PlayerController.dirFacing == 2)
        {
            goRight = true;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(goLeft == true)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector3.left * speed);
            print(goLeft);
        }
        if (goRight == true)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector3.right * speed);
            print(goRight);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);
            Debug.Log(this.gameObject);
            Debug.Log("destroy Bullet");
        }

        if(collision.gameObject.tag == "MainCamera")
        {
            Physics2D.IgnoreLayerCollision(0, 11);
        }
    }
}
