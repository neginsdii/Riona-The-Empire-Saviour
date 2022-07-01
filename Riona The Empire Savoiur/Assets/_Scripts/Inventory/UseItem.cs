///----------------------------------------------------------------------------------
///   Riona the Empire Saviour
///   UseItem.cs
///   Author            : Geek’s Garage
///   Last Modified     : 2022/01/29
///   Description       : This script manages the usage of items
///   Revision History  : 2nd. ed Update function removed for this push as the
///                     functionality for remove items is not needed                    
///----------------------------------------------------------------------------------



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseItem : MonoBehaviour
{
    [Header("Inventory Slots")]
    public GameObject ReferenceToPlayer;

    [Header("Audio")]
    public AudioClip equipClip;

    private void Start()
    {
        ReferenceToPlayer = GameObject.FindGameObjectWithTag("Player");
    }

    /*
     * whenever the player clicks on any item on inventory this function is called
     * this function equips user with that item and remove that from the inventory
     */

    public void OnItemClick()
    {
        if (gameObject.transform.GetChild(0).GetComponent<Image>().sprite != null)
        {
            DeleteItem();
            AudioManager.GetInstance().PlaySFX(GetComponent<AudioSource>(), equipClip, 0.5f, false);
            if (int.Parse(gameObject.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text) > 1)
            {
                int count = int.Parse(gameObject.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text);
                gameObject.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = (count - 1).ToString();

            }
            else
            {

                gameObject.transform.GetChild(0).GetComponent<Image>().sprite = null;
                gameObject.transform.GetChild(0).GetComponent<Image>().color = new Color(
                    gameObject.transform.GetChild(0).GetComponent<Image>().color.r,
                    gameObject.transform.GetChild(0).GetComponent<Image>().color.g,
                    gameObject.transform.GetChild(0).GetComponent<Image>().color.b, 0);
                gameObject.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = "";
            }

        }

    }
    /*
   * removes the item from the items list in the inventory
   */

    private void DeleteItem()
    {
        for (int i = 0; i < ReferenceToPlayer.gameObject.GetComponent<AddToInventory>().pickupItems.Count; i++)
        {


            if (gameObject.transform.GetChild(0).GetComponent<Image>().sprite.name == ReferenceToPlayer.gameObject
                .GetComponent<AddToInventory>().pickupItems[i].pickupImage.name)
            {
                if (ReferenceToPlayer.gameObject.GetComponent<AddToInventory>().pickupItems[i].count > 1)
                {
                    ReferenceToPlayer.gameObject.GetComponent<AddToInventory>().pickupItems[i].count--;
                }
                else
                {
                    ReferenceToPlayer.gameObject.GetComponent<AddToInventory>().pickupItems.RemoveAt(i);
                }

            }
        }

    }
}

