using UnityEngine;
using System.Collections;

public class PlayerState : MonoBehaviour
{
	//Singleton instance, globally accessible for setting player's state
	private static PlayerState _instance;
	public static PlayerState Instance
	{
		get
		{
			if (_instance == null)
				_instance = new GameObject("PlayerState").AddComponent<PlayerState>();

			return _instance;
		}
	}

	//Stores exclusive states for Horizontal, Vertical, Direction and Attack states
	public Horizontal Horizontal;
	public Vertical Vertical;
	public DirectionFacing DirectionFacing;
	public Attack Attack;
}

public enum Horizontal
{
	Idle = 0,
	MovingLeft = -1,
	MovingRight = 1
}

public enum Vertical
{
	Grounded,
	Airborne
}

public enum DirectionFacing
{
	Left = -1,
	Right = 1
}

public enum Attack
{
	Passive,
	Punch,
	Projectile
}


