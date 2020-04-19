using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainController : MonoBehaviour
{
    public GameObject player;
    public static TerrainController terrainController = null; // Allows other scripts to call functions within AudioController
    public int groundHeight;

    private int prevChunkNum = 0;

    private static int currentChunk = 0;
    private static Dictionary<int, GameObject> chunks;

    private GameObject terrainParent;

    // Start is called before the first frame update
    void Start()
    {
        // If there is no instance of terrainController, make it this
        if (terrainController == null)
            terrainController = this;
        // Enforces a singleton on the terrainController ensuring there will only be one terrainController
        else if (terrainController != this)
            Destroy(gameObject);

        // Generate chunks dictionary
        chunks = new Dictionary<int, GameObject>();

        // Creating a parent game object to place all terrain objects in
        terrainParent = new GameObject();
        terrainParent.name = "Terrain";

        // Generate static spawn chunks
        GenerateSpawn("Forest");

        // Sets TerrainController to DontDestroyOnLoad so that it won't be destroyed when reloading a scene
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        currentChunk = Mathf.RoundToInt(player.transform.position.x / 16);

        if (prevChunkNum != currentChunk)
            GenerateChunk(currentChunk);

        prevChunkNum = currentChunk;
    }

    private void GenerateSpawn(string environment)
    {
        int[] spawnChunks = { -2, -1, 0, 1, 2 };
        foreach(int chunkNum in spawnChunks)
        {
            GameObject chunk = ItemFactory.CreateItem("Ground", chunkNum.ToString());
            Vector3 location = new Vector3(chunkNum * 16, groundHeight);
            ItemFactory.SetLocation(chunk, location);

            GameObject chunkInstance = Instantiate(chunk) as GameObject;
            ItemFactory.SetParent(chunkInstance, terrainParent);

            if (chunkNum == 0)
            {
                if(environment.Equals("Forest"))
                    Populate(chunkInstance, "House");
            }

            chunks.Add(chunkNum, chunkInstance);
        }
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

                GameObject goLeftGround = Instantiate(leftGround) as GameObject;
                ItemFactory.SetName(goLeftGround, (chunkNum - 2).ToString());
                ItemFactory.SetParent(goLeftGround, terrainParent);
                Populate(goLeftGround);
                chunks.Add(chunkNum - 2, goLeftGround);
            }
            else
            {
                chunks[chunkNum - 2].gameObject.SetActive(true);
            }

            Vector3 destroyLocation = new Vector3((chunkNum + 3) * 16, groundHeight);
            Collider2D collider = Physics2D.OverlapCircle(destroyLocation, 2);
            if (collider != null)
                collider.gameObject.SetActive(false);
        }

        // Player is moving right
        if (prevChunkNum < chunkNum)
        {
            if (!chunks.ContainsKey(chunkNum + 2))
            {
                // Generating a new chunk to the right
                GameObject rightGround = ItemFactory.CreateItem("Ground");
                Vector3 newLocation = new Vector3((chunkNum + 2) * 16, groundHeight);
                ItemFactory.SetLocation(rightGround, newLocation);

                GameObject goRightGround = Instantiate(rightGround) as GameObject;
                ItemFactory.SetName(goRightGround, (chunkNum + 2).ToString());
                ItemFactory.SetParent(goRightGround, terrainParent);
                Populate(goRightGround);
                chunks.Add(chunkNum + 2, goRightGround);
            }
            else
            {
                chunks[chunkNum + 2].SetActive(true);
            }

            Vector3 destroyLocation = new Vector3((chunkNum - 3) * 16, groundHeight);
            Collider2D collider = Physics2D.OverlapCircle(destroyLocation, 2);
            if (collider != null)
                collider.gameObject.SetActive(false);
        }
    }

    private void Populate(GameObject parent)
    {
        float xPositionModifier = -8.0f;
        for (int i = 0; i < Random.Range(1, 4); i++)
        {
            
            GameObject tree = ItemFactory.CreateItem("Tree", "Tree");
            Vector3 treePos = new Vector3(parent.transform.position.x +xPositionModifier, parent.transform.position.y + Random.Range(8.0f, 8.7f));
            ItemFactory.SetLocation(tree, treePos);

            GameObject treeAsChild = Instantiate(tree) as GameObject;
            ItemFactory.SetParent(treeAsChild, parent);
            xPositionModifier += Random.Range(1.5f, 5.0f);
        }

    }

    private void Populate(GameObject parent, string prefab)
    {
        GameObject gameObject = ItemFactory.CreateItem(prefab);

        float parentHeight = parent.GetComponent<SpriteRenderer>().bounds.size.y;
        float childHeight = gameObject.GetComponent<SpriteRenderer>().bounds.size.y;

        Vector3 spawnPos = new Vector3(parent.transform.position.x, parent.transform.position.y + ((parentHeight/2) + (childHeight/2)));
        ItemFactory.SetLocation(gameObject, spawnPos);

        GameObject setAsChild = Instantiate(gameObject) as GameObject;
        ItemFactory.SetParent(setAsChild, parent);
    }

    public static GameObject GetCurrentChunk()
    {
        Debug.Log(currentChunk);
        return chunks[currentChunk];
    }
}
