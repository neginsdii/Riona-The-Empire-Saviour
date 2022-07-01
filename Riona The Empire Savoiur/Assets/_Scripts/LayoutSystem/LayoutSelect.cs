///----------------------------------------------------------------------------------
///   Riona the Empire Saviour
///   LayoutSelect.cs
///   Author            : Geek’s Garage
///   Last Modified     : 2022/03/08
///   Description       : This script is used to select a desired layout for the player and sets the position of onscreen controls accordingly. 
///   Revision History  : 1st. lefLayoutSlect and RightLayoutSelect functions added
///                     
///----------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LayoutSelect : MonoBehaviour
{
    public GameObject leftPanel, rightPanel;
    [Header("positions")]
    public GameObject joystickLeftPosition;
    public GameObject joystickRightPosition;
    public GameObject jumpLeftPosition;
    public GameObject jumpRightPosition;
    public GameObject attackJoystickLeftPosition;
    public GameObject attackJoystickRightPosition;
    public GameObject attackButtonLeftPosition;
    public GameObject attackButtonRightPosition;
    public GameObject combatLeftPosition;
    public GameObject combatRightPosition;
    public GameObject PowerLeftPosition;
    public GameObject PowertRightPosition;

    [Header("Onscreen Controls")]
    public GameObject joyStick;
    public GameObject jump;
    public GameObject attackJoyStick;
    public GameObject attack;
    public GameObject combat;
    public GameObject power;
    private void Start()
    {
        if (PlayerPrefs.HasKey("Layout"))
        {

            if (PlayerPrefs.GetString("Layout") == "Left")
            {
                leftPanel.SetActive(true);
                rightPanel.SetActive(false);
            }
            else
            {
                leftPanel.SetActive(false);
                rightPanel.SetActive(true);
            }
        }
        }

    public void LeftLayoutSelect()
    {
        leftPanel.SetActive(true);
        rightPanel.SetActive(false);

        PlayerPrefs.SetString("Layout", "Left");
       
        PlayerPrefs.Save();
        if(SceneManager.GetActiveScene().name== "GameplayScreen")
        {
            joyStick.transform.position = joystickLeftPosition.transform.position;
            jump.transform.position = jumpLeftPosition.transform.position;
            combat.transform.position = combatLeftPosition.transform.position;
            attackJoyStick.transform.position = attackJoystickRightPosition.transform.position;
            attack.transform.position = attackButtonRightPosition.transform.position;
            power.transform.position = PowertRightPosition.transform.position;
        }

    }
    public void RightLayoutSelect()
    {
        leftPanel.SetActive(false);
        rightPanel.SetActive(true);
        PlayerPrefs.SetString("Layout", "Right");
       
        PlayerPrefs.Save();
        if (SceneManager.GetActiveScene().name == "GameplayScreen")
        {
            joyStick.transform.position = joystickRightPosition.transform.position;
            jump.transform.position = jumpRightPosition.transform.position;
            combat.transform.position = combatRightPosition.transform.position;
            attackJoyStick.transform.position = attackJoystickLeftPosition.transform.position;
            attack.transform.position = attackButtonLeftPosition.transform.position;
            power.transform.position = PowerLeftPosition.transform.position;
        }
    }
}
