///-----------------------------
///     Riona the Empire Saviour
///     ShrinkingPlatform.cs
///     Author          : Geek's Garage
///     Last Modified   : 2022/1/30
///     Description     : Script for making a platform shrink when the player collides with it
///     Revision History: 1st ed. Placed logic so that when object with the player component collides
///                       the box will shrink, then expand when a player leaves the platform
/// ----------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkingPlatform : MonoBehaviour
{
    [Header("Shrinking Time Monitors")]
    [SerializeField] private float shrinkTimeElapsed;
    [Range(0.1f, 10.0f)]
    [SerializeField] private float shrinkingTime; // Must alter in the editor, total time it takes to fully shrink the platform, as well as the max value for scaling purposes
    [SerializeField] private Vector3 platformPosition; // Platform position

    [Header("Platform State")]
    [SerializeField] private bool isPlayerOn;
    [SerializeField] private bool isExpanding;

    // scaling properties
    private Vector3 tempScale;          // scale that will reference the transform's own local scale, will scale down/up over time given contextual action
    private Vector4 originalScale;   // keep a reference to the box' collider scale to change size as well
    float scale;                        // scaling factor (stays between 0 and 1)
    private BoxCollider boxCollider;

    private AudioSource audioSource;
    [SerializeField] private List<AudioClip> audioClips;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        audioSource = GetComponent<AudioSource>();
        platformPosition = transform.position;

        originalScale = boxCollider.size;

        isPlayerOn = false;
        isExpanding = false;
        shrinkTimeElapsed = shrinkingTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlayerOn)
        {
            if (isExpanding)
            {
                Expand();
            }
        } else
        {
            Shrink();
        }
    }

    void Expand()
    {
        shrinkTimeElapsed += Time.deltaTime;
        scale = shrinkTimeElapsed / shrinkingTime;

        tempScale = transform.localScale;
        tempScale.x = scale;
        tempScale.y = scale;
        tempScale.z = scale;
        transform.localScale = tempScale;


        // when we return to the original scale, we're done expanding, change the if statement
        if (scale >= 1.0f)
        {
            scale = 1.0f;
            isExpanding = false;
        }
    }

    void Shrink()
    {
        shrinkTimeElapsed -= Time.deltaTime;
        scale = shrinkTimeElapsed / shrinkingTime;

        tempScale = transform.localScale;
        tempScale.x = scale;
        tempScale.y = scale;
        tempScale.z = scale;
        transform.localScale = tempScale;


    }

   // private void OnCollisionEnter(Collision collision)
   // {
   //     // CHANGE THIS TO THE FINAL PLAYER CHARACTER
   //
   //     Debug.Log("Collision");
   //
   //     if (collision.gameObject.CompareTag("Player"))
   //     {
   //         isPlayerOn = true;
   //         isExpanding = false;
   //     }
   // }
   //
   // private void OnCollisionExit(Collision collision)
   // {
   //     isPlayerOn = false;
   //     isExpanding = true;
   // }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AudioManager.GetInstance().PlaySFX(audioSource, audioClips[0], 0.1f, false);
            isPlayerOn = true;
            isExpanding = false;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        isPlayerOn = false;
        isExpanding = true;

        audioSource.loop = false;
        audioSource.Stop();
    }
}
