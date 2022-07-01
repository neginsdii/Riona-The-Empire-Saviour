///-------------------------------------------------------------------------------------
///     Riona the Empire Saviour
///     ButtonClick.cs
///     Author          : Geek's Garage
///     Last Modified   : 2022/3/12
///     Description     : Script for applying sound effects to buttons
///     Revision History: 1st ed. Applied audio source to play when onClick delegate is called
/// ------------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour
{

    private Button button;
    private AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        audioSource = GetComponent<AudioSource>();
        button.onClick.AddListener(PlaySound);
    }

    void PlaySound()
    {
        audioSource.Play(); // play the click sound effect attached to the audio source
    }
}
