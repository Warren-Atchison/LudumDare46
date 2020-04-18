using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractHandler : MonoBehaviour
{

    public static void Interact(GameObject go)
    {
        string entityName = go.name;
        Debug.Log("Interacting with: " + entityName);

        if (entityName.Contains("Tree"))
            Chop(go);
    }

    private static void Chop(GameObject tree)
    {
        if (tree.GetComponent<DeadTree>().treeHealth > 0)
        {
            tree.GetComponent<DeadTree>().treeHealth--;
            return;
        }

        // Creating the log prefab and setting its location to the location of the tree
        GameObject log = ItemFactory.CreateItem("Log");
        ItemFactory.SetLocation(log, tree.transform.position);

        // Instantiating a log instance and setting it inside of the chunk it spawned in
        GameObject logObject = Instantiate(log) as GameObject;
        ItemFactory.SetParent(logObject, tree.transform.parent.gameObject);

        Destroy(tree);
    }
}
