﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public Transform DustParticle;

	private States state = States.Ground;
	private enum States {Ground, Air, Wall};

	[Range(2f, 20f)]
	public float JumpForce = 1f;
	[Range(2f, 10f)]
	public float GroundSpeed = 1f;
	[Range(10f, 100f)]
	public int Gravity = 12;

	private CharacterController2D controller;
	private PlatformMover platform;
	private Vector2 velocity = new Vector2();
	private LayerMask layerPlatforms;
	private LayerMask layerWalls;
	private int direction = 1;
	private bool grounded = false;
	private bool jumping = false;
	private bool falling = false;
	private bool facingRight = true;

	// animation
	private Animator animator;
	private int animWalk = Animator.StringToHash("PlayerWalk");
	private int animFall = Animator.StringToHash("PlayerFall");
	private int animClimb = Animator.StringToHash("PlayerClimb");
	private int animFlip = Animator.StringToHash("PlayerFlip");
	private int animWallHang = Animator.StringToHash("PlayerWall");

	// 
	private float deathLimit;

	public bool Frozen = false;

	void Start ()
	{
		layerPlatforms = LayerMask.NameToLayer("Platforms");
		layerWalls = LayerMask.NameToLayer("Walls");

		animator = GetComponent<Animator>();
		controller = GetComponent<CharacterController2D>();

		deathLimit = GameManager.instance.RemoveZone.position.y;
	}

	void Update ()
	{
		if (Frozen) return;

		velocity = controller.velocity;

		// check ground
		var wasGrounded = grounded;
		grounded = controller.isGrounded;

		if (grounded)
		{
			if (!wasGrounded)
				Land();
			HandleGround();
		}
		else if (state == States.Wall)
		{
			HandleWallSlide();
		}
		else
		{
			if (wasGrounded)
			{
				falling = true;
				animator.Play(animFall);
			}
			HandleAir();
		}

		// check for loss
		if (transform.position.y < deathLimit)
		{
			// game over
			Application.LoadLevel("GameOver");
		}
	}

	void HandleGround()
	{
		transform.Translate(0, -Time.deltaTime * GameManager.instance.PlatformSpeed, 0);

		velocity.y = 0;
		velocity.x = direction * GroundSpeed;

		if ((velocity.x > 0 && CheckWallRight()) || (velocity.x < 0 && CheckWallLeft()))
		{
			velocity.x *= -1;
			Flip();
			animator.Play(animFlip);
		}

		if (Input.anyKeyDown)
		{
			Jump();
		}

		velocity.y -= Gravity * Time.deltaTime;
		controller.move(velocity * Time.deltaTime);
	}

	void HandleAir()
	{
		if ((velocity.x > 0 && CheckWallRight()) || (velocity.x < 0 && CheckWallLeft()))
		{
			// start wall-sliding (will reset the velocity)
			StartWallSlide();
		}
		else
		{
			//velocity.x = direction * Time.deltaTime * GroundSpeed;
			velocity.y -= Gravity * Time.deltaTime;
		}

		controller.move(velocity * Time.deltaTime);
	}

	void StartWallSlide()
	{
		// set state
		state = States.Wall;

		// animation
		animator.Play(animWallHang);

		// reset speed
		velocity.x = 0;
		velocity.y = 0;
	}

	void HandleWallSlide()
	{
		// jump off the wall
		if (Input.anyKeyDown)
		{
			// flip the direction so we can jump the other way
			Flip();

			velocity.x = direction * GroundSpeed;
			Jump();
		}
		
		velocity.y -= Gravity * Time.deltaTime * .3f;
		controller.move (velocity * Time.deltaTime);
	}
	
	void Jump()
	{
		state = States.Air;

		// figure out the jump velocity
		var targetJumpHeight = JumpForce;
		velocity.y = Mathf.Sqrt(2f * targetJumpHeight * Gravity);

		// change flags
		jumping = true;

		// animate
		animator.Play(animFall);
	}

	private void Land()
	{
		// change the state
		state = States.Ground;

		// reset the flags
		jumping = falling = false;

		// animate
		animator.Play(animWalk);

		// save the platform we landed on
		var rayDistance = .2f;//velocity.y * Time.deltaTime;
		var rayDirection = -Vector2.up;
		var initialRayOrigin = new Vector3(transform.position.x, transform.position.y - controller.boxCollider.size.y * .5f); // bottom
		
		var ray = new Vector2(initialRayOrigin.x, initialRayOrigin.y);
		
		Debug.DrawRay(ray, rayDirection * rayDistance, Color.blue);
		var raycastHit = Physics2D.Raycast(ray, rayDirection, rayDistance, 1 << layerPlatforms.value);

		if (raycastHit)
			platform = raycastHit.collider.gameObject.GetComponent<PlatformMover>();

		Transform dust = Instantiate(DustParticle);
		Vector3 position = transform.position;
		position.y -= 1f;
		dust.position = position;
	}

	private void Flip()
	{
		// flip the object
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;

		// flip the flags
		facingRight = !facingRight;
		direction = -direction;
	}

	private bool CheckWallLeft()
	{
		//var rayDistance = Mathf.Max(velocity.x * Time.deltaTime, .1f);
		var rayDistance = velocity.x * Time.deltaTime;
		var rayDirection = -Vector2.right;
		var initialRayOrigin = new Vector3(transform.position.x - controller.boxCollider.size.x * 0.5f, transform.position.y);
		
		var ray = new Vector2(initialRayOrigin.x, initialRayOrigin.y);
		
		Debug.DrawRay( ray, rayDirection * rayDistance, Color.green );
		var raycastHit = Physics2D.Raycast(ray, rayDirection, rayDistance, 1 << layerWalls.value);
		if(raycastHit)
			return true;
		return false;
	}
	
	private bool CheckWallRight()
	{
		//var rayDistance = Mathf.Max(velocity.x * Time.deltaTime, .1f);
		var rayDistance = velocity.x * Time.deltaTime;
		var rayDirection = Vector2.right;
		var initialRayOrigin = new Vector3(transform.position.x + controller.boxCollider.size.x * 0.5f, transform.position.y);
		
		var ray = new Vector2(initialRayOrigin.x, initialRayOrigin.y);
		
		Debug.DrawRay( ray, rayDirection * rayDistance, Color.green );
		var raycastHit = Physics2D.Raycast(ray, rayDirection, rayDistance, 1 << layerWalls.value);
		if(raycastHit)
			return true;
		return false;
	}
}