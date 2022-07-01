///----------------------------------------------------------------------------------
///   Riona the Empire Saviour
///   ItemTable.cs
///   Author            : Geek’s Garage
///   Last Modified     : 2022/03/17
///   Description       : Master Item table script to keep track of items
///   Revision History  : 1st ed. Item Table List and IDs
///----------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "MasterItemTable", menuName = "ItemSystem/ItemTable")]
public class ItemTable : ScriptableObject
{
    [SerializeField]
    private List<ItemScriptableObject> itemTable;


    /// <summary>
    /// Get Item from Item Table directly
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public ItemScriptableObject GetItem(int id)
    {
        return itemTable[id];
    }
}
