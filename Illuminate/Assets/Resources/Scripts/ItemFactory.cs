using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFactory : MonoBehaviour
{
    public static GameObject CreateItem(string prefabName)
    {
        GameObject output = Resources.Load<GameObject>("Prefabs/" + prefabName);
        return output;
    }

    public static void SetLocation(GameObject go, Vector3 newLocation)
    {
        go.transform.position = newLocation;
    }
}
