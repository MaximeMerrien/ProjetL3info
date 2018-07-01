﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;


    public class SplitItem : MonoBehaviour, IPointerDownHandler
    {     //splitting an Item

        public Inventory inv;                          //inventory script  
        static InputManager inputManagerDatabase = null;

        void Start()
        {
            inputManagerDatabase = (InputManager)Resources.Load("InputManager");
        }

        public void OnPointerDown(PointerEventData data)                    //splitting the item now
        {
            inv = transform.parent.parent.parent.GetComponent<Inventory>();
            if (transform.parent.parent.parent.GetComponent<CraftSystem>() == null && transform.parent.parent.parent.GetComponent<Hotbar>() == null && Input.GetKeyDown(KeyCode.Mouse1) && inv.stackable && (inv.ItemsInInventory.Count < (inv.height * inv.width))) //if you press rightclick
            {
                ItemOnObject itemOnObject = GetComponent<ItemOnObject>();                                                   //we take the ItemOnObject script of the item in the slot

                if (itemOnObject.item.itemValue > 1)                                                                         //we split the item only when we have more than 1 in the stack
                {
                    int splitPart = itemOnObject.item.itemValue;                                                           //we take the value and store it in there
                    itemOnObject.item.itemValue = (int)itemOnObject.item.itemValue / 2;                                     //calculate the new value for the splitted item
                    splitPart = splitPart - itemOnObject.item.itemValue;                                                   //take the different

                    inv.addItemToInventory(itemOnObject.item.itemID, splitPart);                                            //and add a new item to the inventory
                    inv.stackableSettings();

                    if (GetComponent<ConsumeItem>().duplication != null)
                    {
                        GameObject dup = GetComponent<ConsumeItem>().duplication;
                        dup.GetComponent<ItemOnObject>().item.itemValue = itemOnObject.item.itemValue;
                        dup.GetComponent<SplitItem>().inv.stackableSettings();
                    }
                    inv.updateItemList();
                }
            }
        }
    }


//static InputManager inputManagerDatabase = null;
//private bool pressingButtonToSplit;             //bool for pressing a item to split it

/*void Update()
{
    if (Input.GetKeyDown(inputManagerDatabase.SplitItem))                     //if we press right controll the ....
        pressingButtonToSplit = true;                               //getting changed to true 
    if (Input.GetKeyUp(inputManagerDatabase.SplitItem))
        pressingButtonToSplit = false;                              //or false

}

void Start()
{
    inputManagerDatabase = (InputManager)Resources.Load("InputManager");
}*/
