using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : Interactable
{
    private bool touchingGround = false;
    private float progress = 0.0f;
    private float pickupTime = 0.5f;

    public abstract void PickUp();

    public void Drop(string itemName)
    {
        GameObject itemPrefab = ItemFactory.CreateItem(itemName);
        Vector3 spawnLocation = GameObject.FindWithTag("Player").transform.position;
        ItemFactory.SetLocation(itemPrefab, spawnLocation);

        GameObject chunkInstance = Instantiate(itemPrefab) as GameObject;
        ItemFactory.SetParent(chunkInstance, TerrainController.GetCurrentChunk());
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Item Spawned");
    }

    // Update is called once per frame
    void Update()
    {
        if (touchingGround == true)
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ground")
        {
            Debug.Log("Ground");
            touchingGround = true;
        }
    }

    public override void SetProgress(float progress)
    {
        this.progress = progress;
    }

    public override float GetInteractTime()
    {
        return pickupTime;
    }

    public override float GetProgress()
    {
        return progress;
    }
}
