using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LightSource : MonoBehaviour
{
    private bool touchingGround;

    public abstract bool AddToEnergyHandler();
    public abstract float GetValue();
    public abstract bool Place();
    public abstract bool Remove();

    protected void UpdateTouchingGround()
    {
        if (touchingGround == true)
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
    }

    protected void CheckTouchingGround(Collider2D other)
    {
        if (other.tag == "Ground")
        {
            Debug.Log("Ground");
            touchingGround = true;
        }
    }
}
