//----------------------------------------------------------------------------------
///   Riona the Empire Saviour
///   FlyingEnemy_Behaviour.cs
///   Author            : Geek�s Garage
///   Last Modified     : 2022/04/07
///   Description       : A Flying enemy type
///   Revision History  : 7th ed. Removed the Empty Start function
///                         Commented out Debug.Log
///                         Removed the Initialization of Smoke particle  when Phoenix dies
///----------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;



public class FlyingEnemy_Behaviour : MonoBehaviour
{
    public NavMeshAgent navmeshagent;

    public Transform PlayerTransform;
    public PlayerBehaviour RionaPlayerBehaviour;
    public GameObject Riona;

    public LayerMask GroundLayer, PlayerLayer;
    [Range(1, 100)]
    public int force = 32;
    [SerializeField]

    public Vector3 walkPoint;
    public bool walkPointSet;
    public float walkPointRange;

    public float TimeBetweenAttaks;
    bool alreadyAttacked;
    public GameObject projectile;
    public GameObject Lookat;

    public float sightRange, attackRange;
    public bool playerInsightRange, playerInattackRange;

    [Header("Animation")]
    private Animator PhoenixAnimator;

    protected AudioSource audioSource;

    public List<AudioClip> audioClips;

    public int damage = 10;

    [SerializeField]
    private Slider Enemy_HealthBar_ScreenSpace_Slider;

    [Header("Enemy Health Properties")]
    [Range(0, 100)]
    public int Enemy_currentHealth = 100;

    [Range(1, 100)]
    public int Enemy_maximumHealth = 100;

    [SerializeField]
    public int Score = 10;

    [Header("After Death")]
    public ParticleSystem Smoke_after_Phoenix_death;
    public GameObject pickup;

    [Header("Set Patrol Points")]
    [Range(-1000, 1000)]
    public int First_RandomX_Value = 6;
    [Range(-1000, 1000)]
    public int Second_RandomX_Value = -28;
    [Range(-1000, 1000)]
    public int First_RandomZ_Value = -7;
    [Range(-1000, 1000)]
    public int Second_RandomZ_Value = -55;

    public List<float> attackPercentOnType;

    private void Awake()
    {
        Riona = GameObject.Find("Riona").gameObject;
        PlayerTransform = Riona.GetComponent<Transform>();
        //PlayerTransform = GameObject.Find("Riona").transform;
        RionaPlayerBehaviour = Riona.GetComponent<PlayerBehaviour>();
        navmeshagent = GetComponent<NavMeshAgent>();
        PhoenixAnimator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        Enemy_HealthBar_ScreenSpace_Slider = GetComponentInChildren<Slider>(); //GetComponent<Slider>();
        Enemy_currentHealth = Enemy_maximumHealth;
    }

    // Update is called once per frame
    void Update()
    {
      
        //-----------------------------
        if (Input.GetKeyDown(KeyCode.L))
        {
            TakeDamage(10, AttackTypes.FIRE);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reset();
        }

        playerInsightRange = Physics.CheckSphere(transform.position, sightRange, PlayerLayer);
        playerInattackRange = Physics.CheckSphere(transform.position, attackRange, PlayerLayer);

        if (!playerInsightRange && !playerInattackRange) Patrolling();
        if (playerInsightRange && !playerInattackRange) chasePlayer();
        if (playerInattackRange && playerInsightRange) AttackPlayer();

        
    }

    public virtual void TakeDamage(int damage, AttackTypes attackType)
    {
        int elementTypeDamage = damage;

        elementTypeDamage = (int)(damage * attackPercentOnType[(int)attackType]);

        //Debug.Log("Phoenix took " + elementTypeDamage + " elemental damage");

        Enemy_HealthBar_ScreenSpace_Slider.value -= elementTypeDamage;
        Enemy_currentHealth -= elementTypeDamage;

        AudioManager.GetInstance().PlaySFX(audioSource, audioClips[0], 0.1f, false);

        if (Enemy_currentHealth <= 0)
        {
            Enemy_HealthBar_ScreenSpace_Slider.value = 0;
            Enemy_currentHealth = 0;
            GameManager.GetInstance().EnemyManager_KillEnemy(Score);
            var pickupSpawn = Random.Range(0, 10);
            Enemy_HealthBar_ScreenSpace_Slider.gameObject.SetActive(false);
            if (pickupSpawn < 8)
            {
                Rigidbody rb = Instantiate(pickup, new Vector3(transform.position.x, transform.position.y - 1.0f, transform.position.z), Quaternion.identity).GetComponent<Rigidbody>();
                //Debug.Log("Pickup is available :" + pickupSpawn);
            }
            Instantiate(Smoke_after_Phoenix_death, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
        else
        {
            PhoenixAnimator.SetTrigger("TakeDamage");
            //Debug.Log("Taking Damage");
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
            SearchWalkPoint();
        }
        if (walkPointSet)
        {
            navmeshagent.SetDestination(walkPoint);
        }

        Vector3 Distance = transform.position - walkPoint;

        if (Distance.magnitude < 1.0f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        float RandomX = Random.Range(First_RandomX_Value, Second_RandomX_Value);
        float RandomZ = Random.Range(First_RandomZ_Value, Second_RandomZ_Value);
        walkPoint = new Vector3( RandomX, this.transform.position.y, RandomZ);
        walkPointSet = true;
        if (walkPointSet)
        {
            navmeshagent.SetDestination(walkPoint);
        }
    }

    private void chasePlayer()
    {       
        navmeshagent.SetDestination(Riona.transform.position);
    }

    private void AttackPlayer()
    {
        navmeshagent.SetDestination(transform.position);

        transform.LookAt(PlayerTransform);


        if (!alreadyAttacked)
        {
            //Attack code here
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            AudioManager.GetInstance().PlaySFX(audioSource, audioClips[1], 0.1f, false);

            rb.AddForce(transform.forward * force, ForceMode.Impulse);
            rb.AddForce(transform.up * 4f, ForceMode.Impulse);

            //End of attack code
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), TimeBetweenAttaks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
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
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(new Vector3(First_RandomX_Value, transform.position.y, First_RandomZ_Value), 0.75f);
        Gizmos.DrawWireSphere(new Vector3(Second_RandomX_Value, transform.position.y, Second_RandomZ_Value), 0.75f);
    }
}
