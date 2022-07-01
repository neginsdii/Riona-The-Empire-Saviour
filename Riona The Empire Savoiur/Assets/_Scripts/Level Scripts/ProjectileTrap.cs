///----------------------------------------------------------------------------------
///   Riona the Empire Saviour
///   ProjectileTrap.cs
///   Author            : Geek’s Garage
///   Last Modified     : 2022/03/11
///   Description       : This script is active when the player hits the collider of the trap platform
///   Revision History  : 1st. Spawns a rock and the warning reticle when the player interacts with the platform
///
///----------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTrap : MonoBehaviour
{
    [SerializeField] private GameObject projectileToSpawn;
    [SerializeField] private GameObject telegraphReticle;
    [SerializeField] private Transform projectileSpawnTransform;

    private AudioSource audioSource;
    [SerializeField]
    private List<AudioClip> audioClips;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        projectileToSpawn.SetActive(false);
        telegraphReticle.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!projectileToSpawn.activeInHierarchy)
        {
            telegraphReticle.SetActive(false);
        }
    }

    void DropProjectile()
    {
        if (projectileToSpawn != null)
        {
            projectileToSpawn.transform.position = projectileSpawnTransform.position;
            projectileToSpawn.GetComponent<Rigidbody>().velocity = Vector3.zero;
            projectileToSpawn.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerBehaviour>() != null)
        {
            AudioManager.GetInstance().PlaySFX(audioSource, audioClips[0], 0.1f, false);

            telegraphReticle.SetActive(true);
            DropProjectile();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        telegraphReticle.SetActive(false);
    }

}
