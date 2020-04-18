using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItem : MonoBehaviour
{
    private bool touchingGround = false;

    public Rigidbody2D rigidbody2D;

    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Item Spawned");
        rigidbody2D = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (touchingGround == true)
            rigidbody2D.constraints = RigidbodyConstraints2D.FreezePosition;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ground")
        {
            Debug.Log("Ground");
            touchingGround = true;
        }
    }

}
