//----------------------------------------------------------------------------------
///   Riona the Empire Saviour
///   Objectives.cs
///   Author            : Geek’s Garage
///   Last Modified     : 2022/02/04
///   Description       : Objectives Manager to keep track of objectives
///   Revision History  : 1st ed. Completes Objective and immediately checks stats
///                     if all objectives are completed
///----------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Objectives : MonoBehaviour
{
    [SerializeField] 
    private int ObjectiveNumber = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerBehaviour player = other.GetComponent<PlayerBehaviour>();

            // Complete the objective
            player.CompleteObjective(ObjectiveNumber);

            // Get objective stats of Total Objectives
            if (player.GetObjectiveStats((int)ObjectivesEnum.TotalObjectives))
            {
                SceneManager.LoadScene(3);
            }
        }
    }
}