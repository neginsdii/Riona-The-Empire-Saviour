//----------------------------------------------------------------------------------
///   Riona the Empire Saviour
///   KeyManager.cs
///   Author            : Geek’s Garage
///   Last Modified     : 2022/02/17
///   Description       : Maps the user desired keys to actions
///   Revision History  : 2nd ed. Few bug fixes
///----------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class KeyMappingFromUI : MonoBehaviour
{
    //public TextMeshProUGUI up, down, left, right, jump;
    

    //private GameObject EditingKey;
    //private void Start()
    //{
    //    up.text = KeyManager.keysDictionary["Up"].ToString();
    //    down.text = KeyManager.keysDictionary["Down"].ToString();
    //    left.text = KeyManager.keysDictionary["Left"].ToString();
    //    right.text = KeyManager.keysDictionary["Right"].ToString();
    //    jump.text = KeyManager.keysDictionary["Jump"].ToString();
    //}
   
    //private void OnGUI()
    //{
    //    if (EditingKey != null)
    //    {
    //        Event keyPressed = Event.current;

    //        if (keyPressed.isKey)
    //        {
                
    //            KeyManager.keysDictionary.Remove(EditingKey.name);
    //            KeyManager.keysDictionary.Add(EditingKey.name, keyPressed.keyCode);
    //            EditingKey.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = keyPressed.keyCode.ToString();
    //            EditingKey.GetComponent<Image>().color = Color.white;
    //            EditingKey = null;
    //        }
    //    }
    //}

    //public void ChangeKeyButtonStyle(GameObject clicked)
    //{
    //    EditingKey = clicked;

    //    EditingKey.GetComponent<Image>().color = Color.cyan;
    //}


}
