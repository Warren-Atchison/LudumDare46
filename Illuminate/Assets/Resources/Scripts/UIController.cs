using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    private Sprite Frame;
    public int InventorySlots;

    // Start is called before the first frame update
    void Start()
    {
        Frame = Resources.Load<Sprite>("Sprites/InventoryFrame");
        //Test Case, implement with checking inventory capacity of player
        CreateInventoryBar(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateInventoryBar(float x, float y)
    {
        for (int i = 0; i < InventorySlots; i++)
        {
            GameObject inventoryFrame = Resources.Load<GameObject>("Prefabs/InventoryFrame");
            inventoryFrame.AddComponent<SpriteRenderer>();
            SpriteRenderer spr = inventoryFrame.GetComponent<SpriteRenderer>();
            spr.sprite = Frame;
            inventoryFrame.transform.position = new Vector2(x, y);
            Instantiate(inventoryFrame);
            y += 1.03f;
        }

    }
}
