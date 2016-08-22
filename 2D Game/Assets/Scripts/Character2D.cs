using UnityEngine;
using System;

public class Character2D : MonoBehaviour {

	[SerializeField] private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
	[SerializeField] private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
	[SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character
	[SerializeField] private LayerMask m_WhatIsClimbable;

    private Player player;
	private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
	const float k_GroundedRadius = .01f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded;            // Whether or not the player is grounded.
	private Transform m_CeilingCheck;   // A position marking where to check for ceilings
	const float k_CeilingRadius = .05f; // Radius of the overlap circle to determine if the player can stand up
	private bool m_Ceiling;  
	private bool m_Wall;
	private Transform m_ClimbCheckA;
	private Transform m_ClimbCheckB;
	private Animator m_Anim;            // Reference to the player's animator component.
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private bool canDoubleJump;
	private bool canClimb;
    //private bool stamina;
	private float gravity;
	
	private void Awake()
	{
        // Setting up references.
		m_GroundCheck = transform.Find("GroundCheck");
		m_CeilingCheck = transform.Find("CeilingCheck");
		m_ClimbCheckA = transform.Find ("ClimbCheckA");
		m_ClimbCheckB = transform.Find ("ClimbCheckB");
		m_Anim = GetComponent<Animator>();
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		gravity = m_Rigidbody2D.gravityScale;

        GameObject thePlayer = GameObject.Find("Player");
        player = thePlayer.GetComponent<Player>();
    }
	
	
	private void Update()
	{
        /*if (player.getStamina() == 0)
            m_Anim.SetBool("Climb", false);*/
            //Debug.Log("Hello");
		if (m_Rigidbody2D.velocity.x == 0 && m_Rigidbody2D.velocity.y == 0) {
			m_Anim.SetBool ("Stationary", true);
		} else 
		{
			m_Anim.SetBool("Stationary", false);
		}
		if (Physics2D.OverlapCircle (m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround)) {
			m_Grounded = true;
			canDoubleJump = true;
		} else {
			m_Grounded = false;
		}

		if (m_Grounded != m_Anim.GetBool ("Ground")) 
		{
			m_Anim.SetBool ("Ground", m_Grounded);
		}
		
		// Set the vertical animation
		m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);
		m_Anim.SetFloat("Speed", m_Rigidbody2D.velocity.x);

		if (Physics2D.OverlapArea (m_ClimbCheckA.position, m_ClimbCheckB.position, m_WhatIsClimbable)) {
			m_Wall = true;
		} else {
			m_Wall = false;
		}
		if (m_Wall != m_Anim.GetBool ("Wall")) 
		{
			m_Anim.SetBool ("Wall", m_Wall);
		}

		if (Physics2D.OverlapCircle (m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround)) {
			m_Ceiling = true;
		} else {
			m_Ceiling = false;
		}
		if (m_Ceiling != m_Anim.GetBool ("Ceiling")) 
		{
			m_Anim.SetBool ("Ceiling", m_Ceiling);
		}

		if ((Physics2D.OverlapArea (m_ClimbCheckA.position, m_ClimbCheckB.position, m_WhatIsClimbable)) || (Physics2D.OverlapCircle (m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))) 
		{
			canClimb = true;
		} else {
			canClimb = false;
		}
	}
	
	
	public void Move(float moveH, float moveV, bool climb, bool jump)
	{
		if (canClimb && climb && player.getStamina() > 0) 
		{
			if (!m_Anim.GetBool("Climb"))
			{
				m_Anim.SetBool ("Climb",true);
			}

			if(m_Anim.GetBool("Wall") && !m_Anim.GetBool("Ceiling"))
			{
				m_Anim.SetFloat ("Speed", 0);
				if(m_Rigidbody2D.gravityScale != 0)
				{
					m_Rigidbody2D.gravityScale = 0;
				}
				m_Rigidbody2D.velocity = new Vector2 (0, moveV * (m_MaxSpeed / 2));
				m_Anim.SetFloat ("vSpeed", Mathf.Abs (moveV));
			}

			if(m_Anim.GetBool ("Ceiling") && !m_Anim.GetBool("Wall"))
			{
				m_Anim.SetFloat ("vSpeed", 0);
				if(m_Rigidbody2D.gravityScale != 0)
				{
					m_Rigidbody2D.gravityScale = 0;
				}
				m_Rigidbody2D.velocity = new Vector2 (moveH * (m_MaxSpeed / 2), 0);
				m_Anim.SetFloat ("Speed", Mathf.Abs (moveH));
			}

			if(m_Anim.GetBool ("Ceiling") && m_Anim.GetBool("Wall"))
			{
				m_Anim.SetFloat ("vSpeed", 0);
				m_Anim.SetFloat ("Speed", 0);
				m_Anim.SetBool ("Stationary", true);
				if(m_Rigidbody2D.gravityScale != 0)
				{
					m_Rigidbody2D.gravityScale = 0;
				}

				if (moveH != 0){
					m_Rigidbody2D.velocity = new Vector2 (moveH * (m_MaxSpeed / 2), 0);
					m_Anim.SetFloat ("Speed", Mathf.Abs (moveH));
				}
				if (moveV != 0){
					m_Rigidbody2D.velocity = new Vector2 (0, moveV * (m_MaxSpeed / 2));
					m_Anim.SetFloat ("vSpeed", Mathf.Abs (moveV));
				}
			}
			canDoubleJump = true;
		} else 
		{
			if(m_Rigidbody2D.gravityScale != gravity)
			{
				m_Rigidbody2D.gravityScale = gravity;
			}
			if(m_Anim.GetBool("Climb"))
			{
				m_Anim.SetBool ("Climb",false);
			}

			// The Speed animator parameter is set to the absolute value of the horizontal input.
			m_Anim.SetFloat ("Speed", Mathf.Abs (moveH));
			
			// Move the character
			m_Rigidbody2D.velocity = new Vector2 (moveH * m_MaxSpeed, m_Rigidbody2D.velocity.y);
		
			// If the player should jump...
			if (m_Grounded && jump && m_Anim.GetBool ("Ground")) {
				// Add a vertical force to the player.
				m_Grounded = false;
				m_Anim.SetBool ("Ground", false);
				m_Rigidbody2D.AddForce (new Vector2 (0f, m_JumpForce));
			} else if (jump && canDoubleJump) {
				m_Rigidbody2D.velocity = new Vector2 (m_Rigidbody2D.velocity.x, 0);
				m_Rigidbody2D.AddForce (new Vector2 (0f, m_JumpForce));
				canDoubleJump = false;
			}
		}
		if (!m_Anim.GetBool ("Climb") || m_Anim.GetBool ("Ceiling")) {
			if (moveH > 0 && !m_FacingRight) {
				// ... flip the player.
				Flip ();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (moveH < 0 && m_FacingRight) {
				// ... flip the player.
				Flip ();
			}
		}
	}

	private void ClimbCeiling(float moveH)
	{
		m_Anim.SetFloat ("vSpeed", 0);
		if(m_Rigidbody2D.gravityScale != 0)
		{
			m_Rigidbody2D.gravityScale = 0;
		}
		m_Rigidbody2D.velocity = new Vector2 (moveH * (m_MaxSpeed / 2), 0);
		m_Anim.SetFloat ("Speed", Mathf.Abs (moveH));
	}

	private void ClimbWall(float moveV)
	{
		m_Anim.SetFloat ("Speed", 0);
		if(m_Rigidbody2D.gravityScale != 0)
		{
			m_Rigidbody2D.gravityScale = 0;
		}
		m_Rigidbody2D.velocity = new Vector2 (0, moveV * (m_MaxSpeed / 2));
	}
	
	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;
		
		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
