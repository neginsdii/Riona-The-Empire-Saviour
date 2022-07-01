///----------------------------------------------------------------------------------
///   Riona the Empire Saviour
///   FireHazard.cs
///   Author            : Geek’s Garage
///   Last Modified     : 2022/04/07
///   Description       : Fire Hazard script
///   Revision History  : 2nd ed. Commented out Debug.Log 
///----------------------------------------------------------------------------------


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireHazard : MonoBehaviour
{
    [SerializeField]
    private int damageValue;
    [SerializeField]
    private float damageDelay;
    [Range(0.5f, 2.0f)]
    [SerializeField]
    private float maxDamageTime;

    private AudioSource audioSource;
    [SerializeField]
    private List<AudioClip> audioClips;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        // play sound here
        AudioManager.GetInstance().PlaySFX(audioSource, audioClips[0], 0.1f, false);
       
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Player burning");
            PlayerBehaviour playerBehaviour = other.gameObject.GetComponent<PlayerBehaviour>();

            if (damageDelay > 0)
            {
                damageDelay -= Time.deltaTime;
            }
            else
            {
                playerBehaviour.UpdateHP(-damageValue);
                damageDelay = maxDamageTime;
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        audioSource.loop = false;
        audioSource.Stop();
    }
}
