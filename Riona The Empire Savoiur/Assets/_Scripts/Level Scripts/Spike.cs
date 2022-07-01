///-----------------------------
///     Riona the Empire Saviour
///     Spike.cs
///     Author          : Geek's Garage
///     Last Modified   : 2022/3/12
///     Description     : Script for the spike hazard
///     Revision History: 1st ed. OnTriggerEnter behaviour
///                       2nd ed. Play Sound Effect - code optimization
/// ----------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private List<AudioClip> audioClips;
    [SerializeField] private int damageValue;

    private PlayerBehaviour playerBehaviour;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("OnTriggerOfSpike");

        if (other.gameObject.CompareTag("Player"))
        {
            AudioManager.GetInstance().PlaySFX(audioSource, audioClips[0], 0.5f, false);     
             playerBehaviour = other.gameObject.GetComponent<PlayerBehaviour>();
            playerBehaviour.UpdateHP(-damageValue);
            //Debug.Log("Subtract health here");
        }
        
    }
}
