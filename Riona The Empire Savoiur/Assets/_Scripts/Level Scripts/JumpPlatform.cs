///-----------------------------
///     Riona the Empire Saviour
///     JumpPlatform.cs
///     Author          : Geek's Garage
///     Last Modified   : 2022/04/07
///     Description     : Script for making the player bounce up
///     Revision History: 3rd ed. Removed the Empty Update function 
/// ----------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlatform : MonoBehaviour
{
    [Range(10, 100), SerializeField]
    private float bounceForce;

    private AudioSource audioSource;
    [SerializeField]
    private List<AudioClip> audioClips;

    private GameObject go;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        go = other.gameObject;
        PlayerMovement player = go.GetComponent<PlayerMovement>();
        if (player != null)
        {
            //Debug.Log("Enter");
            AudioManager.GetInstance().PlaySFX(audioSource, audioClips[0], 0.1f, false);
            player.velocity.y = bounceForce;
        }
    }
}
