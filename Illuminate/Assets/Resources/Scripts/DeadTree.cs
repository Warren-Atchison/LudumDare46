using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadTree : Interactable
{
    public GameObject chopText;
    public float chopTime;
    public float progress;

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject go = collision.gameObject;
        if (go.name.Equals("Player"))
        {
            chopText.SetActive(false);
        }
    }

    override public float GetInteractTime()
    {
        return chopTime;
    }

    override public float GetProgress()
    {
        return progress;
    }

    public override void SetProgress(float progress)
    {
        this.progress = progress;
    }
}
