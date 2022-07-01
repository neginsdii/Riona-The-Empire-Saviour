///----------------------------------------------------------------------------------
///   Riona the Empire Saviour
///   SetLayoutt.cs
///   Author            : Geek’s Garage
///   Last Modified     : 2022/03/08
///   Description       : This script is used to select a desired layout for the player and sets the position of onscreen controls accordingly in the beginning of the game. 
///   Revision History  : 1st. 
///                     
///----------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLayout : MonoBehaviour
{
    [Header("positions")]
    public GameObject joystickLeftPosition;
    public GameObject joystickRightPosition;
    public GameObject jumpLeftPosition;
    public GameObject jumpRightPosition;
    public GameObject attackJoyStickLeftPosition;
    public GameObject attackJoyStickRightPosition;
    public GameObject attackButtonLeftPosition;
    public GameObject attackButtonRightPosition;
    public GameObject combatLeftPosition;
    public GameObject combatRightPosition;
    public GameObject PowerLeftPosition;
    public GameObject PowertRightPosition;
    [Header("Onscreen Controls")]
    public GameObject joyStick;
    public GameObject jump;
    public GameObject attackJoystick;
    public GameObject attack;
    public GameObject combat;
    public GameObject power;
    void Start()
    {
        if(PlayerPrefs.HasKey("Layout"))
        {
           
            if (PlayerPrefs.GetString("Layout")=="Left")
            {
              
                joyStick.transform.position = joystickLeftPosition.transform.position;
                jump.transform.position = jumpLeftPosition.transform.position;
                combat.transform.position = combatLeftPosition.transform.position;
                attackJoystick.transform.position = attackJoyStickRightPosition.transform.position;
                attack.transform.position = attackButtonRightPosition.transform.position;
                power.transform.position = PowertRightPosition.transform.position;
            }
            else
            {
               
                joyStick.transform.position = joystickRightPosition.transform.position;
                jump.transform.position = jumpRightPosition.transform.position;
                combat.transform.position = combatRightPosition.transform.position;
                attackJoystick.transform.position = attackJoyStickLeftPosition.transform.position;
                attack.transform.position = attackButtonLeftPosition.transform.position;
                power.transform.position = PowerLeftPosition.transform.position;
            }
        }
    }

   
}
