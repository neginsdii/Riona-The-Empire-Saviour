///----------------------------------------------------------------------------------
///   Riona the Empire Saviour
///   ItemUsage.cs
///   Author            : Geek�s Garage
///   Last Modified     : 2022/04/07
///   Description       : Handles usage of items based on type and count
///   Revision History  : 3rd ed. Commented out Debug.Log                    
///----------------------------------------------------------------------------------



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script manages the usage of items. Whenever user clicks on an item slot,
/// if the quantity of the item is equal to 1
/// the item gets removed from the item list
/// and the item slot gets disappeared.
/// but if the quantity is more than one, we just simply subtract the quantity by 1
/// </summary>
public class ItemUsage : MonoBehaviour
{
    public InventoryScriptableObject inventory;
    [Header("Audio")]
    public AudioClip[] equipClip;

    private ItemType typeOfItem = ItemType.TOTAL_TYPES;

    public GameObject closeButton;

  
    public void OnItemClicked()
    {
        if(closeButton==null)
        {
            GameObject[] button = GameObject.FindGameObjectsWithTag("CloseButton");
            closeButton = button[0];
        }

        closeButton.GetComponent<Button>().interactable = false;
        StartCoroutine(delayForRepositioning());

      
        if (int.Parse(gameObject.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text) > 1)
        {

            int count = int.Parse(gameObject.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text);
            gameObject.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = (count - 1).ToString();
            for (int i = 0; i < inventory.listOfItems.Count; i++)
            {
                if(inventory.listOfItems[i].item.name == gameObject.name)
                {
                    if(inventory.listOfItems[i].item.name=="Healing Potion")
                        AudioManager.GetInstance().PlaySFX(GetComponent<AudioSource>(), equipClip[1], 0.5f, false);
                    else
                        AudioManager.GetInstance().PlaySFX(GetComponent<AudioSource>(), equipClip[0], 0.5f, false);
                    // Item usage >> Implement potions

                    //Debug.Log("Item destroyed type: " + inventory.listOfItems[i].item.type);
                    typeOfItem = inventory.listOfItems[i].item.type;

                    inventory.listOfItems[i].SubtractQuantity(inventory.listOfItems[i].quantity);
                }
            }
        }
        else
        {
            for (int i = 0; i < inventory.listOfItems.Count; i++)
            {
                if (inventory.listOfItems[i].item.name == gameObject.name)
                {
                    if (inventory.listOfItems[i].item.name == "Healing Potion")
                        AudioManager.GetInstance().PlaySFX(GetComponent<AudioSource>(), equipClip[1], 0.5f, false);
                    else
                        AudioManager.GetInstance().PlaySFX(GetComponent<AudioSource>(), equipClip[0], 0.5f, false);
                 
                    AssignInventoryToPlayer.visibleItems.Remove(inventory.listOfItems[i].item);

                    // Item usage >> Potions and Spell books
                    //Debug.Log("Item destroyed type: " + inventory.listOfItems[i].item.type);
                    typeOfItem = inventory.listOfItems[i].item.type;


                    inventory.RemoveItem(inventory.listOfItems[i].item, 1);
                  
                }
            }
            if ((GetComponent<AudioSource>().isPlaying))
            {

                GetComponent<Image>().color = new Color(0,0,0,0) ;
                transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(1).gameObject.SetActive(false);
                StartCoroutine(delayBeforeDestroy());
              
            }
        }

        // If item was actually selected
        if (typeOfItem != ItemType.TOTAL_TYPES)
        {
            // based on type of item
            switch (typeOfItem)
            {
                case ItemType.SPELLBOOK_TYPE_1:
                    GameManager.GetInstance().Player.GetComponent<PlayerBehaviour>().EnableAttackType(ItemType.SPELLBOOK_TYPE_1);
                    break;


                case ItemType.SPELLBOOK_TYPE_2:
                    GameManager.GetInstance().Player.GetComponent<PlayerBehaviour>().EnableAttackType(ItemType.SPELLBOOK_TYPE_2);
                    break;


                case ItemType.POTION:
                    GameManager.GetInstance().Player.GetComponent<PlayerBehaviour>().UsePotion(FindObjectOfType<PlayerBehaviour>());
                    break;
            }
        }
    }
    IEnumerator delayBeforeDestroy()
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(gameObject);
    }
    IEnumerator delayForRepositioning()
    {
        yield return new WaitForSeconds(0.8f);
        closeButton.GetComponent<Button>().interactable = true;
    }
}
