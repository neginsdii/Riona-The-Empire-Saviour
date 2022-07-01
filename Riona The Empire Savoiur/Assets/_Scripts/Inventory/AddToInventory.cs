///----------------------------------------------------------------------------------
///   Riona the Empire Saviour
///   AddToInventory.cs
///   Author            : Geek’s Garage
///   Last Modified     : 2022/01/29
///   Description       : AddToInventory script checks for the collision between player and pickups,
///                     if the player collides with a pickup, the item will be added to the inventory
///   Revision History  : 1st ed. OnTriggerEnter and AddToInventory functions added                    
///----------------------------------------------------------------------------------


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class AddToInventory : MonoBehaviour
{
    [Header("Inventory")]
    public List<PickUp> pickupItems;

    [Header("ItemsSprite"), SerializeField]
    private Sprite spellBook;


    private void Start()
    {

        pickupItems = new List<PickUp>();
    }
    //When the player collides with the pickup items, this function destroys that item and calls the AddItemToInventory function to add it to the inventory list
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup") || other.gameObject.CompareTag("CoinBag"))

        {
            //------VINEET's note: this is item score on item pickup
            GameManager._instance.ItemPickUp(10);
            AddItemToInventory(other.gameObject.tag);
            Destroy(other.gameObject);
        }

    }
    private void AddItemToInventory(string tag)
    {
        int index = 0;
        bool found = false;
        while (index < pickupItems.Count)
        {
           
            if (tag == pickupItems[index].name)
            {
                pickupItems[index].count++;
                found = true;
            }
            index++;
        }
        if (!found)
        {
            pickupItems.Add(new PickUp { name = tag, pickupImage = spellBook, count = 1 });
            
        }
    }
}
