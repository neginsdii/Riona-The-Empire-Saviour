///----------------------------------------------------------------------------------
///   Riona the Empire Saviour
///   EnemyHealthBarWorldSpaceController.cs
///   Author            : Geek’s Garage
///   Last Modified     : 2022/02/16
///   Description       : Added EnemyHealthBarWorldSpaceController
///   Revision History  : 1st ed. Created the EnemyHealthBar and attached the script  
///-----------------------------------------------------------------------------------



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBarWorldSpaceController : MonoBehaviour
{
    public Transform PlayerCamera;
    // Start is called before the first frame update
    void Start()
    {
        PlayerCamera = GameObject.Find("Main Camera").transform;
    }


    // Update is called once per frame
    void LateUpdate()
    {
        //Billboard healthbar
        transform.LookAt(transform.position + PlayerCamera.forward);
    }
}
