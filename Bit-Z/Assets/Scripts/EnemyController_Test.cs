﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController_Test : MonoBehaviour {

    GameObject Player;

    public float move;
    public float Speed = 10f;
    public bool facingLeft = true;
	public static int Counter = 0;
    Vector2 Player_pos = new Vector2();
    Vector2 Enemy_pos = new Vector2();
    Vector2 move_Direction = new Vector2();
    Rigidbody2D AI_rig;

    Animator enemy_anim;
     
    // Use this for initialization
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        AI_rig = GetComponent<Rigidbody2D>();

		if(!PauseMenu.isPaused)
			enemy_anim = GetComponent<Animator>();
        
    }

    /// <summary>
    /// No Use of delta-time because FixedUpdate function -> called for working with rigidbody!
    /// FixedUpdate is called once per Frame!
    /// </summary>
    void FixedUpdate()
    {
        
		if (!PauseMenu.isPaused) {
			Player_pos = Player.transform.position;
			Enemy_pos = this.transform.position;
       
			move_Direction = Player_pos - Enemy_pos;
			enemy_anim.enabled = true;

			move = move_Direction.magnitude;

			if(facingLeft)
			{
				AI_rig.velocity = new Vector2(-Speed, GetComponent<Rigidbody2D>().velocity.y);
			}
			else if(!facingLeft)
			{
				AI_rig.velocity = new Vector2(Speed, GetComponent<Rigidbody2D>().velocity.y);
			} 

			enemy_anim.SetFloat ("Speed", Mathf.Abs (move));

			if (Player_pos.x > Enemy_pos.x && facingLeft == true) {
				Flip ();
			} else if (Enemy_pos.x > Player_pos.x && facingLeft == false) {
				Flip ();
			}
		} else {
			enemy_anim.enabled = false;
			AI_rig.velocity = Vector2.zero;
		}
    }

    void Flip()
    {
        facingLeft = !facingLeft;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(this.gameObject);
			Counter++;
			Debug.Log("Counter: " +Counter);

        }
    }
}
