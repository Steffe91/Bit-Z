
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float maxSpeed = 10f;
    public float jumpForce = 10f;
    public Rigidbody2D PlayerBody;
    public LayerMask ground;
    public static int dirFacing = 2;
    public int Health;
    public GameOver gameOver;
    public Win win;
    private CameraController cc;
    public static bool StanleyIsDead;
    //private Stanley stan;
    //public Slider Healthbar;

    // tweaking movement
    float HorizontalMotion;
	bool JumpActive;
	public static int MoveSpeed;

    private GameObject Bullet;
    
    public int _currentHealth;
    bool facingRight = true;

    Animator anim;

    
    // Use this for initialization
	void Start ()
    {

        // tweak movement
        StanleyIsDead = false;
        HorizontalMotion = 0;
		MoveSpeed = 10;

		PlayerState.Instance.Horizontal = Horizontal.Idle;
		PlayerState.Instance.Vertical = Vertical.Airborne;
		PlayerState.Instance.DirectionFacing = DirectionFacing.Right;
		PlayerState.Instance.Attack = Attack.Passive;

		// ******


        PlayerBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        

        _currentHealth = Health;
        //Healthbar.maxValue = Health;
        //Healthbar.value = Health;


    }

    /// <summary>
    /// No Use of delta-time because FixedUpdate function -> called for working with rigidbody!
    /// FixedUpdate is called once per Frame!
    /// </summary>
    void FixedUpdate()
    {
        if (StanleyIsDead)
        {
            Win();
        }

        Walk();
		Jump();

		float move = Input.GetAxis("Horizontal");
		anim.SetFloat("Speed", Mathf.Abs(move));
		PlayerBody.velocity = new Vector2(move * maxSpeed, PlayerBody.velocity.y);

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            Death();
        }

        //Debug.Log("Health: " + _currentHealth);
        //Debug.Log("Healthbar-Health: " + Healthbar.value);

        //Allow player movement only when not attacking
        if (PlayerState.Instance.Attack != Attack.Passive)
		{
			PlayerBody.velocity = new Vector2(0, 0.1f);
			HorizontalMotion = 0;
		}
		else
		{
			HorizontalMotion = Input.GetAxisRaw("Horizontal");

			if (HorizontalMotion != 0)
			{
				transform.localScale = new Vector3(HorizontalMotion, 1, 1);
				PlayerState.Instance.DirectionFacing = (DirectionFacing)HorizontalMotion;
			}

			if (Input.GetButtonDown("Jump"))
				JumpActive = true;
		}

		if (PlayerBody.velocity.y == 0 && PlayerState.Instance.Attack == Attack.Passive)
			PlayerState.Instance.Vertical = Vertical.Grounded;

		Horizontal previousMotion = PlayerState.Instance.Horizontal;
		Horizontal currentMotion = PlayerState.Instance.Horizontal = (Horizontal)HorizontalMotion;

		//Fixes an error with the camera following the player incorrectly if quickly changing direction while at the furthest possible positions at each side of the screen.
		if ((int)previousMotion * (int)currentMotion == -1)
			PlayerState.Instance.Horizontal = Horizontal.Idle;

    }

	private void Walk()
	{
		PlayerBody.velocity = new Vector2(HorizontalMotion * MoveSpeed, PlayerBody.velocity.y);
	}

	//Handles player's vertical state and allows jumping only when grounded, using physics-based AddForce(), called in FixedUpdate()
	private void Jump()
	{
		if (JumpActive)
		{
			if (PlayerState.Instance.Vertical == Vertical.Grounded)
			{
				PlayerState.Instance.Vertical = Vertical.Airborne;
				PlayerBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
			}
			JumpActive = false;
		}
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            TakeDamage(1);
        }
        if(collision.gameObject.tag == "Bullet")
        {
            Physics2D.IgnoreLayerCollision(8, 11);
        }
    }

    void TakeDamage(int amount)
    {
        _currentHealth -= amount;
        //Healthbar.value = _currentHealth;
    }

    void Death()
    {
        //Destroy(this.gameObject);
        //Time.timeScale = 0;

            gameOver.isDead = true;
            //Debug.Break();
            //Application.Quit();
    }

    void Win()
    {
        win.hasWon = true;
    }
}

