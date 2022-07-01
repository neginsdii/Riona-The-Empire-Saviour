///----------------------------------------------------------------------------------
///   Riona the Empire Saviour
///   RockProjectile.cs
///   Author            : Geek’s Garage
///   Last Modified     : 2022/03/12
///   Description       : This script dictates the behaviour of the rock projectile prefab when walking over the trap platform hazard
///   Revision History  : 1st. Subtract HP value of player based on damage value assigned to the rock
///                       2nd. Play Particle effect regardless of whether or not it hits the player
///
///----------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockProjectile : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rb;
    [SerializeField] private int damageValue;
    [SerializeField] private ParticleSystem rockParticles;

    private AudioSource audioSource;
    [SerializeField]
    private List<AudioClip> audioClips;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerBehaviour>() != null)
        {

            collision.gameObject.GetComponent<PlayerBehaviour>().UpdateHP(-damageValue);

        }
        if (rockParticles != null)
        {
            rockParticles.gameObject.SetActive(true);
            rockParticles.Play();
        }
        StartCoroutine(DelayDespawn());
    }

    IEnumerator DelayDespawn()
    {
        AudioManager.GetInstance().PlaySFX(audioSource, audioClips[0], 0.1f, false);
        yield return new WaitForSeconds(0.85f);
        this.gameObject.SetActive(false);
    }
}
