//----------------------------------------------------------------------------------
///   Riona the Empire Saviour
///   EndScreenData.cs
///   Author            : Geek's Garage
///   Last Modified     : 2022/04/07
///   Description       : An End screen script
///   Revision History  : 2nd ed. Removed the Empty Update function
///----------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndScreenData : MonoBehaviour
{

    [SerializeField] 
    private TextMeshProUGUI scoreTMP;

    [SerializeField]
    private TextMeshProUGUI timeTMP;

    [SerializeField]
    private TextMeshProUGUI wonOrLostTMP;


    // Start is called before the first frame update
    void Start()
    {
        scoreTMP.text = Data.score.ToString();
       
        if (Data.isWon)
        {
            wonOrLostTMP.text = "You Have Won!";
            int minutes = Mathf.FloorToInt(Data.LevelTimer / 60);
            int seconds = Mathf.FloorToInt(Data.LevelTimer - minutes * 60f);
            string textTime = string.Format("{0:00}:{1:00}", minutes, seconds);
            timeTMP.text = textTime;
        }
        else
        {
            wonOrLostTMP.text = "You have Lost!";
            timeTMP.text = "00:00";
        }



    }

}
