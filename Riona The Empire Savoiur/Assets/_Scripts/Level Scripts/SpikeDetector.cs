///----------------------------------------------------------------------------------
///   Riona the Empire Saviour
///   SpikeDetector.cs
///   Author            : Geek’s Garage
///   Last Modified     : 2022/04/07
///   Description       : Spike Detector script used to activate mesh renderer once player is in range, optimization script
///   Revision History  : 1st ed. meshRenderer activates when player enters the trigger volume
///----------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeDetector : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovement>() != null)
        {
            meshRenderer.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerMovement>() != null)
        {
            meshRenderer.enabled = false;
        }
    }
}
