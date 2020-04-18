using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainController : MonoBehaviour
{
    public GameObject player;
    public static TerrainController terrainController = null; // Allows other scripts to call functions within AudioController
    public int groundHeight;

    private int prevChunkNum = 0;
    private Dictionary<int, GameObject> chunks;

    // Start is called before the first frame update
    void Start()
    {
        // If there is no instance of terrainController, make it this
        if (terrainController == null)
            terrainController = this;
        // Enforces a singleton on the terrainController ensuring there will only be one terrainController
        else if (terrainController != this)
            Destroy(gameObject);

        // Generate base chunks
        chunks = new Dictionary<int, GameObject>();

        // Sets TerrainController to DontDestroyOnLoad so that it won't be destroyed when reloading a scene
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        int chunkNum = Mathf.RoundToInt(player.transform.position.x / 16);

        if (prevChunkNum != chunkNum)
            GenerateChunk(chunkNum);

        prevChunkNum = chunkNum;
    }

    private void GenerateChunk(int chunkNum)
    {
        // Player is moving left
        if(prevChunkNum > chunkNum)
        {
            if (!chunks.ContainsKey(chunkNum - 2))
            {
                // Generating a new chunk to the left
                GameObject leftGround = ItemFactory.CreateItem("Ground", (chunkNum - 2).ToString());
                Vector3 newLocation = new Vector3((chunkNum - 2) * 16, groundHeight);
                ItemFactory.SetLocation(leftGround, newLocation);
                chunks.Add(chunkNum - 2, Instantiate(leftGround));
            }
            else
            {
                chunks[chunkNum - 2].gameObject.SetActive(true);
            }

            Vector3 destroyLocation = new Vector3((chunkNum + 3) * 16, groundHeight);
            Collider2D collider = Physics2D.OverlapCircle(destroyLocation, 2);
            Debug.Log(destroyLocation.ToString() + " : " + collider.ToString());
            if (collider != null)
                collider.gameObject.SetActive(false);
        }

        // Player is moving right
        if (prevChunkNum < chunkNum)
        {
            if (!chunks.ContainsKey(chunkNum + 2))
            {
                // Generating a new chunk to the right
                GameObject rightGround = ItemFactory.CreateItem("Ground", (chunkNum + 2).ToString());
                Vector3 newLocation = new Vector3((chunkNum + 2) * 16, groundHeight);
                ItemFactory.SetLocation(rightGround, newLocation);
                chunks.Add(chunkNum + 2, Instantiate(rightGround));
            }
            else
            {
                chunks[chunkNum + 2].SetActive(true);
            }

            Vector3 destroyLocation = new Vector3((chunkNum - 3) * 16, groundHeight);
            Collider2D collider = Physics2D.OverlapCircle(destroyLocation, 2);
            Debug.Log(destroyLocation.ToString() + " : " + collider.ToString());
            if (collider != null)
                collider.gameObject.SetActive(false);
        }

        PrintDict();
    }

    private void PrintDict()
    {
        foreach (KeyValuePair<int, GameObject> kv in chunks)
            Debug.Log(kv.Key + " : " + kv.Value.name);
    }
}
