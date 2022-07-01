///----------------------------------------------------------------------------------
///   Riona the Empire Saviour
///   LoadVolume.cs
///   Author            : Geek’s Garage
///   Last Modified     : 2022/02/05
///   Description       : Load Volume script loads the saved volume from PlayerPrefs
///   Revision History  : 1st ed. Read from the player prefs key
///----------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadVolume : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("Volume"))
        {
            AudioManager.GetInstance().SetVolume(PlayerPrefs.GetFloat("Volume"));
        }
    }
}
