using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpPower;
    public bool isGrounded;
    private int curInvSlot;

    public LayerMask groundLayers;
    private Rigidbody2D rb;

    private GameObject collisionObject = null;

    AudioController ac;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        ac = GameObject.Find("AudioController").GetComponent<AudioController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapArea(new Vector2(transform.position.x - 0.5f, transform.position.y - 0.5f),
            new Vector2(transform.position.x + 0.5f, transform.position.y + 0.51f), groundLayers);

        if (Input.GetKeyDown(KeyCode.Space))
            Jump();

        if (Input.GetKey(KeyCode.A))
            rb.velocity = computeVelocity(-1f);
        else if (Input.GetKey(KeyCode.D))
            rb.velocity = computeVelocity(1f);
        else
            rb.velocity = computeVelocity(0f);

        // Inventory slot switching
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            Debug.Log(++curInvSlot);
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            Debug.Log(--curInvSlot);

        // Interact
        if (Input.GetKeyDown(KeyCode.E) && collisionObject != null)
            InteractHandler.Interact(collisionObject);
    }

    Vector2 computeVelocity(float axis = 0f)
    {
        Vector2 move = rb.velocity;

        move.x = axis;
        move.x *= moveSpeed;

        return move;
    }

    private void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collisionObject = collision.gameObject;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collisionObject = null;
    }
}
