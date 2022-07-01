///----------------------------------------------------------------------------------
///   Riona the Empire Saviour
///   LOS.cs
///   Author            : Geek’s Garage
///   Last Modified     : 2022/04/07
///   Description       : Added Enemy LOS 
///   Revision History  : 2nd ed. Commented out Debug.Log & Removed the Empty Update function
///----------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(BoxCollider))]
public class LOS : MonoBehaviour
{
    [Header("Detection properties")]
    public bool reachedToTarget = false;
    //public BoxCollider boxCollider;
    
    
    public PlayerBehaviour player;
    
    
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerBehaviour>();
        //boxCollider = GetComponent<BoxCollider>();
    }


    /// <summary>
    /// Example script for LOS, if we want to use it only to check the Enemy LOS with different script
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Debug.Log("reachedToTarget");
            reachedToTarget = true;
        }

    }


}
