//----------------------------------------------------------------------------------
///   Riona the Empire Saviour
///   Paladin_Behaviour.cs
///   Author            : Geek�s Garage
///   Last Modified     : 2022/04/07
///   Description       : A Ground enemy type, including the animationstates.
///   Revision History  : 8th ed. Commented out Debug.Log
///----------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

/// <summary>
/// this is the Enum for the Animation States
/// </summary>
[System.Serializable]
public enum PaladinState
{
    IDLE,
    WALK,
    ATTACK
}


public class Paladin_Behaviour : MonoBehaviour
{
   
    public NavMeshAgent navmeshagent;
    
    public Transform PlayerTransform;
    public PlayerBehaviour RionaPlayerBehaviour;
    public GameObject Riona;

    public LayerMask GroundLayer, PlayerLayer;
    
    [SerializeField]
  
    public Vector3 walkPoint;
    public bool walkPointSet;
    public float walkPointRange;

    public float TimeBetweenAttaks;
    bool alreadyAttacked;

    public float sightRange, attackRange;
    public bool playerInsightRange, playerInattackRange;
 
    // Animation
    [Header("Animation")]
    private Animator PaladinAnimator;

    protected AudioSource audioSource;
  
    public List<AudioClip> audioClips;

    public int damage = 10;

    public GameObject pickup;

    [SerializeField]
    private Slider Enemy_HealthBar_ScreenSpace_Slider;

    [Header("Enemy Health Properties")]
    [Range(0, 100)]
    public int Enemy_currentHealth = 100;

    [Range(1, 100)]
    public int Enemy_maximumHealth = 100;

    [SerializeField]
    public int Score = 10;

    [Header("Set Patrol Points")]
    [Range(-1000, 1000)]
    public int First_RandomX_Value = 6;
    [Range(-1000, 1000)]
    public int Second_RandomX_Value = -28;
    [Range(-1000, 1000)]
    public int First_RandomZ_Value = -7;
    [Range(-1000, 1000)]
    public int Second_RandomZ_Value = -55;

    public Vector3 Initial_Start_Position = Vector3.zero;

    public Vector3 MinBound, MaxBound;

    private AttackTypes playerAttackType = AttackTypes.FIRE;

    public List<float> attackPercentOnType;

    private void Awake()
    {
        Riona = GameObject.Find("Riona").gameObject;
        PlayerTransform = Riona.GetComponent<Transform>();
    
        RionaPlayerBehaviour = Riona.GetComponent<PlayerBehaviour>();
        navmeshagent = GetComponent<NavMeshAgent>();
        PaladinAnimator = GetComponent<Animator>();
      
        audioSource = GetComponent<AudioSource>();
        Enemy_HealthBar_ScreenSpace_Slider = GetComponentInChildren<Slider>(); 
        Enemy_currentHealth = Enemy_maximumHealth;
        Initial_Start_Position = transform.position;
        MinBound = Initial_Start_Position - new Vector3(First_RandomX_Value, transform.position.y, First_RandomZ_Value);
        MaxBound = Initial_Start_Position + new Vector3(Second_RandomX_Value, transform.position.y, Second_RandomZ_Value);
    }


    // Start is called before the first frame update
    void Start()
    {
        PaladinAnimator.SetInteger("AnimState", (int)PaladinState.IDLE);
    }

    // Update is called once per frame
    void Update()
    {
        //-----------------------------
        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(10, AttackTypes.FIRE);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reset();
        }

        //-----------------------------


        
        playerInsightRange = Physics.CheckSphere(transform.position, sightRange, PlayerLayer);
        playerInattackRange = Physics.CheckSphere(transform.position, attackRange, PlayerLayer);

        if (!playerInsightRange && !playerInattackRange) Patrolling();
        if (playerInsightRange && !playerInattackRange) chasePlayer();
        if (playerInattackRange && playerInsightRange) AttackPlayer();
        if (Enemy_currentHealth <= 0 && (playerInattackRange || playerInsightRange || !playerInsightRange || !playerInattackRange))
        {
            navmeshagent.ResetPath();         
        }
    }


    public virtual void TakeDamage(int damage, AttackTypes attackType)
    {
        int elementTypeDamage = damage;

        elementTypeDamage = (int)(damage * attackPercentOnType[(int)attackType]);

        //Debug.Log("Paladin took " + elementTypeDamage + " elemental damage");

        Enemy_HealthBar_ScreenSpace_Slider.value -= elementTypeDamage;
        Enemy_currentHealth -= elementTypeDamage;

        AudioManager.GetInstance().PlaySFX(audioSource, audioClips[0], 0.1f, false);

        if (Enemy_currentHealth <= 0)
        {
            Enemy_HealthBar_ScreenSpace_Slider.value = 0;
            Enemy_currentHealth = 0;
           
            PaladinAnimator.SetTrigger("Death");
          
            GetComponent<CapsuleCollider>().enabled = false;
            //Debug.Log("Play Death Animation");
            GameManager.GetInstance().EnemyManager_KillEnemy(Score);
            Enemy_HealthBar_ScreenSpace_Slider.gameObject.SetActive(false);
            var pickupSpawn = Random.Range(0, 10);

            if (pickupSpawn < 6)
            {
                Rigidbody rb = Instantiate(pickup, new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z), Quaternion.identity).GetComponent<Rigidbody>();
                //Debug.Log("Pickup is available : " + pickupSpawn);
            }
        }
        else
        {           
            PaladinAnimator.SetTrigger("TakeDamage");
        }
     
    }

    public virtual void Reset()
    {
        Enemy_HealthBar_ScreenSpace_Slider.value = Enemy_maximumHealth;
        Enemy_currentHealth = Enemy_maximumHealth;
    }

    private void Patrolling()
    {
        
        if (!walkPointSet)
        {
            PaladinAnimator.SetInteger("AnimState", (int)PaladinState.IDLE);                        //IDLE

            SearchWalkPoint();
        }

        if (walkPointSet)
        {           
            navmeshagent.SetDestination(walkPoint);
            PaladinAnimator.SetInteger("AnimState", (int)PaladinState.WALK);                       //WALK 

        }

        Vector3 Distance = transform.position - walkPoint;

        if (Distance.magnitude < 1.0f )
        {
            walkPointSet = false;
            PaladinAnimator.SetInteger("AnimState", (int)PaladinState.IDLE);                        //IDLE
        }
    }

    private void SearchWalkPoint()
    {
        float RandomX = Random.Range(First_RandomX_Value,Second_RandomX_Value);
        float RandomZ = Random.Range(First_RandomZ_Value, Second_RandomZ_Value);
        walkPoint = new Vector3(RandomX, transform.position.y,RandomZ);

        
        walkPointSet = true;
    
        if (walkPointSet)
        {
            navmeshagent.SetDestination(walkPoint);       
            PaladinAnimator.SetInteger("AnimState", (int)PaladinState.WALK);                       //WALK            
        }
    }

    private void chasePlayer()
    {
        navmeshagent.SetDestination(Riona.transform.position);
        PaladinAnimator.SetInteger("AnimState", (int)PaladinState.WALK);                       //WALK
    }

    private void AttackPlayer()
    {
        navmeshagent.SetDestination(transform.position);

        transform.LookAt(PlayerTransform);
        PaladinAnimator.SetInteger("AnimState", (int)PaladinState.ATTACK);                       //ATTACK
    }

    /// <summary>
    /// Attack riona
    /// </summary>
    public void AttackRiona()
    {
        AudioManager.GetInstance().PlaySFX(audioSource, audioClips[0], 0.1f, false);
        RionaPlayerBehaviour.UpdateHP(-damage);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(new Vector3(First_RandomX_Value, transform.position.y, First_RandomZ_Value),0.75f);
        Gizmos.DrawWireSphere(new Vector3(Second_RandomX_Value, transform.position.y, Second_RandomZ_Value), 0.75f);
    }
}
