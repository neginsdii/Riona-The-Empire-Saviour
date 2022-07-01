///----------------------------------------------------------------------------------
///   Riona the Empire Saviour
///   CheckpointTrigger.cs
///   Author            : Geek’s Garage
///   Last Modified     : 2022/03/31
///   Description       : Checkpoint Trigger will be activated when Riona collides
///   Revision History  : 3rd ed. Material change works
///----------------------------------------------------------------------------------


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{

    private GameManager _gameManager;

    public MeshRenderer ringMeshRenderer;

    private bool isCheckpointTriggered = false;

    public Material CheckpointGreenMaterial;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.FindObjectOfType<GameManager>();
        //MeshRenderer[] TempMeshRenderer  = GetComponentsInChildren<MeshRenderer>();

        // Since there is only one mesh renderer, this will assign that to the required var
       //foreach (var meshRenderer in TempMeshRenderer)
       //{
       //    ringMeshRenderer = meshRenderer;
       //}

        isCheckpointTriggered = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!isCheckpointTriggered)
            {
                isCheckpointTriggered = true;

                _gameManager.SetCurrentCheckPoint(GetComponentInParent<Checkpoint>().RespawnPointVector);
                other.gameObject.GetComponent<PlayerBehaviour>().SaveOnCheckPointTrigger();
                StartCoroutine(DisableCheckpoint());
            }
            
        }
    }

    /// <summary>
    /// Co-routine for disabling checkpoint.
    /// Before that visually give feedback to player
    /// </summary>
    /// <returns></returns>
    IEnumerator DisableCheckpoint()
    {
        ringMeshRenderer.material = CheckpointGreenMaterial;
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}