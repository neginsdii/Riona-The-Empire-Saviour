//----------------------------------------------------------------------------------
///   Riona the Empire Saviour
///   PlayerBehaviour.cs
///   Author            : Geek’s Garage
///   Last Modified     : 2022/04/18
///   Description       : A player controller that manages over all player behaviour
///   Revision History  : 12th ed. Fix for Riona drink potion sfx 
///----------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ObjectivesEnum
{
	ReachEndOfLevel,
	TotalObjectives
}

public enum AttackTypes
{
	FIRE,
	ICE,
	LIGHTNING,
	TOTAL_TYPES
}

public class PlayerBehaviour : MonoBehaviour, ISaveable
{

	public enum PlayerSFX
	{
		Walk,
		Jump,
		Attack,
		TakeDamage,
		NumberOfSFX
	}

	public List<Color> AttackTypeColors = new List<Color>();

	// Character Controller component
	//private CharacterController controller;

	// Movement variables
	[Header("Movement")]
	public float movementSpeed = 10f;
	private Vector3 velocity;

	// Animation
	[Header("Animation")]
	private Animator _playerAnimator;

	// Strings to hash to make it easier to set animator "set" functions
	public readonly int anim_combatModeHash = Animator.StringToHash("IsInCombatMode");
	public readonly int anim_magicAttackHash = Animator.StringToHash("MagicAttack");
	public readonly int anim_isHitHash = Animator.StringToHash("IsHit");

	[Header("Combat Mode")] 
	public bool isInCombatMode = false;

	public bool isCombatModeButtonPressed = false;
	public bool isFiring = false;
	public GameObject crosshair;
	public List<GameObject> AttackType = new List<GameObject>();
	public AttackTypes currentAttackType;

	// Camera
	private CameraController camera;

	[Header("Health")]
	[SerializeField]
	private float currentHP = 100;
	[SerializeField]
	private float MaxHP = 100;
	private float tempHP;
	private float HPbeforeDamage;
	public Image HPbar;
	public bool isUpdatingHPBar = false;
	private float lerpT = 0;
	public float LerpSpeed = 1f;
	public bool isDying = false;

	[Header("Objectives")]
	public int ObjectivesCompleted = 0;

	private List<bool> ObjectiveList;

	private GameManager _gameManager;
	public UIController UIController;

	[Header("Sounds")] 
	public AudioSource audioSource;

	public List<AudioClip> SFXList;

	// fire, ice, lightning - 0, 1, 2
	private bool[] ProjectilesEnabled = {true, false, false};
	public SceneController sceneController;
	// Start is called before the first frame update
	void Start()
	{
		//controller = GetComponent<CharacterController>();
		camera = GetComponent<CameraController>();
		_playerAnimator = GetComponent<Animator>();

		currentHP = MaxHP = 100.0f;

		lerpT = 0f;

		// List of objectives
		ObjectiveList = new List<bool>();

		// Game manager
		_gameManager = GameObject.FindObjectOfType<GameManager>();

		// Audio Source
		audioSource = GetComponent<AudioSource>();

		// Deactivate Gameobject
		crosshair.gameObject.SetActive(false);

		// Disable all attack types
		foreach (var type in AttackType)
		{
			type.gameObject.SetActive(false);
		}

		currentAttackType = AttackTypes.FIRE;

		// Combat mode button press
		isCombatModeButtonPressed = false;
	}

	// Update is called once per frame
	void Update()
	{

		// Move function
		Move();

		// Toggle combat mode
		//ToggleCombatMode();

		// If in combat mdoe, Attack
		//if (isInCombatMode)
		//{
		//    Attack();
		//}


		// Lerp the HP bar
		if (isUpdatingHPBar)
		{
			UpdateHPBar();
		}

		// Debug

		//if (Input.GetKeyDown(KeyCode.M))
		//{
		//    transform.position = new Vector3(-12.48f, 1.74f, 9.62f);
		//}

	}


	/// <summary>
	/// Move function
	/// </summary>
	private void Move()
	{
		// get horizontal and vertical axis
		float X = Input.GetAxis("Horizontal");
		float Z = Input.GetAxis("Vertical");

		velocity = new Vector3(X, 0, Z);

		//controller.Move(velocity * movementSpeed * Time.deltaTime);
	}

	/// <summary>
	/// Function to toggle combat mode and trigger the correct animation
	/// </summary>
	public void ToggleCombatMode()
	{
		// Mouse RMB
		//if (/*Input.GetMouseButtonDown(1)*/ /*&& !GetComponent<PlayerMovement>().Grounded*/
		//    UIController.CombatMode
		//    )
		//{
			isInCombatMode = !isInCombatMode;

			if (isInCombatMode)
			{
				camera.ChangeCamera(isInCombatMode);
				crosshair.gameObject.SetActive(true);
				crosshair.gameObject.GetComponent<Image>().color = AttackTypeColors[(int)currentAttackType];

				// Enable attack type hint to player
				AttackType[(int)currentAttackType].gameObject.SetActive(true);
			}
			else
			{
				camera.ChangeCamera(isInCombatMode);
				crosshair.gameObject.SetActive(false);
				crosshair.gameObject.GetComponent<Image>().color = Color.white;

				// Disable attack type hint to player
				AttackType[(int)currentAttackType].gameObject.SetActive(false);
			}

			// Set/ Trigger the animation using bool
			_playerAnimator.SetBool(anim_combatModeHash, isInCombatMode);
		//}
	}

	/// <summary>
	/// Implementation of basic attacks IF in combat mode
	/// </summary>
	public void Attack()
	{
		// For now just play the animation
		// Mouse LMB
		if (/*Input.GetMouseButtonDown(0) &&*/ !isFiring)
		{
			isFiring = true;

			// Directly plays the animation
			_playerAnimator.Play(anim_magicAttackHash, -1, 0f);

			// Audio for attack
			audioSource.Stop();
			audioSource.clip = SFXList[(int)PlayerSFX.Attack];
			audioSource.loop = false;
			audioSource.volume = 0.5f;
			audioSource.Play();
		}
	}

	/// <summary>
	/// Take damage
	/// </summary>
	/// <param name="damage"></param>
	public void UpdateHP(int damage)
	{
		if (!isDying)
		{

		   if (damage < 0)
		{
			_playerAnimator.Play("Getting Hit",0,0);
			isFiring = false;
		}

		HPbeforeDamage = currentHP;

		currentHP += damage;

		currentHP = Mathf.Clamp(currentHP, 0, MaxHP);

		isUpdatingHPBar = true;


            if (damage < 0)
            {

                // Take damage audio
                audioSource.clip = SFXList[(int)PlayerSFX.TakeDamage];
                audioSource.PlayOneShot(audioSource.clip);
			}

            if (currentHP <= 0)
			{
				_playerAnimator.Play("RionaDeathForward", 0, 0);
				isDying = true;
				if (isInCombatMode)
				{
					UIController.Button_CombatModeToggle();
				}
				GetComponent<PlayerMovement>().isMoving = false;
				GetComponent<PlayerMovement>().enabled = false;
				StartCoroutine(DelayAfterDeathAnimation());

			}
		}
	}
	IEnumerator DelayAfterDeathAnimation()
	{
		yield return new WaitForSeconds(3.0f);
		GetComponent<PlayerMovement>().enabled = true;

		isDying = false;
		_playerAnimator.Play("Idle", 0, 0);
		TriggerCheckpoint();
	}
	/// <summary>
	/// Respawn the player
	/// </summary>
	private void TriggerCheckpoint()
	{
		//Debug.Log("Checkpoint activated with " + _gameManager.rionaSpawnPoint.position);
		if (_gameManager)
		{
			GetComponent<CharacterController>().enabled = false;
			transform.position = _gameManager.RionaPosition;
			GetComponent<CharacterController>().enabled = true;
			UpdateHP(100);
		}
	}


	/// <summary>
	/// Update the HP bar via Lerp
	/// </summary>
	private void UpdateHPBar()
	{
		// Get Lerp Time = Lerp Rate * deltatime
		lerpT += LerpSpeed * Time.deltaTime;

		// Get Lerp Value
		tempHP = Mathf.Lerp(HPbeforeDamage, currentHP, lerpT);

		// Start updating the fill amount with that lerp value / max HP
		HPbar.fillAmount = tempHP / MaxHP;

		// If HP updated, stop calling this function and reset lerp time
		if (tempHP == currentHP)
		{
			isUpdatingHPBar = false;
			lerpT = 0;
		}
	}


	/// <summary>
	/// Returns which objective has been completed
	/// </summary>
	public bool GetObjectiveStats(int objectiveNumber)
	{
		// If requesting total objectives stats, iterate through the entire list
		// if one also is false, return false
		if (objectiveNumber == (int)ObjectivesEnum.TotalObjectives)
		{
			foreach (var objective in ObjectiveList)
			{
				if (!objective)
				{
					return false;
				}
				else
				{
					return true;
				}
			}
		}

		return ObjectiveList[objectiveNumber];
	}


	/// <summary>
	/// Updates the stats as to which objective was completed
	/// </summary>
	/// <param name="objectiveNumber"></param>
	public void CompleteObjective(int objectiveNumber)
	{
		ObjectiveList[objectiveNumber] = true;
	}

	/// <summary>
	/// Fired the projectile
	/// </summary>
	public void Fired()
	{
		isFiring = false;
	}

	/// <summary>
	/// Toggle Combat Mode button
	/// </summary>
	public void Button_ToggleCombat()
	{
		isCombatModeButtonPressed = !isCombatModeButtonPressed;
	}

	/// <summary>
	/// 
	/// </summary>
	public void PopulateSaveData(SaveData a_SaveData)
	{
		SaveData.PlayerData player = new SaveData.PlayerData();
		player.m_health = currentHP;
		player.posX = transform.position.x;
		player.posY = transform.position.y;
		player.posZ = transform.position.z;

		player.Rotx = this.transform.rotation.x;
		player.Roty = this.transform.rotation.y;
		player.Rotz = this.transform.rotation.z;
		player.Rotw = this.transform.rotation.w;

		a_SaveData.player = player;

		for (int i = 0; i < ProjectilesEnabled.Length; i++)
		{
            a_SaveData.ProjectilesEnabled[i] = ProjectilesEnabled[i];

        }
    }
	public void SaveOnCheckPointTrigger()
	{
		sceneController.OnSaveButtonPRessed();
	}
    /// <summary>
    /// 
    /// </summary>
    public void LoadFromSaveData(SaveData a_SaveData)
    {
        GetComponent<CharacterController>().enabled = false;
        this.transform.position = new Vector3(a_SaveData.player.posX, a_SaveData.player.posY, a_SaveData.player.posZ);
        this.transform.rotation.Set(a_SaveData.player.Rotx, a_SaveData.player.Roty, a_SaveData.player.Rotz, a_SaveData.player.Rotw);
        currentHP = a_SaveData.player.m_health;
        HPbar.fillAmount = currentHP / MaxHP;
        GetComponent<CharacterController>().enabled = true;
        for (int i = 0; i < a_SaveData.ProjectilesEnabled.Length; i++)
        {
          ProjectilesEnabled[i] = a_SaveData.ProjectilesEnabled[i];

        }
    }



    /// <summary>
    /// Enable attack type element based on item type
    /// </summary>
    /// <param name="type"></param>
    public void EnableAttackType(ItemType type)
    {
        switch (type)
        {
            case ItemType.SPELLBOOK_TYPE_1:
                ProjectilesEnabled[1] = true;
                //Debug.Log("Attack type " + AttackTypes.ICE + ProjectilesEnabled[1]);
                break;

            case ItemType.SPELLBOOK_TYPE_2:
                ProjectilesEnabled[2] = true;
                //Debug.Log("Attack type " + AttackTypes.LIGHTNING + ProjectilesEnabled[2]);
                
                break;

            default:
                //Debug.Log("Attack type " + AttackTypes.ICE + " enabled");
                break;
        }
    }

    /// <summary>
    /// Changes the element
    /// </summary>
    public AttackTypes ChangeElement()
    {
        // Set current attack type
        if (currentAttackType == AttackTypes.FIRE && ProjectilesEnabled[(int)AttackTypes.ICE])
        {
            currentAttackType = AttackTypes.ICE;
            GetComponent<PlayerCombat>().projectileType = currentAttackType;
        }
        else if (currentAttackType == AttackTypes.ICE && ProjectilesEnabled[(int)AttackTypes.LIGHTNING] 
                || (currentAttackType == AttackTypes.FIRE && !ProjectilesEnabled[(int)AttackTypes.ICE] && ProjectilesEnabled[(int)AttackTypes.LIGHTNING]))
        {
            currentAttackType = AttackTypes.LIGHTNING;
            GetComponent<PlayerCombat>().projectileType = currentAttackType;
        }
        else
        {
            currentAttackType = AttackTypes.FIRE;
            GetComponent<PlayerCombat>().projectileType = currentAttackType;
        }

        // If in combat mode, enable the right element to display on Riona's hand
        if (isInCombatMode)
        {
            foreach (var attackGameObject in AttackType)
            {
                attackGameObject.SetActive(false);
            }

            AttackType[(int)currentAttackType].gameObject.SetActive(true);
        }

        // Change crosshair color
        crosshair.gameObject.GetComponent<Image>().color = AttackTypeColors[(int)currentAttackType];

        return currentAttackType;
    }

    /// <summary>
    /// Use potion function
    /// </summary>
    public void UsePotion(PlayerBehaviour playerBehaviour)
    {
        //Debug.Log("Usage potion");
       
        playerBehaviour.UpdateHP(10);
    }


    
}
