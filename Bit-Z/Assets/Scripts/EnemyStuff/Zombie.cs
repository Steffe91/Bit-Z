using UnityEngine;
using System.Collections;
using System;

public class Zombie : Enemy
{
	void Start()
	{
		HP = 1;

		//Collider.radius = 0.21f;
		//Body.gravityScale = 1;

		GameObject Hero = GameObject.Find("TestCharakter");
		Direction = (Hero.transform.position.x - transform.position.x < 0) ? -1 : 1;
	}

	void FixedUpdate()
	{
		MovementPattern();
	}

	//Simple slow-moving enemy
	protected override void MovementPattern()
	{
		Body.velocity = new Vector2(Speed * Direction, Body.velocity.y);
	}
}