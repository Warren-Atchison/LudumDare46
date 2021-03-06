﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFactory : MonoBehaviour
{
    public static GameObject CreateItem(string prefabName)
    {
        GameObject output = Resources.Load<GameObject>("Prefabs/" + prefabName);
        return output;
    }

    public static GameObject CreateItem(string prefabName, string gameObjectName)
    {
        GameObject output = Resources.Load<GameObject>("Prefabs/" + prefabName);
        output.name = gameObjectName;
        return output;
    }

    public static void SetLocation(GameObject go, Vector3 newLocation)
    {
        go.transform.position = newLocation;
    }

    public static void SetParent(GameObject child, GameObject parent)
    {
        child.transform.parent = parent.transform;
    }

    public static void SetName(GameObject go, string name)
    {
        go.name = name;
    }
}
