using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : Item
{
    public override void PickUp()
    {
        Destroy(this);
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
