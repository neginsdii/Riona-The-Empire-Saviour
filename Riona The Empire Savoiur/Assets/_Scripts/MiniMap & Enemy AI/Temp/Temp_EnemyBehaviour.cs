/////----------------------------------------------------------------------------------
/////   Riona the Empire Saviour
/////   EnemyBehaviour.cs
/////   Author            : Geek’s Garage
/////   Last Modified     : 2022/01/28
/////   Description       : Added Enemy AI using unity Navmesh Agent
/////   Revision History  : 1st ed. Window > Ai > Navigation to integrate Navigation Mesh 
/////                     : 2nd ed. added the Stopping distance 
/////                     : 3rd ed. Check the collision with Player using Raycast,Added player Box collider(Trigger) 
/////----------------------------------------------------------------------------------


//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.AI;
//using UnityEngine.UI;


//public class TemEnemyBehaviour : MonoBehaviour
//{
//    //public NavMeshAgent navMeshAgent;
//    public GameObject player;

//    [SerializeField]
//    private Slider Enemy_HealthBar_ScreenSpace_Slider;

//    [Header("Enemy Health Properties")]
//    [Range(0, 100)]
//    public int Enemy_currentHealth = 100;

//    [Range(1, 100)]
//    public int Enemy_maximumHealth = 100;

//    [SerializeField]
//    public int Score = 10;

//    protected AudioSource audioSource;
//    [SerializeField]
//    protected List<AudioClip> audioClips;

//    //// Animation
//    //[Header("Animation")]
//    //private Animator _enemyAnimator;

//    //public bool chasePlayer = false;

//    // Start is called before the first frame update
//    void Start()
//    {
//        audioSource = GetComponent<AudioSource>();
//        //navMeshAgent = GetComponent<NavMeshAgent>();
//        //player = FindObjectOfType<PlayerBehaviour>();
//        Enemy_HealthBar_ScreenSpace_Slider = GetComponentInChildren<Slider>(); //GetComponent<Slider>();
//        Enemy_currentHealth = Enemy_maximumHealth;
//        //_enemyAnimator = GetComponent<Animator>();

//    }


//    // Update is called once per frame
//    void Update()
//    {
//        //if (Input.GetKeyDown(KeyCode.K))
//        //{
//        //    TakeDamage(10);
//        //}
//        //if (Input.GetKeyDown(KeyCode.R))
//        //{
//        //    Reset();
//        //}
//        // navMeshAgent.SetDestination(player.transform.position);

//        //    if (Input.GetKeyDown(KeyCode.P))
//        //    {
//        //        chasePlayer = !chasePlayer;
//        //    }
//        //    if (chasePlayer)
//        //    {
//        //        navMeshAgent.isStopped = false;
//        //        navMeshAgent.SetDestination(player.transform.position);
//        //        _enemyAnimator.SetBool("IsWalking", true);
//        //    }
//        //    else
//        //    {
//        //        //navMeshAgent.SetDestination(transform.position);
//        //        navMeshAgent.isStopped = true;
//        //        _enemyAnimator.SetBool("IsWalking", false);
//        //    }

//        //    RaycastHit hit;

//        //if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, 1.5f))
//        //{
//        //    //if (hit.transform.gameObject.tag == "Player")
//        //    //{
//        //    //    //Debug.Log("Collide with Player");
//        //    //    _enemyAnimator.SetBool("IsWalking", false);
//        //    //    //navMeshAgent.isStopped = false;
//        //    //    //_enemyAnimator.SetBool("IsAttacking", true);
//        //    //}
//        //    //else
//        //    //{
//        //    //    //_enemyAnimator.SetBool("IsAttacking", false);
//        //    //    _enemyAnimator.SetBool("IsWalking", true);
//        //    //     //navMeshAgent.isStopped = true;
//        //    //    //Debug.Log(Collide with player);
//        //    //}
//        //}
//    }

//    //public virtual void TakeDamage(int damage)
//    //{
//    //    Enemy_HealthBar_ScreenSpace_Slider.value -= damage;
//    //    Enemy_currentHealth -= damage;

//    //    AudioManager.GetInstance().PlaySFX(audioSource, audioClips[0], 0.1f, false);

//    //    if (Enemy_currentHealth <= 0)
//    //    {
//    //        Enemy_HealthBar_ScreenSpace_Slider.value = 0;
//    //        Enemy_currentHealth = 0;
//    //        GameManager.GetInstance().EnemyManager_KillEnemy(Score);
//    //        Destroy(gameObject);
//    //    }
//    //}

//    //public virtual void Reset()
//    //{
//    //    Enemy_HealthBar_ScreenSpace_Slider.value = Enemy_maximumHealth;
//    //    Enemy_currentHealth = Enemy_maximumHealth;
//    //}
//}
