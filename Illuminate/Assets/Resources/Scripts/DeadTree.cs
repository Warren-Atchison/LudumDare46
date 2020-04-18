using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadTree : Interactable
{
    public GameObject chopText;
    public float treeHealth;
    public float progress;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject go = collision.gameObject;
        if (go.name.Equals("Player"))
        {
            chopText.SetActive(true);

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject go = collision.gameObject;
        if (go.name.Equals("Player"))
        {
            chopText.SetActive(false);
        }
    }

    override public float GetHealth()
    {
        return treeHealth;
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
