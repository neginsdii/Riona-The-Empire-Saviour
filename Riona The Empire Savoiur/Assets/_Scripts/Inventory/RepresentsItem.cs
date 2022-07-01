///----------------------------------------------------------------------------------
///   Riona the Empire Saviour
///   RepresentsItem.cs
///   Author            : Geek’s Garage
///   Last Modified     : 2022/03/03
///   Description       : This script is just a simply container for each pickup.
///                     Each pickup represents a unique item with a unique sprite 
///   Revision History  : 1st ed. Represents item container
///                     
///----------------------------------------------------------------------------------


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RepresentsItem : MonoBehaviour, ISaveable
{
    public ItemScriptableObject item;
    public Sprite icon;
    public string m_id;
    public void LoadFromSaveData(SaveData a_SaveData)
    {

        bool found = false;
        foreach (var item in a_SaveData.items)
        {
            if (m_id == item.m_id)
            {
                this.transform.position = new Vector3(item.posX, item.posY, item.posZ);

                found = true;
                break;
            }
        }
        if (!found)
        {
            Destroy(gameObject);
        }
    }

    public void PopulateSaveData(SaveData a_SaveData)
    {


        SaveData.Item InventoryItem = new SaveData.Item();
        InventoryItem.m_id = m_id;

        InventoryItem.posX = transform.position.x;
        InventoryItem.posY = transform.position.y;
        InventoryItem.posZ = transform.position.z;

        InventoryItem.name = item.name;
        InventoryItem.descreption = item.description;
        InventoryItem.type = (int)item.type;



        a_SaveData.items.Add(InventoryItem);

    }
}
