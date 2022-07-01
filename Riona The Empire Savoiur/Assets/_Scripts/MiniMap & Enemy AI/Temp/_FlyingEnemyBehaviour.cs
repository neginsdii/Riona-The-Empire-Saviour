/////----------------------------------------------------------------------------------
/////   Riona the Empire Saviour
/////   FlyingEnemyBehaviour.cs
/////   Author            : Geek’s Garage
/////   Last Modified     : 2022/02/15
/////   Description       : Added Enemy AI using LOS and animations
/////   Revision History  : 1st ed. Changed the FlyingEnemy with LOS  
/////-----------------------------------------------------------------------------------

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.AI;

//public class FlyingEnemyBehaviour : EnemyBehaviour
//{
//    [Header("LOS")]
//    public bool HasLOS;
//    //public GameObject player;

//    private NavMeshAgent navMeshAgent;

//    //[SerializeField]
//    //public Transform[] PatrolPoints;

//    [SerializeField]
//    public Vector3[] PatrolPoints;
//    //public Vector3[] PatrolPoints;
//    private int destPoint = 0;

//    // Animation
//    [Header("Animation")]
//    private Animator FlyingAnimator;

//    int TotalPatrolPoints = 2;

//    // Start is called before the first frame update
//    void Start()
//    {
//        navMeshAgent = GetComponent<NavMeshAgent>();

//        FlyingAnimator = GetComponent<Animator>();

       
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (HasLOS)                                                                                 //if Enemy can see the Riona
//        {
//            navMeshAgent.SetDestination(player.transform.position);                                //Set the Destionation to Riona's location
//        }
//        else if(!HasLOS)
//        {
//            GotoNextPoint();                                                                        //Change to Next patrol Point
//        }
//    }

//    void GotoNextPoint()
//    {
//        //Returns if no points have been set up
//        if (PatrolPoints.Length == 0)
//            return;

//        //// Set the agent to go to the currently selected destination.
//        //navMeshAgent.destination = PatrolPoints[destPoint].position;
//        navMeshAgent.destination = new Vector3(PatrolPoints[destPoint].x, PatrolPoints[destPoint].y, PatrolPoints[destPoint].z);


//        //// Choose the next point in the array as the destination,
//        //// cycling to the start if necessary.
//        destPoint = (destPoint + 1) % PatrolPoints.Length;
//    }
//    private void OnDrawGizmos()
//    {
//        // Draws a blue line from this transform to the target
//        Gizmos.color = Color.blue;
//        foreach (Vector3 Points in PatrolPoints)
//        {
//            Gizmos.DrawSphere(Points, 1.5f);
//        }
//    }

//    /// <summary>
//    /// check it has LOS with Player or not
//    /// </summary>
//    /// <param name="other"></param>
//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.gameObject.CompareTag("Player"))
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

//}