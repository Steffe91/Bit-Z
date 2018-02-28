using UnityEngine;
using System.Collections;
using System;

public class Enemy<T> where T : Enemy
{
	public GameObject GameObject;
	public T ScriptComponent;

	public Enemy(string name)
	{
		GameObject = new GameObject(name);
		ScriptComponent = GameObject.AddComponent<T>();
	}
}

public abstract class Enemy : MonoBehaviour
{
	protected int HP;

	public Rigidbody2D Body;
	public SpriteRenderer Sprite;
	//public SpriteRenderer BossSprite;
	public BoxCollider2D Collider;

	public int Speed;
	public int Direction;

	protected abstract void MovementPattern();

	void Awake()
	{
		//Adds components common to all enemy types
		Body = gameObject.AddComponent<Rigidbody2D>();
		Sprite = gameObject.AddComponent<SpriteRenderer>();
		//BossSprite = gameObject.AddComponent<SpriteRenderer> ();
		Collider = gameObject.AddComponent<BoxCollider2D>();

		//Sets "eyeball" sprite common to all enemy types
		Sprite.sprite = Resources.Load<Sprite>("dark");
		//BossSprite.sprite = Resources.Load<Sprite> ("Boss");
		Body.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

		gameObject.tag = "Enemy";
		gameObject.layer = LayerMask.NameToLayer("Enemy");
	}

	//Enemy speed, direction and position determined by this pseudo-constructor just after instantiation in EnemiesFactory script
	public void Initialize(int speed, int direction, Vector3 position)
	{
		Speed = speed;
		Direction = direction;
		transform.position = position;
	}

	//Initialize overload for enemies that don't require a specific direction at instantiation
	public void Initialize(int speed, Vector3 position)
	{
		Speed = speed;
		transform.position = position;
	}

	//Called whenever the player damages the enemy
	public void DoDamage(int damageAmount)
	{
		HP -= damageAmount;

		if (HP <= 0)
		{
			Destroy(gameObject);
		}  
	}

	//Checks if the enemy hit the horizontal border of the screen, and pushes it back into play
	protected void BorderHitCheck(float force)
	{
		force *= Speed;
		Vector3 enemyPosition = Camera.main.WorldToViewportPoint(transform.position);

		if (enemyPosition.x < 0f)
		{
			Body.velocity = new Vector2(0, Body.velocity.y);
			Body.AddForce(new Vector2(force, 0));
			Direction = 1;
		}
		else if (enemyPosition.x > 1f)
		{
			Body.velocity = new Vector2(0, Body.velocity.y);
			Body.AddForce(new Vector2(force * -1, 0));
			Direction = -1;
		}
	}
		

	//Determines if the Enemy collided with the player and effects the player GameObject accordingly
/*	protected virtual void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Player")
		{
			Rigidbody2D Hero = coll.gameObject.GetComponent<Rigidbody2D>();
			Hero.velocity = new Vector2(0, 0);
			Hero.AddForce(new Vector2(0, 300));

			coll.gameObject.GetComponent<PlayerController>().enabled = false;
			coll.gameObject.GetComponent<Collider2D>().enabled = false;

			foreach (Transform child in coll.gameObject.transform)
				Destroy(child.gameObject);

			GameOver.isDead = true;
		}
	} */
}