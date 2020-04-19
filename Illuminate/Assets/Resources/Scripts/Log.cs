using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : Item
{
    public override void PickUp()
    {
        InventoryHandler ih = GameObject.Find("InventoryHandler").GetComponent<InventoryHandler>();
        if(ih.AddItem("Log"))
        {
            Debug.Log("Log Picked Up");
            Destroy(this.gameObject);
        }
        
    }

    public void Drop()
    {
        base.Drop("Log");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
            Debug.Log("I am a log");
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
            Debug.Log("Bye bye from the log");
    }
}
