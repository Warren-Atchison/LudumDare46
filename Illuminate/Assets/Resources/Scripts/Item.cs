using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : Interactable
{
    private bool touchingGround = false;
    private float progress = 0.0f;
    private float pickupTime = 0.5f;

    public abstract void PickUp();

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
