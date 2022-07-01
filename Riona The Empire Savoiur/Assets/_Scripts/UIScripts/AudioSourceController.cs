///----------------------------------------------------------------------------------
///   Riona the Empire Saviour
///   AudioSourceController.cs
///   Author            : Geek’s Garage
///   Last Modified     : 2022/01/26
///   Description       : AudioSourceController script adjusts the volume of soundtrack.
///   Revision History  : 1st ed. Reloading the desired volume from what user entered
///                     in options screen.
///----------------------------------------------------------------------------------



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceController : MonoBehaviour
{
    void Update()
    {
        if (PlayerPrefs.HasKey("Volume"))
        {
            if (PlayerPrefs.GetFloat("Volume") != gameObject.GetComponent<AudioSource>().volume)
            {
                float volume = PlayerPrefs.GetFloat("Volume");
                gameObject.GetComponent<AudioSource>().volume = volume;
                AudioManager.GetInstance().SetVolume(volume);
            }
               
        }
    }
}
