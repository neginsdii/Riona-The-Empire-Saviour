///----------------------------------------------------------------------------------
///   Riona the Empire Saviour
///   DeathPlane.cs
///   Author            : Geek’s Garage
///   Last Modified     : 2022/02/04
///   Description       : Death plane script
///   Revision History  : 1st ed. Death plane scripts directly kills the player
///----------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlane : MonoBehaviour
{
    /// <summary>
    /// If player has collided, reduce HP
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerBehaviour>().UpdateHP(-500);
        }
    }
}
