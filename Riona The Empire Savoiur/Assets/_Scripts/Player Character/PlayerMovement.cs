//----------------------------------------------------------------------------------
///   Riona the Empire Saviour
///   PlayerMovement.cs
///   Author            : Geek’s Garage
///   Last Modified     : 2022/03/10
///   Description       : Control Movement, Jump, Camera and Animations 
///   Revision History  : 12th ed. Freezinf movement when loading screen is on
///----------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// @Negin and @Vineet -> 3rd Feb 2022
/// was done in a group meeting
/// </summary>
public class PlayerMovement : MonoBehaviour
{
	private CharacterController characterController;
	[Header("Movement Properties")]
	//public float WalkSpeed;
	public float RunSpeed;
	public float turnSpeed;
	private float targetRotation = 0.0f;
	private float rotationVelocity;
	[Range(0.0f, 0.3f)]
	public float RotationSmoothTime = 0.12f;

	private float rotation = 0f;

	public float gravity;
	public float JumpSpeed;
	public float floorDistance;
	public Vector3 velocity = Vector3.zero;
	public LayerMask groundLayerMask;

	[Header("Animation properties")]
	public float animSpeed;
	//bool isRunning;
	private Animator animator;

	[Header("Mobile input properties")]
	public Joystick joystick;

	// Camera adjustment
	private CameraController camera;

	private PlayerBehaviour _playerBehaviour;

	public Quaternion currentRotation;

	private Vector3 targetDirection;

	private bool executeOnce = false;
	public bool isMoving = false;
	public bool Grounded;
	public bool isJumping = false;
	public bool isFalling = false;
	public bool IsOnPlatform = false;

	public GameObject optionPanel;

	// Animations
	// Strings to hash to make it easier to set animator "set" functions
	public readonly int anim_movingHash = Animator.StringToHash("IsMoving");
	public readonly int anim_jumpingHash = Animator.StringToHash("IsJumping");
	public readonly int anim_IsGroundedHash = Animator.StringToHash("IsGrounded");
	public readonly int anim_IsFallingHash = Animator.StringToHash("IsFalling");
	public readonly int anim_IsJumpHash = Animator.StringToHash("IsJump");
	public readonly int anim_MovementX = Animator.StringToHash("MovementX");
	public readonly int anim_MovementY = Animator.StringToHash("MovementY");

	void Start()
	{
		characterController = GetComponent<CharacterController>();
		animator = GetComponent<Animator>();
		camera = GetComponent<CameraController>();
		_playerBehaviour = GetComponent<PlayerBehaviour>();

		executeOnce = true;

	}

	void Update()
	{
		if (!GameManager.GetInstance().levelIsLoading)
		{
			Move();
			animator.SetBool(anim_IsGroundedHash, isGrounded());
			if (!executeOnce && isMoving && isGrounded())
			{
				executeOnce = true;
				PlayFootSteps();
			}
			else if (!isGrounded())
			{

				executeOnce = false;
				_playerBehaviour.audioSource.Stop();
			}
		}
		else
		{
			AudioSource audioSource = _playerBehaviour.audioSource;



			if (audioSource.isPlaying)
			{
				_playerBehaviour.audioSource.Stop();
			}
		}
		
	}

	/// <summary>
	/// Player Footsteps
	/// </summary>
	private void PlayFootSteps()
	{
		AudioSource audioSource = _playerBehaviour.audioSource;

		

		if (audioSource.isPlaying)
		{
			_playerBehaviour.audioSource.Stop();
		}

		audioSource.loop = true;
		audioSource.clip = _playerBehaviour.SFXList[(int)PlayerBehaviour.PlayerSFX.Walk];
		audioSource.Play();
	}

	/// <summary>
	/// Move
	/// </summary>
	private void Move()
	{
		// Always have gravity on Riona

		// Check for grounded
		if (isGrounded() )
		{
			if(velocity.y < 0.0f)
			velocity.y = -0.3f;
            animator.SetBool(anim_IsGroundedHash, true);

            animator.SetBool(anim_IsFallingHash, false);
            isFalling = false;
            isJumping = false;
            animator.SetBool(anim_IsJumpHash, false);
            //if (Input.GetButtonDown("Jump") || UIController.JumpButtonDown)
            //{
            //    // animator.SetTrigger(anim_jumpingHash);
            //    //Debug.Log("Jump Trigger activated");
            //    animator.SetBool(anim_IsJumpHash, true);
            //
            //    isJumping = true;
            //
            //}
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
            animator.SetBool(anim_IsGroundedHash, false);
            if ((isJumping && velocity.y < 0) || velocity.y < -3  || (velocity.y > 2))
            {
                animator.SetBool(anim_IsFallingHash, true);
                isFalling = true;
                ChangeCapsuleCollider(0.01f);
            }
        }

        characterController.Move(Vector3.up * velocity.y* Time.deltaTime);
		// Set Target Run Speed
		float targetSpeed = RunSpeed;

		// Take Input from K/B or Virtual Joystick
		Vector3 movement = new Vector3(Input.GetAxis("Horizontal") + joystick.Horizontal,0f, Input.GetAxis("Vertical") + joystick.Vertical);
		//-----------------------KEY MAPPING-------------
		//if (KeyManager.GetKeyDown("Left") && !optionPanel.activeSelf)
		//    movement.x -= 1;

		//
		//if (KeyManager.GetKeyDown("Right") && !optionPanel.activeSelf)
		//    movement.x += 1;
		//

		//if (KeyManager.GetKeyDown("Up") && !optionPanel.activeSelf)
		//    movement.z += 1;
		//if (KeyManager.GetKeyDown("Down") && !optionPanel.activeSelf)
		//    movement.z -= 1;
		// Normalize them
		Vector3 moveVector = new Vector3(movement.x, 0, movement.z).normalized;

		if (_playerBehaviour.isInCombatMode)
		{
			// Combat animation
			animator.SetFloat(anim_MovementX, moveVector.x);
			animator.SetFloat(anim_MovementY, moveVector.z);
		}


		// Handle Rotation
		if (moveVector.magnitude >= 0.1f)
		{

			// Cursor lock
			//Cursor.lockState = CursorLockMode.Locked;
			//Cursor.visible = false;

			
			
			

			// Calculate rotation
			targetRotation = Mathf.Atan2(moveVector.x, moveVector.z) * Mathf.Rad2Deg + camera.GetFollowTarget().eulerAngles.y;

			
			if (!_playerBehaviour.isInCombatMode)
			{

				// Smooth the rotation
				rotation = Mathf.SmoothDampAngle(rotation, targetRotation, ref rotationVelocity, RotationSmoothTime);

				// Adjust rotation
				transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
			}

			// is Moving
			isMoving = true;

			// Target Direction using forward vector
			targetDirection = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward;


			// Give the direction, speed to controller
			characterController.Move(targetDirection.normalized * (targetSpeed * Time.deltaTime));
		}
		else
		{
			// Not moving
			isMoving = false;

			// Set Speed to 0
			targetSpeed = 0.0f;

			// taxing statement.
			//if (!_playerBehaviour.isInCombatMode)
			//{
			//    Cursor.lockState = CursorLockMode.Confined;
			//    Cursor.visible = true;
			//}
			

			executeOnce = false;

			//_playerBehaviour.audioSource.Stop();
			_playerBehaviour.audioSource.loop = false;
		}

		// Animation
		animator.SetBool(anim_movingHash,isMoving);

		// Handle Jump
		if (isGrounded())
		{
			if (Input.GetButtonDown("Jump") || UIController.JumpButtonDown )
			{
				// animator.SetTrigger(anim_jumpingHash);
				//Debug.Log("Jump Trigger activated");
				animator.SetBool(anim_IsJumpHash, true);

				isJumping = true;
			}
		}
	}
	private bool isGrounded()
	{
		
		RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, floorDistance, groundLayerMask))
        {
            Grounded = true;
            ChangeCapsuleCollider(0.17f);
        }
        else
        {
            Grounded = false;
        }
		return Grounded;
	}

	/// <summary>
	/// Jump animation event
	/// </summary>
	public void JumpForceAnimation()
	{
		AudioSource audioSource = _playerBehaviour.audioSource;

		//Debug.Log("Playing footsteps");

		if (audioSource.isPlaying)
		{
			_playerBehaviour.audioSource.Stop();
		}

		audioSource.loop = false;

		audioSource.clip = _playerBehaviour.SFXList[(int)PlayerBehaviour.PlayerSFX.Jump];
		audioSource.Play();

		velocity.y = JumpSpeed;
	}


	/// <summary>
	/// Rotate the player character for combat mode
	/// </summary>

	public void RotateForCombatMode()
	{
		transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Camera.main.transform.localEulerAngles.y, transform.localEulerAngles.z);
	}

	private void OnDrawGizmos()
	{
		//Gizmos.color = Color.magenta;

        //Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }
    private void ChangeCapsuleCollider(float radius)
	{
        characterController.radius = radius;

	}
}

