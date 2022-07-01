///----------------------------------------------------------------------------------
///   Riona the Empire Saviour
///   AssignInventoryToPlayer.cs
///   Author            : Geek�s Garage
///   Last Modified     : 2022/03/03
///   Description       : This script is a scriptable object for inventory.  It creates itemslots based on the items available in the inventory
///   Revision History  : 1st. createInventory and updateInventory functions added- Code optimization
///
///----------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class AssignInventoryToPlayer : MonoBehaviour, ISaveable
{
    public InventoryScriptableObject inventory;



  
    public int XSpace;
    public int XStart;
    public int YStart;
    public int YSpace;
    public int Columns;
    public GameObject inventoryParent;
   
     public static  Dictionary<ItemScriptableObject, GameObject> visibleItems = new Dictionary<ItemScriptableObject, GameObject>();
    public Sprite[] itemIcons;
    public GameObject ItemSlotPrefab;
    private RepresentsItem item;
    private void Start()
    {
        if (visibleItems.Count > 0)
            visibleItems.Clear();
        if (inventory.listOfItems.Count > 0)
            inventory.listOfItems.Clear();
        CreateInventory();
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Pickup") || other.gameObject.CompareTag("CoinBag") || other.gameObject.CompareTag("SpellBook") || other.gameObject.CompareTag("HealingPotion"))

        {
           
            //------VINEET's note: this is item score on item pickup
            if (other.gameObject.CompareTag("SpellBook"))
            {
                GameManager.GetInstance().SpellBookPickUp();
            }
            else
            {
                GameManager.GetInstance().ItemPickUp(10);
            }
            
            var item = other.GetComponent<RepresentsItem>();
            if(item)
            {
             
                inventory.AddItem(item.item, 1, item.icon);
                 UpdateInventory();
                Destroy(other.gameObject);
            }
           
        }

    }
    private void CreateInventory()
    {
        for (int i = 0; i < inventory.listOfItems.Count; i++)
        {
            var obj = Instantiate(inventory.listOfItems[i].item.slotPrefab, Vector3.zero, Quaternion.identity, inventoryParent.transform);
           // obj.transform.localPosition = getPosition(i);
            obj.gameObject.name = inventory.listOfItems[i].item.name;
            obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.listOfItems[i].quantity.ToString("n0");
            obj.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = inventory.listOfItems[i].itemIcon;
            if (!visibleItems.ContainsKey(inventory.listOfItems[i].item))
                visibleItems.Add(inventory.listOfItems[i].item, obj);
        }
    }
    private Vector3 getPosition(int i)
    {
        return new Vector3(XStart + (XSpace * (i % Columns)), YStart + (-YSpace * (i / Columns)), 0.0f);
    }
    private void UpdateInventory()
    {
        for (int i = 0; i < inventory.listOfItems.Count; i++)
        {
          
            if (inventory.listOfItems[i].quantity>1)
            {
               
                visibleItems[inventory.listOfItems[i].item].GetComponentInChildren<TextMeshProUGUI>().text = inventory.listOfItems[i].quantity.ToString("n0");
            }
            else
            {
                if (!visibleItems.ContainsKey(inventory.listOfItems[i].item))
                    
                {
                    var obj = Instantiate(inventory.listOfItems[i].item.slotPrefab, Vector3.zero, Quaternion.identity, inventoryParent.transform);
                 //   obj.transform.localPosition = getPosition(i);
                    obj.gameObject.name = inventory.listOfItems[i].item.name;
                    obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.listOfItems[i].quantity.ToString("n0");
                    obj.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = inventory.listOfItems[i].itemIcon;
                    visibleItems.Add(inventory.listOfItems[i].item, obj);
                }
                   
            }
            
        }
    }




    private void OnApplicationQuit()
    {
        inventory.listOfItems.Clear();
    }

    public void PopulateSaveData(SaveData a_SaveData)
    {
        for (int i = 0; i < inventory.listOfItems.Count; i++)
        {
            SaveData.InventoryItem inventoryItem = new SaveData.InventoryItem();
            inventoryItem.name = inventory.listOfItems[i].item.name;
            inventoryItem.descreption = inventory.listOfItems[i].item.description;
            inventoryItem.type =(int) inventory.listOfItems[i].item.type;
            inventoryItem.Quantity = 1;
            a_SaveData.InventoryItems.Add(inventoryItem);
        }
    }

	public void LoadFromSaveData(SaveData a_SaveData)
	{
        foreach (var item in a_SaveData.InventoryItems)
        {

            ItemScriptableObject itm = new ItemScriptableObject();
            itm.description = item.descreption;
            itm.name = item.name;
            itm.type = (ItemType)item.type;
            itm.itemIcon = GetIcon(itm.type);
            itm.slotPrefab = ItemSlotPrefab;
            inventory.AddItem(itm, 1, itm.itemIcon);
            UpdateInventory();
        }
    }

    Sprite GetIcon(ItemType tp)
	{
		switch (tp)
		{
			case ItemType.SPELLBOOK_TYPE_0:
                return itemIcons[0];
            case ItemType.SPELLBOOK_TYPE_1:
                return itemIcons[0];
            case ItemType.SPELLBOOK_TYPE_2:
                return itemIcons[0];
			case ItemType.POTION:
                return itemIcons[1];
            
		}
        return itemIcons[0];
    }
}
