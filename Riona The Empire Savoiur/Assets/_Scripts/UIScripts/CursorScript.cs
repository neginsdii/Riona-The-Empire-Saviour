///----------------------------------------------------------------------------------
///   Riona the Empire Saviour
///   CursorScript.cs
///   Author            : Geek’s Garage
///   Last Modified     : 2022/03/10
///   Description       : Cursor Script adjusts cursor
///   Revision History  : 3rd ed. Cursor modifications commented as this build will
///                     be for mobile   - code optimization       
///----------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorScript : MonoBehaviour
{

    [SerializeField] 
    private bool CursorVisiblity;

    public CursorScript _instance;
    

    // Start is called before the first frame update
    void Awake()
    {
        _instance = this;
        //CursorVisible(CursorVisiblity);
        Time.timeScale = 1;
    }

    //private void Start()
    //{
    //    CursorVisible(CursorVisiblity);
    //}


    /// <summary>
    /// Public cursor visible function
    /// </summary>
    /// <param name="visible"></param>
    //public void CursorVisible(bool visible)
    //{
    //    //if (visible)
    //    //{
    //    //    Cursor.lockState = CursorLockMode.None;
    //    //    Cursor.visible = true;
    //    //}
    //    //else
    //    //{
    //    //    Cursor.lockState = CursorLockMode.Locked;
    //    //    Cursor.visible = false;
    //    //}
    //}
}
