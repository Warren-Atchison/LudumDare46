using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpPower;
    public bool isGrounded;
    public GameObject progressBar;

    public LayerMask groundLayers;
    private Rigidbody2D rb;

    private int curInvSlot;
    private GameObject collisionObject = null;

    AudioController ac;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        ac = GameObject.Find("AudioController").GetComponent<AudioController>();
        progressBar.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapArea(new Vector2(transform.position.x - 1.5f, transform.position.y - 1.5f),
            new Vector2(transform.position.x + 0.5f, transform.position.y + 0.51f), groundLayers);

        if (Input.GetKeyDown(KeyCode.Space))
            Jump();

        if (Input.GetKey(KeyCode.A))
            rb.velocity = computeVelocity(-1f);
        else if (Input.GetKey(KeyCode.D))
            rb.velocity = computeVelocity(1f);
        else if (isGrounded)
            rb.velocity = computeVelocity(0f);

        // Inventory slot switching
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            Debug.Log(++curInvSlot);
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            Debug.Log(--curInvSlot);

        // Interact
        if (Input.GetKeyDown(KeyCode.E) && collisionObject != null)
            StartCoroutine("Interact");
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
        Debug.Log("Enter: " + collision.gameObject.name);
        collisionObject = collision.gameObject;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Exit: " + collision.gameObject.name);
        collisionObject = null;
    }

    IEnumerator Interact()
    {
        progressBar.SetActive(true);

        float totalHealth = InteractHandler.GetHealth(collisionObject);
        float startProgress = InteractHandler.GetProgress(collisionObject);

        float timeLeftToComplete = totalHealth - (totalHealth * startProgress);
        float start = Time.time;
        float current = Time.time;

        while(Input.GetKey(KeyCode.E))
        {
            current = Time.time;
            Debug.Log(totalHealth + " : " + (current - start) + " >= " + timeLeftToComplete);
            progressBar.GetComponent<Slider>().value = (current - start) / timeLeftToComplete;

            if (current - start >= timeLeftToComplete)
            {
                InteractHandler.Interact(collisionObject);
                progressBar.SetActive(false);
                StopCoroutine("Interact");
            }

            yield return new WaitForEndOfFrame();
        }

        InteractHandler.SetProgress(collisionObject, startProgress + current - start);
        progressBar.SetActive(false);
        yield return null;
    }
}
