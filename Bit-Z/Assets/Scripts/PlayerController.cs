
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float maxSpeed = 10f;
    public Vector2 jumpForce = new Vector2(0, 300);
    public Rigidbody2D rig_bod;
    public LayerMask ground;
    public static int dirFacing = 2;
    public int life = 3;
    private GameObject Bullet;
    bool facingRight = true;

    Animator anim;

    
    // Use this for initialization
	void Start ()
    {
        rig_bod = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();


    }

    /// <summary>
    /// No Use of delta-time because FixedUpdate function -> called for working with rigidbody!
    /// FixedUpdate is called once per Frame!
    /// </summary>
    void FixedUpdate()
    {
        

        float move = Input.GetAxis("Horizontal");

        anim.SetFloat("Speed", Mathf.Abs(move));

        rig_bod.velocity = new Vector2(move * maxSpeed, rig_bod.velocity.y);

        if (move > 0 && !facingRight)
        {
            Flip();
            dirFacing = 2;
        }
        else if (move < 0 && facingRight)
        {
            Flip();
            dirFacing = 1;
        }

		if (Input.GetKeyDown("w"))
        {        
            Jump();     
        }

        if(life == 0)
        {
            //Destroy(this.gameObject);
            Time.timeScale = 0;
            Application.Quit();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    bool IsGrounded()
    {
        Vector2 pos = transform.position;
        Vector2 dir = Vector2.down;
        float dis = 2.5f;

        Debug.DrawRay(pos, dir, Color.green);
        RaycastHit2D hit = Physics2D.Raycast(pos, dir, dis, ground);
        if(hit.collider != null)
        {
            return true;
        }

        return false;
    }

    void Jump()
    {
        if(!IsGrounded())
        {
            return;
        }
        else
        {
            rig_bod.AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            life--;
            Debug.Log(life);
        }
        if(collision.gameObject.tag == "Bullet")
        {
            Physics2D.IgnoreLayerCollision(8, 11);
        }
    }
}

