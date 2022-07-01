///----------------------------------------------------------------------------------
///   Riona the Empire Saviour
///   ItemScriptableObject.cs
///   Author            : Geek�s Garage
///   Last Modified     : 2022/03/17
///   Description       : This script is a scriptable object for each item
///   Revision History  : 2nd ed. Item Script updated with few more required
///                     properties (Unique ID) 
///----------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "NewItem", menuName = "ItemSystem/Item")]
public  class ItemScriptableObject : ScriptableObject
{
    public new string name;
    public ItemType type;
    public GameObject slotPrefab;
    public Sprite itemIcon;
    public string description;
    public bool isConsumable = false;
    public int itemID;
}
public enum ItemType
{
    SPELLBOOK_TYPE_0,
    SPELLBOOK_TYPE_1,
    SPELLBOOK_TYPE_2,
    COINBAG,
    POTION,
    TOTAL_TYPES
}
