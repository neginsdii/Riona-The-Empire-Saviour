///----------------------------------------------------------------------------------
///   Riona the Empire Saviour
///   Checkpoint.cs
///   Author            : Geek’s Garage
///   Last Modified     : 2022/02/04
///   Description       : Checkpoint script for storing the spawn points
///   Revision History  : 1st ed. Stores the world location of the character when
///                     colliding with the checkpoint
///----------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    // Check point parameters
    // Transform
    public Transform RespawnPoint;
    public Transform TriggerPoint;

    // Vector
    public Vector3 RespawnPointVector;
    public Vector3 TriggerPointVector;

    // Start is called before the first frame update
    void Start()
    {
        RespawnPointVector = RespawnPoint.position;
        TriggerPointVector = TriggerPoint.position;
    }
}