///----------------------------------------------------------------------------------
///   Riona the Empire Saviour
///   SaveOptionsSettings.cs
///   Author            : Geek’s Garage
///   Last Modified     : 2022/03/27
///   Description       : SaveSensitivity script stores the settings the user sets in
///                         the option screen for joystick sensitivity
///   Revision History  : 1st ed. Follow logic similar to save option settings
///----------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveSensitivity : MonoBehaviour
{
    [Header("Joystick Sensitivity Adjustment")]
    [SerializeField]
    private float joyStickSensitivity;

    private Slider sensitivitySlider;

    // Start is called before the first frame update
    void Start()
    {
        sensitivitySlider = GetComponent<Slider>();
        if (PlayerPrefs.HasKey("Sensitivity"))
        {
            joyStickSensitivity = PlayerPrefs.GetFloat("Sensitivity");
            sensitivitySlider.value = joyStickSensitivity;
            Data.joystickSensitivity = joyStickSensitivity;
        }
    }
    
    public void OnSensitivityChanged()
    {
        joyStickSensitivity = sensitivitySlider.value;

        if (GameObject.FindObjectOfType<GameManager>() != null)
        {
            Data.joystickSensitivity = joyStickSensitivity;
        }
        PlayerPrefs.SetFloat("Sensitivity", joyStickSensitivity);
        PlayerPrefs.Save();
    }
    

}
