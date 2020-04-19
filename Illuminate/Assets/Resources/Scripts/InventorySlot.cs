using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Item item;
    public int quantity;

    private Text itemQuantity;
    private Image inventoryItem;
    // Start is called before the first frame update
    void Start()
    {
        itemQuantity = gameObject.GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            quantity--;
            Debug.Log("Test");
            itemQuantity.text = quantity.ToString();
        }
    }
}
