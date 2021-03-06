﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stanley : MonoBehaviour {

	GameObject Player;

	public float Life;
    //public bool StanleyIsDead;
	public float Move;
	public int Speed = 4;
	public bool FacingLeft = false;
	public static int Counter = 0;
	Vector2 HeroPosition = new Vector2();
	Vector2 StanleyDaBoss = new Vector2();
	Vector2 MoveDirection = new Vector2();
	Rigidbody2D StanleysHotRigidBody;

	public int Direction;

	Animator EnemyAnimator;

	// Use this for initialization
	void Start()
	{
		Player = GameObject.FindGameObjectWithTag("Player");
		StanleysHotRigidBody = GetComponent<Rigidbody2D>();
        

        if (!PauseMenu.isPaused)
			EnemyAnimator = GetComponent<Animator>();
	}


		
	void FixedUpdate()
	{

		if (!PauseMenu.isPaused)
        {
			HeroPosition = Player.transform.position;
			StanleyDaBoss = this.transform.position;

			MoveDirection = HeroPosition - StanleyDaBoss;
			//EnemyAnimator.enabled = true;
			Direction = Random.Range (0, 2) * 2 - 1;

            Debug.Log("Direction" + Direction);
			

			Move = MoveDirection.magnitude;

            if (!FacingLeft)
            {
                //StanleysHotRigidBody.GetComponent<Rigidbody2D>().velocity = new Vector2(-Speed, GetComponent<Rigidbody2D>().velocity.y);
                StanleysHotRigidBody.velocity = new Vector2(-(Random.Range(-5, 5) * Speed), Random.Range(-5, 5) * Speed);
            }
            else if (FacingLeft)
            {
                //StanleysHotRigidBody.GetComponent<Rigidbody2D>().velocity = new Vector2(Speed, GetComponent<Rigidbody2D>().velocity.y);
                StanleysHotRigidBody.velocity = new Vector2(Random.Range(-5, 5) * Speed, Random.Range(-5,5)*Speed);
            }

            EnemyAnimator.SetFloat("Speed", Mathf.Abs(Move));

            if (HeroPosition.x > StanleyDaBoss.x && FacingLeft == true)
            {
				Flip ();
			}
            else if (StanleyDaBoss.x > HeroPosition.x && FacingLeft == false)
            {
				Flip ();
			}

           // StanleyJumpLikeCrazy ();
        }

        //EnemyAnimator.enabled = false;
        //StanleysHotRigidBody.velocity = Vector2.zero;



    }

	void Flip()
	{
		FacingLeft = !FacingLeft;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
        if (Life < 1)
        {
            PlayerController.StanleyIsDead = true;
            Destroy(this.gameObject);
            
        }

        if (collision.gameObject.tag == "Bullet")
		{
			Life--;
		}
	}

	private void StanleyJumpLikeCrazy()
	{
		StanleysHotRigidBody.velocity = new Vector2(1 * Direction, StanleysHotRigidBody.velocity.y);

		if (StanleysHotRigidBody.velocity.y == 0)
			StanleysHotRigidBody.AddForce(new Vector2(0, 20));

		//bouncyBill.ScriptComponent.Initialize(speed: 4, direction: Random.Range(0, 2) * 2 - 1, position: new Vector3(randomX, randomY, 1));

	}

}
