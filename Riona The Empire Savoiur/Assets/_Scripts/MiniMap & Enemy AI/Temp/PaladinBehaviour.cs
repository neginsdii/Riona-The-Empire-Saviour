/////----------------------------------------------------------------------------------
/////   Riona the Empire Saviour
/////   PaladinBehaviour.cs
/////   Author            : Geek�s Garage
/////   Last Modified     : 2022/02/14
/////   Description       : Added Enemy AI using LOS and animations
/////   Revision History  : 1st ed. Changed the Paladin Animation 
/////----------------------------------------------------------------------------------


//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.AI;

///// <summary>
///// this is the Enum for the Animation States
///// </summary>
//public enum PaladinState
//{
//    IDLE,
//    WALK,
//    ATTACK
//}

//public class PaladinBehaviour : EnemyBehaviour
//{
//    [Header("LOS")] 
//    public bool HasLOS;
//    //public GameObject player;

//    private NavMeshAgent navMeshAgent;

//    // Animation
//    [Header("Animation")]
//    private Animator PaladinAnimator;

//    [SerializeField]
//    public Vector3[] PatrolPoints;
//    //public Transform[] PatrolPoints;
//    private int destPoint = 0;

//    public PlayerBehaviour Riona;
//    public int damage = 10;

//    [SerializeField]
//    private List<AudioClip> paladinAudioClips;

//    // Start is called before the first frame update
//    void Start()
//    {
//        audioSource = GetComponent<AudioSource>();
//        navMeshAgent = GetComponent<NavMeshAgent>();

//        PaladinAnimator = GetComponent<Animator>();

//        Riona = FindObjectOfType<PlayerBehaviour>();
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (HasLOS)                                                                                 //if Enemy can see the Riona
//        {
//            navMeshAgent.SetDestination(player.transform.position);                                //Set the Destionation to Riona's location
//            PaladinAnimator.SetInteger("AnimState", (int)PaladinState.WALK);                       //Walk towards Riona

//            if (Vector3.Distance(transform.position, player.transform.position) < 1.0f)              //if it is too close to Riona
//            {
//                PaladinAnimator.SetInteger("AnimState", (int)PaladinState.ATTACK);                 //Change to Attack Animation  
//            }
//        }
//        else if (!HasLOS)
//        {
//            GotoNextPoint();
//            PaladinAnimator.SetInteger("AnimState", (int)PaladinState.WALK);                       //Walk towards Riona
//        }
//        //else
//        //{
//        //    PaladinAnimator.SetInteger("AnimState", (int)PaladinState.IDLE);                        //Idle Position when no LOS
//        //}    
//    }

//    private void OnDrawGizmos()
//    {
//        // Draws a blue line from this transform to the target
//        Gizmos.color = Color.blue;
//        foreach(Vector3 Points in PatrolPoints)
//        {
//            Gizmos.DrawSphere(Points,1.5f);
//        }
//    }

//    void GotoNextPoint()
//    {
//        //Returns if no points have been set up
//        if (PatrolPoints.Length == 0)
//            return;

//        //// Set the agent to go to the currently selected destination.
//        navMeshAgent.destination = new Vector3(PatrolPoints[destPoint].x, PatrolPoints[destPoint].y, PatrolPoints[destPoint].z);

//        //// Choose the next point in the array as the destination,
//        //// cycling to the start if necessary.
//        destPoint = (destPoint + 1) % PatrolPoints.Length;
//    }


//    /// <summary>
//    /// check it has LOS with Player or not
//    /// </summary>
//    /// <param name="other"></param>
//    private void OnTriggerEnter(Collider other)
//    {
//        if(other.gameObject.CompareTag("Player"))
//        {
//            HasLOS = true;
//            player = other.transform.gameObject;
//        }
//    }


//    /// <summary>
//    /// Check if Player is out of the visible area
//    /// </summary>
//    /// <param name="other"></param>
//    private void OnTriggerExit(Collider other)
//    {
//        if (other.gameObject.CompareTag("Player"))
//        {
//            HasLOS = false;
//        }
//    }

//    ///// <summary>
//    ///// Attack riona
//    ///// </summary>
//    //public void AttackRiona()
//    //{
//    //    AudioManager.GetInstance().PlaySFX(audioSource, paladinAudioClips[0], 0.1f, false);
//    //    Riona.UpdateHP(-damage);
//    //}
//}