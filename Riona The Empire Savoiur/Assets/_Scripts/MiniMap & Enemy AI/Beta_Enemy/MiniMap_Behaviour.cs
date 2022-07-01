///----------------------------------------------------------------------------------
///   Riona the Empire Saviour
///   MiniMap_Behaviour.cs
///   Author            : Geek�s Garage
///   Last Modified     : 2022/04/7
///   Description       : MiniMap Controller
///   Revision History  : 2nd. ed. Added optimizations - Code optimization for minimap
///----------------------------------------------------------------------------------


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap_Behaviour : MonoBehaviour
{
    // Optimization -> Resource management
    Vector3 eulerAngles = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        eulerAngles.x = transform.eulerAngles.x;
        transform.eulerAngles = eulerAngles;
    }
}