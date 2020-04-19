using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryHandler : MonoBehaviour
{
    public string[] Inventory { get; set; }
    private int InventorySize;

    private int CurrentIndex;

    private GameObject[] InventoryFrames;
    private GameObject selector;

    public void Awake()
    {
         Inventory = new string[3] { " ", " ", " " };
        InventorySize = Inventory.Length;
        selector = GameObject.FindGameObjectsWithTag("InventorySelector")[0];

    }
    public void Update()
    {
        InventoryFrames = GameObject.FindGameObjectsWithTag("InventorySlot");
        //Debug.Log("Loaded " + InventoryFrames.Length + "Frames");

        if (InventoryFrames.Length > 0)
        {
            for (int i = 0; i < InventoryFrames.Length; i++)
            {
                InventoryFrames[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + Inventory[i]);
            }
        }
    }
    public bool AddItem(string itemName)
    { 
            for(int i = 0; i < InventorySize; i++)
            {
                if (Inventory[i].Equals(" "))
                {
                    Inventory[i] = itemName;
                    //Displays All Inventory
                    Debug.Log("Current Inventory"  + Inventory[0] + Inventory [1] + Inventory[2]);

                    return true;
                }
            } 
        return false;
    }

    public bool DropItem()
    {
        if (Inventory[CurrentIndex].Equals(" "))
            return false;
        else
        {
            var gameObject = ItemFactory.CreateItem(Inventory[CurrentIndex]);
            ItemFactory.SetLocation(gameObject, GameObject.Find("Player").GetComponent<Transform>().position);
            Instantiate(gameObject);
            Inventory[CurrentIndex] = " ";
            Debug.Log("Dropped " + gameObject.name);
            Debug.Log("Current Inventory " + Inventory[0] + "/" + Inventory[1] + "/" + Inventory[2]);
            return true;
        }
        
    }

    public void ChangeInventoryIndex(int i)
    { 

        if(CurrentIndex + i > InventorySize-1 || CurrentIndex + i < 0)
        {
            Debug.Log("Inventory Bound Reached" + CurrentIndex);
        }
        else
        {
            CurrentIndex += i;
        }

        selector.transform.position = InventoryFrames[CurrentIndex].transform.position;
    }

    
}

