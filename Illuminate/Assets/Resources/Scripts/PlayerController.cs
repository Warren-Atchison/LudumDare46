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
    public GameObject lightBar;
    public GameObject energyBar;

    public LayerMask groundLayers;
    private Rigidbody2D rb;

    private int curInvSlot;
    private bool isInteracting;
    private GameObject collisionObject = null;

    private Slider progressSlider;
    private Slider lightLevel;
    private Slider energyLevel;

    private InventoryHandler inventoryHandler;

    AudioController ac;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        ac = GameObject.Find("AudioController").GetComponent<AudioController>();
        inventoryHandler = GameObject.Find("InventoryHandler").GetComponent<InventoryHandler>();
        progressBar.SetActive(false);

        isInteracting = false;

        progressSlider = progressBar.GetComponent<Slider>();
        lightLevel = lightBar.GetComponent<Slider>();
        energyLevel = energyBar.GetComponent<Slider>();
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


        // Resolving light changes based relative to player
        UpdateLight();
        if (lightLevel.value == 0f)
            Die();

        // Resolving global energy changes in the world
        UpdateEnergy();

        /* +-------------------+
         * |  Player Controls  |
         * +-------------------+ */

        if (!isInteracting)
        {
            //if (Input.GetKeyDown(KeyCode.Space))
                //Jump();

            if (Input.GetKey(KeyCode.A))
                rb.velocity = computeVelocity(-1f);
            else if (Input.GetKey(KeyCode.D))
                rb.velocity = computeVelocity(1f);
            else if (isGrounded)
                rb.velocity = computeVelocity(0f);

            // Inventory slot switching
            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
                inventoryHandler.ChangeInventoryIndex(1);
            if (Input.GetAxis("Mouse ScrollWheel") < 0f)
                inventoryHandler.ChangeInventoryIndex(-1);
            // Inventory dropping
            if (Input.GetKeyDown(KeyCode.Q))
                inventoryHandler.DropItem();
        }

        // Interact
        if (Input.GetKeyDown(KeyCode.E) && collisionObject != null)
        {
            rb.velocity = computeVelocity(0f);
            StartCoroutine("Interact");
        }
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

    private void UpdateLight()
    {
        GameObject[] lights = GameObject.FindGameObjectsWithTag("LightSource");
        float maxLightValue = float.MinValue;
        float maxSourcePower = float.MinValue;
        int maxIdx = -1;

        for(int i = 0; i < lights.Length; i++)
        {
            float dist = Mathf.Abs(Vector3.Distance(lights[i].transform.position, transform.position));
            float lightRad = lights[i].GetComponent<LightSource>().GetLightRadius();

            if (lightRad - dist > maxLightValue)
            {
                maxLightValue = lightRad - dist;
                maxSourcePower = lightRad;
                maxIdx = i;
            }
        }

        if (maxIdx != -1)
            lightLevel.value = maxLightValue / maxSourcePower;
        else
            lightLevel.value = 0f;
    }

    private void UpdateEnergy()
    {
        energyLevel.value = 0f;
    }

    private void Die()
    {
        Debug.Log("DEAD! You are dead.");
    }

    IEnumerator Interact()
    {
        isInteracting = true;
        progressBar.SetActive(true);

        float totalTimeToComplete = InteractHandler.GetInteractTime(collisionObject);
        float startProgress = InteractHandler.GetProgress(collisionObject);

        float timeLeftToComplete = totalTimeToComplete - (totalTimeToComplete * startProgress);
        float start = Time.time;
        float current = Time.time;

        while(Input.GetKey(KeyCode.E))
        {
            current = Time.time;
            progressSlider.value = ((startProgress * totalTimeToComplete) + current - start) / totalTimeToComplete;

            if (current - start >= timeLeftToComplete)
            {
                InteractHandler.Interact(collisionObject);
                progressBar.SetActive(false);
                isInteracting = false;
                StopCoroutine("Interact");
            }

            yield return new WaitForEndOfFrame();
        }

        InteractHandler.SetProgress(collisionObject, startProgress + ((current - start)/totalTimeToComplete));
        progressBar.SetActive(false);
        isInteracting = false;
        yield return null;
    }
}
