using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractHandler : MonoBehaviour
{

    public static void Interact(GameObject go)
    {
        Debug.Log("Interacting with: " + go.name);

        if (go.tag.Equals("Item"))
            PickUp(go);

        if (go.name.Contains("Tree"))
            Chop(go);
    }

    private static void Chop(GameObject tree)
    {
        // Creating the log prefab and setting its location to the location of the tree
        GameObject log = ItemFactory.CreateItem("Log");
        ItemFactory.SetLocation(log, tree.transform.position);

        // Instantiating a log instance and setting it inside of the chunk it spawned in
        GameObject logObject = Instantiate(log) as GameObject;
        ItemFactory.SetParent(logObject, tree.transform.parent.gameObject);

        Destroy(tree);
    }

    private static void PickUp(GameObject item)
    {
        item.GetComponent<Item>().PickUp();
        Destroy(item);
    }

    public static float GetInteractTime(GameObject go)
    {
        return go.GetComponent<Interactable>().GetInteractTime();
    }

    public static float GetProgress(GameObject go)
    {
        return go.GetComponent<Interactable>().GetProgress();
    }

    public static void SetProgress(GameObject go, float progress)
    {
        go.GetComponent<Interactable>().SetProgress(progress);
    }
}
