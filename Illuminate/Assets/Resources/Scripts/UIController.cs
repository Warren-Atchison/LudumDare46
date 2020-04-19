using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject lightBar;
    public GameObject energyBar;
    public int InventorySlots;

    private PlayerController playerController;
    private Canvas canvas;
    private Slider lightLevel;
    private Slider energyLevel;
    private Sprite frame;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        lightLevel = lightBar.GetComponent<Slider>();
        energyLevel = energyBar.GetComponent<Slider>();
        Sprite frame = Resources.Load<Sprite>("Sprites/InventoryFrame");
        //Test Case, implement with checking inventory capacity of player
        CreateInventoryBar(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        // Resolving global energy changes in the world
        UpdateEnergy();

        // Resolving light changes based relative to player
        UpdateLight();
        if (lightLevel.value == 0f)
            playerController.DieToLight();
    }

    void CreateInventoryBar(float x, float y)
    {
        for (int i = 0; i < InventorySlots; i++)
        {
            GameObject inventoryFrame = Resources.Load<GameObject>("Prefabs/InventoryFrame");
            inventoryFrame.AddComponent<SpriteRenderer>();
            SpriteRenderer spr = inventoryFrame.GetComponent<SpriteRenderer>();
            spr.sprite = frame;
            inventoryFrame.transform.position = new Vector2(x, y);
            Instantiate(inventoryFrame);
            y += 1.03f;
        }

    }

    private void UpdateEnergy()
    {
        energyLevel.value = EnergyHandler.GetFloat();
    }

    private void UpdateLight()
    {
        lightLevel.value = 0.33f;
    }
}
