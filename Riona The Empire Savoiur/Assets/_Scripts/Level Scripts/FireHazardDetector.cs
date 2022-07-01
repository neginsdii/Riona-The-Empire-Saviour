///----------------------------------------------------------------------------------
///   Riona the Empire Saviour
///   FireHazardDetector.cs
///   Author            : Geek’s Garage
///   Last Modified     : 2022/04/07
///   Description       : Fire Hazard Detector script used to activate fire particles once player is in range, optimization script
///   Revision History  : 1st ed. Particles will play when player enters the trigger volume
///----------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireHazardDetector : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private ParticleSystem pSystem;
    [SerializeField] private SphereCollider colliderDetector;


    void Start()
    {
        pSystem = GetComponent<ParticleSystem>();
        colliderDetector = GetComponent<SphereCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovement>() != null && !pSystem.isPlaying)
        {
            pSystem.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerMovement>() != null)
        {
            pSystem.Stop();
        }
    }
}
