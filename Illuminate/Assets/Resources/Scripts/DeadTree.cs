using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadTree : MonoBehaviour
{
    public GameObject chopText;
    public int treeHealth = 3;

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
}
