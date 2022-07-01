///----------------------------------------------------------------------------------
///   Riona the Empire Saviour
///   SaveOptionsSettings.cs
///   Author            : Geek’s Garage
///   Last Modified     : 2022/02/04
///   Description       : SaveOptionsSettings script stores the settings the user sets in
///                         the option screen
///   Revision History  : 2nd ed. Aligning with Audio Manager
///----------------------------------------------------------------------------------


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SaveOptionsSettings : MonoBehaviour
{
    [Header("ChangeInVolume")]
    private float desiredVolume;

    private void Start()
    {
        if (PlayerPrefs.HasKey("Volume"))
        { 
            desiredVolume = PlayerPrefs.GetFloat("Volume");

            gameObject.GetComponent<Slider>().value = desiredVolume;
        }
      
    }


    //When user changes the position of the volume slider
    public void OnVolumeChange()
    {
       
        desiredVolume = gameObject.GetComponent<Slider>().value;
        
        AudioManager.GetInstance().SetVolume(desiredVolume);

        PlayerPrefs.SetFloat("Volume", desiredVolume);
        PlayerPrefs.Save();
       
    }
  
}
