///----------------------------------------------------------------------------------
///   Riona the Empire Saviour
///   OptionLayoutscreen.cs
///   Author            : Geek’s Garage
///   Last Modified     : 2022/02/05
///   Description       : Display options panel button functions
///   Revision History  : 1st ed. Enable/Disable the options panel
///----------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionLayoutscreen : MonoBehaviour
{
    [SerializeField]
    GameObject optionsPanel;

    public void onOptionClicked()
    {
        if(optionsPanel)
        {
            optionsPanel.SetActive(false);
        }
    }
    public void onOptionOpenClicked()
    {
        if (optionsPanel)
        {
            optionsPanel.SetActive(true);
        }
    }
}
