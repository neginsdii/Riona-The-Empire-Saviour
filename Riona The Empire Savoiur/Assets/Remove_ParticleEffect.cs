//----------------------------------------------------------------------------------
///   Riona the Empire Saviour
///   Remove_ParticleEffect.cs
///   Author            : Geek�s Garage
///   Last Modified     : 2022/04/04
///   Description       : After some amount of time, it will destroy.
///   Revision History  : 1st ed. added the behaviour to destroy after sometime
///----------------------------------------------------------------------------------


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Remove_ParticleEffect : MonoBehaviour
{
    [Header("Add DestroyTime")]
    public int DestroyTime;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, DestroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
